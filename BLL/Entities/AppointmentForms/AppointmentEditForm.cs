namespace BLL.Entities.AppointmentForms
{
    public class AppointmentEditForm
    {
        public AppointmentEditForm(DateTime appointmentDate, int durationMinutes, string reason, string diagnosis, Guid animalId, Guid veterinaryId)
        {
            AppointmentDate = appointmentDate;
            DurationMinutes = durationMinutes;
            Reason = reason;
            Diagnosis = diagnosis;
            AnimalId = animalId;
            VeterinaryId = veterinaryId;
        }

        [DateRangeFromTodayToTwoYears]
        public DateTime AppointmentDate { get; set; }
        public int? DurationMinutes { get; set; }
        public string? Reason { get; set; }
        public string? Diagnosis { get; set; }
        public Guid? AnimalId { get; set; }
        public Guid? VeterinaryId { get; set; }
    }
}
