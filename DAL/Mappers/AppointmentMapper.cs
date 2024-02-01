namespace DAL.Mappers
{
    public static class AppointmentMapper
    {
        public static Appointment ToAppointment(
            this Guid appointmentId,
            DateTime appointmentDate,
            DateTime appointmentCreationDate,
            DateTime appointmentUpdateDate,
            int DurationMinutes,
            string reason,
            string diagnosis,
            Guid animalId,
            Guid veterinaryId)
        {
            return new Appointment(appointmentId,
                                    appointmentDate,
                                    appointmentCreationDate,
                                    appointmentUpdateDate,
                                    DurationMinutes,
                                    reason,
                                    diagnosis,
                                    animalId,
                                    veterinaryId);
        }
    }
}
