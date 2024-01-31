using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BLL.Entities
{
    public class UserRegisterForm
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
        public string Phone { get; set; } = "NA";
        [Phone]
        public string Mobile { get; set; } = "NA";
        [DateRangeBeforeTodayAndAfter100Y]
        public DateTime BirthDate { get; set; } = DateTime.Today;
        [Required]
        [PasswordPropertyText]
        public string UserPassword { get; set; } = default!;
        [Compare(nameof(UserPassword))]
        public string ConfirmUserPassword { get; set; } = default!;
        public Role PersonRole { get; set; } = default!;
    }
}
