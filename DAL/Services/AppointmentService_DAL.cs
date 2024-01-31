namespace DAL.Services
{
    public class AppointmentService_DAL : IAppointmentRepository_DAL
    {
        private readonly string _connectionString;
        private Requester _requester;

        public AppointmentService_DAL(string connectionString)
        {
            _connectionString = connectionString;
            _requester = new Requester(connectionString);
        }

        public IEnumerable<Appointment?> Get()
        {
            using (SqlConnection connection = new(_connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Appointment";

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

        public IEnumerable<Appointment?> GetByVeterinaryId(Guid id)
        {
            return _requester.GetAppointmentsBy<Guid>("SELECT * FROM Appointment WHERE VeterinaryId = @id", "@id", id);
        }
        public IEnumerable<Appointment?> GetByDogName(string name)
        {
            return _requester.GetAppointmentsBy<string>("SELECT * FROM Appointment " +
                                                            "WHERE AnimalId = (SELECT AnimalID " +
                                                                            "FROM ClinicAnimal " +
                                                                            "WHERE AnimalName = @name)", "@name", name);
        }

        public IEnumerable<Appointment?> GetByAppointmentRange(Guid vetId, DateTime date)
        {
            string sql = "SELECT * FROM Appointment " +
                            "WHERE VeterinaryId = \'" + vetId + "\' AND AppointmentDate " +
                            "BETWEEN @date and DATEADD(minute, 30, @DATE)";
            return _requester.GetAppointmentsBy<DateTime>(sql, "@date", date);
        }


        public bool Create(Appointment appointment)
        {
            using (SqlConnection connection = new(_connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO Appointment (AppointmentID, AppointmentDate, AppointmentCreationDate, Reason, Diagnosis, AnimalId, VeterinaryId) VALUES (@appointmentId, @appointmentDate, @appointmentCreationDate, @reason, @diagnosis, @animalId, @veterinaryId)";

                command.Parameters.AddWithValue("@appointmentId", appointment.AppointmentId);
                command.Parameters.AddWithValue("@appointmentDate", appointment.AppointmentDate);
                command.Parameters.AddWithValue("@appointmentCreationDate", appointment.AppointmentCreationDate);
                command.Parameters.AddWithValue("@reason", appointment.Reason);
                command.Parameters.AddWithValue("@diagnosis", appointment.Diagnosis);
                command.Parameters.AddWithValue("@animalId", appointment.AnimalId);
                command.Parameters.AddWithValue("@veterinaryId", appointment.VeterinaryId);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                return rowsAffected > 0;
            }
        }
    }
}
