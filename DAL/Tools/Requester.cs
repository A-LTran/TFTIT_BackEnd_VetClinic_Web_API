namespace DAL.Tools
{
    public class Requester
    {
        private string _connectionString;
        public Requester(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Appointment?> GetAppointmentsBy<TBody>(string sqlcommandtxt, string bodyName, TBody body)
        {
            using (SqlConnection connection = new(_connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = sqlcommandtxt;
                command.Parameters.AddWithValue(bodyName, body);

                connection.Open();
                List<Appointment?> appointments = new();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        appointments.Add(((Guid)reader["AppointmentId"]).ToAppointment(
                        (DateTime)reader["AppointmentDate"],
                        (DateTime)reader["AppointmentCreationDate"],
                        (string)reader["Reason"],
                        (string)reader["Diagnosis"],
                        (Guid)reader["AnimalId"],
                        (Guid)reader["VeterinaryId"]));
                    }
                }
                connection.Close();
                return appointments;
            }
        }

        public User? GetUserBy<TBody>(string sqlCommandText, string bodyName, TBody body)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = sqlCommandText;
                command.Parameters.AddWithValue(bodyName, body);

                User? u = new();

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string? phone = ToolSet.ReturnNonDBNull<string>(reader["Phone"]);
                        string? mobile = ToolSet.ReturnNonDBNull<string>(reader["Mobile"]);
                        DateTime birthDate = ToolSet.ReturnNonDBNull<DateTime>(reader["BirthDate"]);
                        Guid addressId = ToolSet.ReturnNonDBNull<Guid>(reader["AddressId"]);
                        Role personRole = ToolSet.ReturnNonDBNull<Role>(reader["PersonRole"]);

                        u = (UserMapper.ToUser((Guid)reader["PersonId"], (string)reader["LastName"], (string)reader["FirstName"], (string)reader["Email"], phone, mobile, birthDate, (string)reader["UserPassword"], personRole, addressId)!);
                    }
                }
                connection.Close();
                return u;
            }
        }
    }
}
