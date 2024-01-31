namespace DAL.Mappers
{
    public static class AppointmentMapper
    {
        public static Appointment ToAppointment(
            this Guid appointmentId,
            DateTime appointementDate,
            DateTime appointmentCreationDate,
            int DurationMinutes,
            string reason,
            string diagnosis,
            Guid animalId,
            Guid veterinaryId)
        {
            return new Appointment(appointmentId, appointementDate, appointmentCreationDate, DurationMinutes, reason, diagnosis, animalId, veterinaryId);
        }
    }
}
