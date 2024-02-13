namespace DAL.Services
{
    public class AppointmentService_DAL : IAppointmentRepository_DAL
    {
        //private AppointmentRequester _requester;
        private readonly MainRequester _mainRequester;

        public AppointmentService_DAL(string connectionString)
        {
            //_requester = new AppointmentRequester(connectionString);
            _mainRequester = new MainRequester(connectionString);
        }

        //******************************************************************************//
        //                                      GET                                     //
        //******************************************************************************//

        public IEnumerable<Appointment?> Get(int scope)
        {
            return _mainRequester.GetEnumTResult<Appointment, string>(AppointmentDateScope.SetScope("SELECT * FROM Appointment", scope, -1), "");

        }

        public Appointment? GetById(Guid appId)
        {
            return _mainRequester.GetTResult<Appointment, Guid>("SELECT * FROM Appointment " +
                                                                    "WHERE AppointmentId = @appId", "appId", appId);
        }

        public IEnumerable<Appointment?> GetByVeterinaryId(Guid id, int scope)
        {
            return _mainRequester.GetEnumTResult<Appointment, Guid>(AppointmentDateScope.SetScope("SELECT * FROM Appointment " +
                                                                                                    "WHERE VeterinaryId = @id", scope, 1), id, "id");
        }

        public IEnumerable<Appointment?> GetByOwnerId(Guid id, int scope)
        {
            return _mainRequester.GetEnumTResult<Appointment, Guid>(AppointmentDateScope.SetScope("SELECT * FROM Appointment " +
                                                                                                "WHERE AnimalId IN " +
                                                                                                    "(SELECT AnimalID " +
                                                                                                    "FROM ClinicAnimal " +
                                                                                                    "WHERE OwnerId = @id)", scope, 1), id, "id");
        }

        public IEnumerable<Appointment?> GetByAnimalName(string name, int scope)
        {
            return _mainRequester.GetEnumTResult<Appointment, string>(AppointmentDateScope.SetScope("SELECT * FROM Appointment " +
                                                                                        "WHERE AnimalId = (SELECT AnimalID " +
                                                                                            "FROM ClinicAnimal " +
                                                                                            "WHERE AnimalName = @name)", scope, 1), name, "name");
        }

        public IEnumerable<Appointment?> GetByAnimalId(Guid id, int scope)
        {
            return _mainRequester.GetEnumTResult<Appointment, Guid>(AppointmentDateScope.SetScope("SELECT * FROM Appointment " +
                                                                                    "WHERE AnimalId = @id", scope, 1), id, "id");
        }

        public IEnumerable<Appointment?> GetByAppointmentRange(Guid vetId, DateTime date, int durationMinutes = 30)
        {
            string sql = "SELECT * FROM Appointment " +
                            "WHERE VeterinaryId = \'" + vetId + "\' AND (AppointmentDate " +
                            "BETWEEN @appointmentDate and DATEADD(minute, " + durationMinutes + ", @appointmentDate) " +
                            "OR DATEADD(minute, DurationMinutes, AppointmentDate) " +
                            "BETWEEN @appointmentDate and DATEADD(minute, " + durationMinutes + ", @appointmentDate))";

            return _mainRequester.GetEnumTResult<Appointment, DateTime>(sql, date, "appointmentDate");
        }

        //******************************************************************************//
        //                                      POST                                    //
        //******************************************************************************//

        public bool Create(Appointment appointment)
        {
            return _mainRequester.Create("INSERT INTO Appointment (AppointmentID" +
                                                                ", AppointmentDate" +
                                                                ", AppointmentCreationDate" +
                                                                ", AppointmentUpdateDate" +
                                                                ", DurationMinutes" +
                                                                ", Reason" +
                                                                ", Diagnosis" +
                                                                ", AnimalId" +
                                                                ", VeterinaryId) " +
                                            "VALUES (@appointmentId" +
                                                    ", @appointmentDate" +
                                                    ", @appointmentCreationDate" +
                                                    ", @appointmentUpdateDate" +
                                                    ", @durationMinutes" +
                                                    ", @reason" +
                                                    ", @diagnosis" +
                                                    ", @animalId" +
                                                    ", @veterinaryId)", appointment);
        }

        //******************************************************************************//
        //                                      PATCH                                   //
        //******************************************************************************//

        public bool Update(Appointment appointment)
        {
            return _mainRequester.Update<Appointment>("UPDATE Appointment " +
                                                        "SET AppointmentDate = @appointmentDate" +
                                                            ", AppointmentUpdateDate = @appointmentUpdateDate" +
                                                            ", DurationMinutes = @durationMinutes" +
                                                            ", Reason = @reason" +
                                                            ", Diagnosis = @diagnosis" +
                                                            ", AnimalId = @animalId" +
                                                            ", VeterinaryId = @veterinaryId " +

                                                        "WHERE AppointmentId = @appointmentId", appointment);
        }

        //******************************************************************************//
        //                                     DELETE                                   //
        //******************************************************************************//

        public bool Delete(Guid id)
        {
            return _mainRequester.Delete("DELETE FROM Appointment WHERE AppointmentId = @id", "id", id);
        }
    }
}
