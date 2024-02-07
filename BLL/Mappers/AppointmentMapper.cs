using BLL.Entities.AppointmentForms;

namespace BLL.Mappers
{
    public static class AppointmentMapper
    {
        public static Appointment ToAppointment(this AppointmentEditForm form, Guid id)
        {
            return new Appointment(id, form.AppointmentDate, form.DurationMinutes == null ? default : (int)form.DurationMinutes, form.Reason, form.Diagnosis, form.AnimalId == null ? default : (Guid)form.AnimalId, form.VeterinaryId == null ? default : (Guid)form.VeterinaryId);
        }

        public static Appointment ToAppointment(this AppointmentRegisterForm form)
        {
            return new Appointment(form.AppointmentDate, form.DurationMinutes, form.Reason, form.Diagnosis, form.AnimalId, form.VeterinaryId);
        }

        public static AppointmentEditForm ToEditForm(this Appointment appointment)
        {
            if (appointment == null)
                return null;

            return new AppointmentEditForm(appointment.AppointmentDate,
                                            appointment.DurationMinutes,
                                            appointment.Reason,
                                            appointment.Diagnosis,
                                            appointment.AnimalId,
                                            appointment.VeterinaryId);
        }
    }
}
