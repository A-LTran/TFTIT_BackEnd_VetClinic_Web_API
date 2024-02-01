namespace DAL.Services
{
    public class AppointmentService_DAL : IAppointmentRepository_DAL
    {
        private readonly string _connectionString;
        private AppointmentRequester _requester;

        public AppointmentService_DAL(string connectionString)
        {
            _connectionString = connectionString;
            _requester = new AppointmentRequester(connectionString);
        }

        public IEnumerable<Appointment?> Get(int scope)
        {
            return _requester.GetAppointmentsBy<string>(AppointmentDateScope.SetScope("SELECT * FROM Appointment", scope), "", "");
        }

        public IEnumerable<Appointment?> GetByVeterinaryId(Guid id, int scope)
        {
            return _requester.GetAppointmentsBy<Guid>(AppointmentDateScope.SetScope("SELECT * FROM Appointment WHERE VeterinaryId = @id", scope), "@id", id);
        }

        public IEnumerable<Appointment?> GetByOwnerId(Guid id, int scope)
        {
            return _requester.GetAppointmentsBy<Guid>(AppointmentDateScope.SetScope("SELECT * FROM Appointment WHERE AnimalId IN " +
                                                                                                                    "(SELECT AnimalID " +
                                                                                                                    "FROM ClinicAnimal " +
                                                                                                                    "WHERE OwnerId = @id)", scope), "@id", id);
        }

        public IEnumerable<Appointment?> GetByAnimalName(string name, int scope)
        {
            return _requester.GetAppointmentsBy<string>(AppointmentDateScope.SetScope("SELECT * FROM Appointment " +
                                                            "WHERE AnimalId = (SELECT AnimalID " +
                                                                            "FROM ClinicAnimal " +
                                                                            "WHERE AnimalName = @name)", scope), "@name", name);
        }

        public IEnumerable<Appointment?> GetByAnimalId(Guid id, int scope)
        {
            return _requester.GetAppointmentsBy<Guid>(AppointmentDateScope.SetScope("SELECT * FROM Appointment " +
                                                            "WHERE AnimalId = @id", scope), "@id", id);
        }

        public IEnumerable<Appointment?> GetByAppointmentRange(Guid vetId, DateTime date, int durationMinutes = 30)
        {
            string sql = "SELECT * FROM Appointment " +
                            "WHERE VeterinaryId = \'" + vetId + "\' AND (AppointmentDate " +
                            "BETWEEN @appointmentDate and DATEADD(minute, " + durationMinutes + ", @appointmentDate) " +
                            "OR DATEADD(minute, DurationMinutes, AppointmentDate) " +
                            "BETWEEN @appointmentDate and DATEADD(minute, " + durationMinutes + ", @appointmentDate))";

            return _requester.GetAppointmentsBy<DateTime>(sql, "@appointmentDate", date);
        }

        public bool Create(Appointment appointment)
        {
            using (SqlConnection connection = new(_connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO Appointment (AppointmentID, AppointmentDate, AppointmentCreationDate, DurationMinutes, Reason, Diagnosis, AnimalId, VeterinaryId) VALUES (@appointmentId, @appointmentDate, @appointmentCreationDate, @durationMinutes, @reason, @diagnosis, @animalId, @veterinaryId)";

                command.Parameters.AddWithValue("@appointmentId", appointment.AppointmentId);
                command.Parameters.AddWithValue("@appointmentDate", appointment.AppointmentDate);
                command.Parameters.AddWithValue("@appointmentCreationDate", appointment.AppointmentCreationDate);
                command.Parameters.AddWithValue("@durationMinutes", appointment.DurationMinutes);
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
