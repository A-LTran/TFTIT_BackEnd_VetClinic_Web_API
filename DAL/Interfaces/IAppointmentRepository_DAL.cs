namespace DAL.Interfaces
{
    public interface IAppointmentRepository_DAL
    {
        public IEnumerable<Appointment?> Get(int scope);
        public IEnumerable<Appointment?> GetById(Guid appId);
        public IEnumerable<Appointment?> GetByVeterinaryId(Guid id, int scope);
        public IEnumerable<Appointment?> GetByOwnerId(Guid id, int scope);
        public IEnumerable<Appointment?> GetByAnimalName(string name, int scope);
        public IEnumerable<Appointment?> GetByAnimalId(Guid id, int scope);
        public IEnumerable<Appointment?> GetByAppointmentRange(Guid vetId, DateTime date, int durationMinutes = 30);
        public bool Create(Appointment appointment);
        public bool Update(Appointment appointment);
        public bool Delete(Guid id);
    }
}
