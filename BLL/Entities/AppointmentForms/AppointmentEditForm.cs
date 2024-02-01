using System.ComponentModel.DataAnnotations;

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

        [Required]
        [DateRangeFromTodayToTwoYears]
        public DateTime AppointmentDate { get; set; }
        [Required]
        public int DurationMinutes { get; set; } = 30;
        public string Reason { get; set; } = default!;
        public string Diagnosis { get; set; } = "NA";
        public Guid AnimalId { get; set; }
        public Guid VeterinaryId { get; set; }
    }
}
