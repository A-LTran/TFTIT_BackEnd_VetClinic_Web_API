namespace DAL.Entities
{
    public class Appointment
    {
        // BLL Form
        public Appointment(DateTime appointmentDate, DateTime appointmentCreationDate, string reason, string diagnosis, Guid animalId, Guid veterinaryId)
        {
            AppointmentDate = appointmentDate;
            AppointmentCreationDate = appointmentCreationDate;
            Reason = reason;
            Diagnosis = diagnosis;
            AnimalId = animalId;
            VeterinaryId = veterinaryId;
        }

        // DAL Get
        public Appointment(Guid appointmentId, DateTime appointmentDate, DateTime appointmentCreationDate, string reason, string diagnosis, Guid animalId, Guid veterinaryId) : this(appointmentDate, appointmentCreationDate, reason, diagnosis, animalId, veterinaryId)
        {
            AppointmentId = AppointmentId;
        }

        public Guid AppointmentId { get; set; } = Guid.NewGuid();
        public DateTime AppointmentDate { get; set; }
        public DateTime AppointmentCreationDate { get; set; } = DateTime.Now;
        public string Reason { get; set; } = default!;
        public string Diagnosis { get; set; } = default!;
        public Guid AnimalId { get; set; }
        public Guid VeterinaryId { get; set; }
    }
}
