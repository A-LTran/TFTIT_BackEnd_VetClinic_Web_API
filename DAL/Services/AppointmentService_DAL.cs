namespace DAL.Services
{
    public class AppointmentService_DAL : IAppointmentRepository_DAL
    {
        private readonly string _connectionString;

        public AppointmentService_DAL(string connectionString)
        {
            _connectionString = connectionString;
        }
    }
}
