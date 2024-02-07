using System.ComponentModel.DataAnnotations;

namespace BLL.Entities.PersonForms
{
    public class OwnerEditForm
    {
        [MaxLength(50)]
        [MinLength(2)]
        public string? LastName { get; set; }
        [MinLength(2)]
        public string? FirstName { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Phone]
        public string? Phone { get; set; }
        [Phone]
        public string? Mobile { get; set; }
        [DateRangeBeforeTodayAndAfter100Y]
        public DateTime BirthDate { get; set; } = default;
    }
}
