using System.ComponentModel.DataAnnotations;

namespace BLL.Entities
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
        public string Phone { get; set; } = default!;
        [Phone]
        public string Mobile { get; set; } = default!;
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public Role UserRole { get; set; } = default!;
    }
}
