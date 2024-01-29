namespace DAL.Entities
{
    internal class Appointment
    {
        internal Guid AppointmentId { get; set; } = Guid.NewGuid();
        internal DateTime AppointmentDate { get; set; }
        internal DateTime AppointmentCreationDate { get; set; } = DateTime.Now;
        internal string Reason { get; set; } = default!;
        internal string Diagnosis { get; set; } = default!;

    }
}
