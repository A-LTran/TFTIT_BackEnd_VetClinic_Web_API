namespace DAL.Entities
{
    public class Appointment
    {
        // BLL Form
        public Appointment(DateTime appointmentDate, int durationMinutes, string reason, string diagnosis, Guid animalId, Guid veterinaryId)
        {
            AppointmentDate = appointmentDate;
            DurationMinutes = durationMinutes;
            Reason = reason;
            Diagnosis = diagnosis;
            AnimalId = animalId;
            VeterinaryId = veterinaryId;
        }
        public Appointment(Guid appointmentId, DateTime appointmentDate, int durationMinutes, string reason, string diagnosis, Guid animalId, Guid veterinaryId) : this(appointmentDate, durationMinutes, reason, diagnosis, animalId, veterinaryId)
        {
            AppointmentId = appointmentId;
        }

        // DAL Get
        public Appointment(Guid appointmentId, DateTime appointmentDate, DateTime appointmentCreationDate, DateTime appointmentUpdateDate, int durationMinutes, string reason, string diagnosis, Guid animalId, Guid veterinaryId) : this(appointmentId, appointmentDate, durationMinutes, reason, diagnosis, animalId, veterinaryId)
        {
            AppointmentCreationDate = appointmentCreationDate;
            AppointmentUpdateDate = appointmentUpdateDate;
        }

        public Guid AppointmentId { get; set; } = Guid.NewGuid();
        public DateTime AppointmentDate { get; set; }
        public DateTime AppointmentCreationDate { get; } = DateTime.Now;
        public DateTime AppointmentUpdateDate { get; set; } = DateTime.Now;
        public int DurationMinutes { get; set; }
        public string Reason { get; set; } = default!;
        public string Diagnosis { get; set; } = default!;
        public Guid AnimalId { get; set; }
        public Guid VeterinaryId { get; set; }
    }
}
