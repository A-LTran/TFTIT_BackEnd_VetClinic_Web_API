namespace BLL.Entities.AppointmentForms
{
    public class AppointmentRegisterForm
    {
        [DateRangeFromTodayToTwoYears]
        public DateTime AppointmentDate { get; set; }
        public int DurationMinutes { get; set; }
        public string Reason { get; set; }
        public string Diagnosis { get; set; }
        public Guid AnimalId { get; set; }
        public Guid VeterinaryId { get; set; }
    }
}
