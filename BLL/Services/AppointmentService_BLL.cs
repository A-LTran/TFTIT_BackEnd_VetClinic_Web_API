using BLL.Entities.AppointmentForms;

namespace BLL.Services
{
    public class AppointmentService_BLL : IAppointmentRepository_BLL
    {
        private readonly IAppointmentRepository_DAL _appointmentService;

        public AppointmentService_BLL(IAppointmentRepository_DAL appointmentService)
        {
            _appointmentService = appointmentService;
        }

        // GET
        public IEnumerable<Appointment?> Get(int scope)
        {
            return _appointmentService.Get(scope);
        }

        public Appointment? GetById(Guid appId)
        {
            List<Appointment> apps = _appointmentService.GetById(appId).ToList();
            return (apps.Count == 1) ? apps[0] : null;
        }

        public IEnumerable<Appointment?> GetByVeterinaryId(Guid vetId, int scope)
        {
            return _appointmentService.GetByVeterinaryId(vetId, scope);
        }

        public IEnumerable<Appointment?> GetByOwnerId(Guid vetId, int scope)
        {
            return _appointmentService.GetByOwnerId(vetId, scope);
        }

        public IEnumerable<Appointment?> GetByAnimalName(string name, int scope)
        {
            return _appointmentService.GetByAnimalName(name, scope);
        }

        public IEnumerable<Appointment?> GetByAnimalId(Guid id, int scope)
        {
            return _appointmentService.GetByAnimalId(id, scope);
        }

        // POST

        public bool GetByAppointmentAvailability(AppointmentRegisterForm form)
        {
            return _appointmentService.GetByAppointmentRange(form.VeterinaryId, form.AppointmentDate, form.DurationMinutes).ToList().Count() > 0;
        }

        public bool Create(AppointmentRegisterForm form)
        {
            return _appointmentService.Create(form.ToAppointment());
        }

        // PATCH

        public bool Update(Guid id, AppointmentEditForm form)
        {
            return _appointmentService.Update(form.ToAppointment(id));
        }

        // DELETE

        public bool Delete(Guid id)
        {
            return _appointmentService.Delete(id);
        }
    }
}
