namespace BLL.Interfaces
{
    public interface IAppointmentRepository_BLL
    {
        public IEnumerable<Appointment?> Get(int scope);
        public IEnumerable<Appointment?> GetByVeterinaryId(Guid vetId, int scope);
        public IEnumerable<Appointment?> GetByOwnerId(Guid vetId, int scope);
        public IEnumerable<Appointment?> GetByAnimalName(string name, int scope);
        public IEnumerable<Appointment?> GetByAnimalId(Guid id, int scope);
        public bool GetByAppointmentAvailability(AppointmentRegisterForm form);
        public bool Create(AppointmentRegisterForm form);
    }
}
