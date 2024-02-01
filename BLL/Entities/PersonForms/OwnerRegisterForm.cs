using System.ComponentModel.DataAnnotations;

namespace BLL.Entities.PersonForms
{
    public class OwnerRegisterForm
    {
        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        public string LastName { get; set; } = default!;
        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        public string FirstName { get; set; } = default!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;
        [Phone]
        public string Phone { get; set; } = "0000000000";
        [Phone]
        public string Mobile { get; set; } = "0000000000"!;
        [DateRangeBeforeTodayAndAfter100Y]
        public DateTime BirthDate { get; set; } = DateTime.Today;
        public Role PersonRole { get; set; } = default!;
    }
}
