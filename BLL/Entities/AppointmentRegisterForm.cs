using System.ComponentModel.DataAnnotations;

namespace BLL.Entities
{
    public class AppointmentRegisterForm
    {
        [Required]
        [DateRangeFromTodayToTwoYears]
        public DateTime AppointmentDate { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime AppointmentCreationDate { get; set; } = DateTime.Now;
        [Required]
        public int DurationMinutes { get; set; } = 30;
        public string Reason { get; set; } = default!;
        public string Diagnosis { get; set; } = "NA";
        public Guid AnimalId { get; set; }
        public Guid VeterinaryId { get; set; }
    }
}
