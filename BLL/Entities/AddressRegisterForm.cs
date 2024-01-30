using System.ComponentModel.DataAnnotations;

namespace BLL.Entities
{
    public class AddressRegisterForm
    {
        [Required]
        [MaxLength(120)]
        public string Address1 { get; set; } = default!;
        [MaxLength(120)]
        public string? Address2 { get; set; } = default!;
        [Required]
        [MaxLength(100)]
        public string City { get; set; } = default!;
        [Required]
        [MaxLength(2)]
        public string Country { get; set; } = default!; //Country code
        [Required]
        [MaxLength(16)]
        public string PostalCode { get; set; } = default!;
    }
}
