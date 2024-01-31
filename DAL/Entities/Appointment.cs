namespace DAL.Entities
{
    public class Appointment
    {
        // BLL Form
        public Appointment(DateTime appointmentDate, DateTime appointmentCreationDate, int durationMinutes, string reason, string diagnosis, Guid animalId, Guid veterinaryId)
        {
            AppointmentDate = appointmentDate;
            AppointmentCreationDate = appointmentCreationDate;
            DurationMinutes = durationMinutes;
            Reason = reason;
            Diagnosis = diagnosis;
            AnimalId = animalId;
            VeterinaryId = veterinaryId;
        }

        // DAL Get
        public Appointment(Guid appointmentId, DateTime appointmentDate, DateTime appointmentCreationDate, int durationMinutes, string reason, string diagnosis, Guid animalId, Guid veterinaryId) : this(appointmentDate, appointmentCreationDate, durationMinutes, reason, diagnosis, animalId, veterinaryId)
        {
            AppointmentId = appointmentId;
        }

        public Guid AppointmentId { get; set; } = Guid.NewGuid();
        public DateTime AppointmentDate { get; set; }
        public DateTime AppointmentCreationDate { get; set; } = DateTime.Now;
        public int DurationMinutes { get; set; }
        public string Reason { get; set; } = default!;
        public string Diagnosis { get; set; } = default!;
        public Guid AnimalId { get; set; }
        public Guid VeterinaryId { get; set; }
    }
}
