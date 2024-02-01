namespace DAL.Interfaces
{
    public interface IAppointmentRepository_DAL
    {
        public IEnumerable<Appointment?> Get();
        public IEnumerable<Appointment?> GetByVeterinaryId(Guid id);
        public IEnumerable<Appointment?> GetByAnimalName(string name);
        public IEnumerable<Appointment?> GetByAnimalId(Guid id);
        public IEnumerable<Appointment?> GetByAppointmentRange(Guid vetId, DateTime date, int durationMinutes = 30);
        public bool Create(Appointment appointment);
    }
}
