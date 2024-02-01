namespace DAL.Tools
{
    public class AppointmentRequester
    {
        private string _connectionString;
        public AppointmentRequester(string connectionString)
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
                        appointments.Add(AppointmentMapper.ToAppointment(
                        (Guid)reader["AppointmentId"],
                        (DateTime)reader["AppointmentDate"],
                        (DateTime)reader["AppointmentCreationDate"],
                        (DateTime)reader["AppointmentUpdateDate"],
                        ToolSet.ReturnNonDBNull<int>(reader["DurationMinutes"]),
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
    }
}
