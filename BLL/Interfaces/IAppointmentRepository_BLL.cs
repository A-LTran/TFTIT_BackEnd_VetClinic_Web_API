namespace BLL.Interfaces
{
    public interface IAppointmentRepository_BLL
    {
        public IEnumerable<Appointment?> Get();
        public IEnumerable<Appointment?> GetByVeterinaryId(Guid vetId);
        public IEnumerable<Appointment?> GetByDogName(string name);
        public bool GetByAppointmentAvailability(AppointmentRegisterForm form);
        public bool Create(AppointmentRegisterForm form);
    }
}
