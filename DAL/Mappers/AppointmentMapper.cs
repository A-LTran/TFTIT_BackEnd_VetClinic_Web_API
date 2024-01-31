namespace DAL.Mappers
{
    public static class AppointmentMapper
    {
        public static Appointment ToAppointment(
            this Guid appointmentId,
            DateTime appointementDate,
            DateTime appointmentCreationDate,
            string reason,
            string diagnosis,
            Guid animalId,
            Guid veterinaryId)
        {
            return new Appointment(appointmentId, appointementDate, appointmentCreationDate, reason, diagnosis, animalId, veterinaryId);
        }
    }
}
