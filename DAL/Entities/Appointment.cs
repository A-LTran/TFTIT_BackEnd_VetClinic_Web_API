namespace DAL.Entities
{
    public class Appointment
    {
        public Guid AppointmentId { get; set; } = Guid.NewGuid();
        public DateTime AppointmentDate { get; set; }
        public DateTime AppointmentCreationDate { get; set; } = DateTime.Now;
        public string Reason { get; set; } = default!;
        public string Diagnosis { get; set; } = default!;
        public int AnimalId { get; set; }
        public int VeterinaryId { get; set; }
    }
}
