namespace BLL.Mappers
{
    public static class AppointmentMapper
    {
        public static Appointment ToAppointment(this AppointmentRegisterForm form)
        {
            return new Appointment(form.AppointmentDate, form.AppointmentCreationDate, form.Reason, form.Diagnosis, form.AnimalId, form.VeterinaryId);
        }
    }
}
