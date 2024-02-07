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

        //*****************************************************************************//
        //                                    GET                                      //
        //*****************************************************************************//

        public IEnumerable<Appointment?> Get(int scope)
        {
            return _appointmentService.Get(scope);
        }

        public Appointment? GetById(Guid appId)
        {
            Appointment? app = _appointmentService.GetById(appId);
            if (!ObjectExistsCheck(app is not null, "Appointment"))
                return null;

            return app;
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

        //*****************************************************************************//
        //                                    POST                                     //
        //*****************************************************************************//

        public bool GetByAppointmentAvailability(AppointmentRegisterForm form)
        {
            return _appointmentService.GetByAppointmentRange(form.VeterinaryId, form.AppointmentDate, form.DurationMinutes).ToList().Count() > 0;
        }

        public bool Create(AppointmentRegisterForm form)
        {
            if (SucceessCheck(GetByAppointmentAvailability(form), "", "", "Cette plage horaire n'est pas disponible."))
                return false;

            if (!SucceessCheck(_appointmentService.Create(form.ToAppointment()), "Appointment", "created"))
                return false;

            return true;
        }

        //*****************************************************************************//
        //                                    PATCH                                    //
        //*****************************************************************************//

        public bool Update(Guid id, AppointmentEditForm form)
        {
            if (!ObjectExistsCheck(_appointmentService.GetById(id) is not null, "Appointment"))
                return false;

            if (!SucceessCheck(_appointmentService.Update(form.ToAppointment(id)), "Appointment", "updated"))
                return false;

            return true;
        }

        //*****************************************************************************//
        //                                    DELETE                                   //
        //*****************************************************************************//

        public bool Delete(Guid id)
        {
            if (!ObjectExistsCheck(_appointmentService.GetById(id) is not null, "Appointment"))
                return false;
            if (!SucceessCheck(_appointmentService.Delete(id), "Appointment", "deleted"))
                return false;

            return true;
        }
    }
}
