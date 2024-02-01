namespace BLL.Services
{
    public class AppointmentService_BLL : IAppointmentRepository_BLL
    {
        private readonly IAppointmentRepository_DAL _appointmentService;

        public AppointmentService_BLL(IAppointmentRepository_DAL appointmentService)
        {
            _appointmentService = appointmentService;
        }
        public IEnumerable<Appointment?> Get()
        {
            return _appointmentService.Get();
        }

        public IEnumerable<Appointment?> GetByVeterinaryId(Guid vetId)
        {
            return _appointmentService.GetByVeterinaryId(vetId);
        }
        public IEnumerable<Appointment?> GetByAnimalName(string name)
        {
            return _appointmentService.GetByAnimalName(name);
        }

        public IEnumerable<Appointment?> GetByAnimalId(Guid id)
        {
            return _appointmentService.GetByAnimalId(id);
        }

        public bool GetByAppointmentAvailability(AppointmentRegisterForm form)
        {
            return _appointmentService.GetByAppointmentRange(form.VeterinaryId, form.AppointmentDate, form.DurationMinutes).ToList().Count() > 0;
        }

        public bool Create(AppointmentRegisterForm form)
        {
            return _appointmentService.Create(form.ToAppointment());
        }
    }
}
