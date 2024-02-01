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

        //******************************************************************************//
        //                                      GET                                     //
        //******************************************************************************//

        public IEnumerable<Appointment?> Get(int scope)
        {
            return _requester.GetAppointmentsBy<string>(AppointmentDateScope.SetScope("SELECT * FROM Appointment", scope, -1), "", "");
        }

        public IEnumerable<Appointment?> GetById(Guid appId)
        {
            return _requester.GetAppointmentsBy<Guid>("SELECT * FROM Appointment WHERE AppointmentId = @appId", "@appId", appId);
        }

        public IEnumerable<Appointment?> GetByVeterinaryId(Guid id, int scope)
        {
            return _requester.GetAppointmentsBy<Guid>(AppointmentDateScope.SetScope("SELECT * FROM Appointment WHERE VeterinaryId = @id", scope, 1), "@id", id);
        }

        public IEnumerable<Appointment?> GetByOwnerId(Guid id, int scope)
        {
            return _requester.GetAppointmentsBy<Guid>(AppointmentDateScope.SetScope("SELECT * FROM Appointment WHERE AnimalId IN " +
                                                                                                                    "(SELECT AnimalID " +
                                                                                                                    "FROM ClinicAnimal " +
                                                                                                                    "WHERE OwnerId = @id)", scope, 1), "@id", id);
        }

        public IEnumerable<Appointment?> GetByAnimalName(string name, int scope)
        {
            return _requester.GetAppointmentsBy<string>(AppointmentDateScope.SetScope("SELECT * FROM Appointment " +
                                                            "WHERE AnimalId = (SELECT AnimalID " +
                                                                            "FROM ClinicAnimal " +
                                                                            "WHERE AnimalName = @name)", scope, 1), "@name", name);
        }

        public IEnumerable<Appointment?> GetByAnimalId(Guid id, int scope)
        {
            return _requester.GetAppointmentsBy<Guid>(AppointmentDateScope.SetScope("SELECT * FROM Appointment " +
                                                            "WHERE AnimalId = @id", scope, 1), "@id", id);
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

        //******************************************************************************//
        //                                      POST                                    //
        //******************************************************************************//

        public bool Create(Appointment appointment)
        {
            using (SqlConnection connection = new(_connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO Appointment (AppointmentID, " +
                                                                "AppointmentDate, " +
                                                                "AppointmentCreationDate, " +
                                                                "AppointmentUpdateDate, " +
                                                                "DurationMinutes, " +
                                                                "Reason, " +
                                                                "Diagnosis, " +
                                                                "AnimalId, " +
                                                                "VeterinaryId) " +
                                            "VALUES (@appointmentId, " +
                                                    "@appointmentDate, " +
                                                    "@appointmentCreationDate, " +
                                                    "@appointmentUpdateDate, " +
                                                    "@durationMinutes, " +
                                                    "@reason, " +
                                                    "@diagnosis, " +
                                                    "@animalId, " +
                                                    "@veterinaryId)";
                Type bodyType = typeof(Appointment);
                foreach (var prop in bodyType.GetProperties())
                {
                    string propName = prop.Name;
                    object propValue = prop.GetValue(appointment);
                    command.Parameters.AddWithValue("@" + propName, propValue);
                }

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                return rowsAffected > 0;
            }
        }

        //******************************************************************************//
        //                                      PATCH                                   //
        //******************************************************************************//

        public bool Update(Appointment appointment)
        {
            using (SqlConnection connection = new(_connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE Appointment " +
                                        "SET AppointmentDate = @appointmentDate, " +
                                            "AppointmentUpdateDate = @appointmentUpdateDate, " +
                                            "Reason = @reason, " +
                                            "Diagnosis = @diagnosis, " +
                                            "AnimalId = @animalId, " +
                                            "VeterinaryId = @veterinaryId " +

                                        "WHERE AppointmentId = @appointmentId";

                Type bodyType = typeof(Appointment);
                foreach (var prop in bodyType.GetProperties())
                {
                    string propName = prop.Name;
                    object propValue = prop.GetValue(appointment);
                    command.Parameters.AddWithValue("@" + propName, propValue);
                }

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                return rowsAffected > 0;
            }
        }

        //******************************************************************************//
        //                                     DELETE                                   //
        //******************************************************************************//

        public bool Delete(Guid id)
        {
            using (SqlConnection connection = new(_connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM Appointment WHERE AppointmentId = @id";
                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                return rowsAffected > 0;
            }
        }
    }
}
