using System.ComponentModel.DataAnnotations;

namespace BLL.Entities.AnimalForms
{
    public class AnimalEditForm
    {
        [MaxLength(50)]
        public string AnimalName { get; set; } = default!;
        [MaxLength(50)]
        public string Breed { get; set; } = default!;
        [DateRangeBeforeTodayAndAfter100Y]
        public DateTime BirthDate { get; set; }
        public Guid OwnerId { get; set; }
    }
}
