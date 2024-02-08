using BLL.Entities.AppointmentForms;

namespace BLL.Services
{
    public class AppointmentService_BLL : IAppointmentRepository_BLL
    {
        private readonly IAppointmentRepository_DAL _appointmentService;
        private readonly ToolSet _toolSet;

        private string _message;

        public string Message
        {
            get { return _message; }
            private set { _message = value; }
        }
        public AppointmentService_BLL(IAppointmentRepository_DAL appointmentService)
        {
            _appointmentService = appointmentService;
            _toolSet = new ToolSet(LogMessage);
        }

        private void LogMessage(string message)
        {
            Message = message;
        }

        public string GetMessage()
        {
            return Message;
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
            if (!_toolSet.ObjectExistsCheck(app is not null, "Appointment"))
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
            if (_toolSet.SuccessCheck(GetByAppointmentAvailability(form), "", "", "Cette plage horaire n'est pas disponible."))
                return false;

            if (!_toolSet.SuccessCheck(_appointmentService.Create(form.ToAppointment()), "Appointment", "created"))
                return false;

            return true;
        }

        //*****************************************************************************//
        //                                    PATCH                                    //
        //*****************************************************************************//

        public bool Update(Guid id, AppointmentEditForm form)
        {
            Appointment? currentApp = _appointmentService.GetById(id)!;
            if (!_toolSet.ObjectExistsCheck(currentApp is not null, "Appointment"))
                return false;

            Appointment? newApp = form.ToAppointment(id);

            Type type = typeof(Appointment);
            foreach (var prop in type.GetProperties())
            {
                if (!(prop.GetValue(newApp) is null || prop.GetValue(newApp) == default))
                {
                    prop.SetValue(currentApp, prop.GetValue(newApp));
                }
            }

            if (!_toolSet.SuccessCheck(_appointmentService.Update(currentApp), "Appointment", "updated"))
                return false;

            return true;
        }

        //*****************************************************************************//
        //                                    DELETE                                   //
        //*****************************************************************************//

        public bool Delete(Guid id)
        {
            if (!_toolSet.ObjectExistsCheck(_appointmentService.GetById(id) is not null, "Appointment"))
                return false;
            if (!_toolSet.SuccessCheck(_appointmentService.Delete(id), "Appointment", "deleted"))
                return false;

            return true;
        }
    }
}
