using System.ComponentModel.DataAnnotations;

namespace BLL.Entities
{
    public class AnimalRegisterForm
    {
        [Required]
        [MaxLength(50)]
        public string AnimalName { get; set; } = default!;
        [Required]
        [MaxLength(50)]
        public string Breed { get; set; } = default!;
        [Required]
        [DateRangeBeforeTodayAndAfter100Y]
        public DateTime BirthDate { get; set; }
        public Guid OwnerId { get; set; }
    }
}
