using System.ComponentModel.DataAnnotations;

namespace BLL.Entities.AddressForms
{
    public class AddressRegisterForm
    {
        [Required]
        [MaxLength(120)]
        public string Address1 { get; set; } = default!;
        [MaxLength(120)]
        public string? Address2 { get; set; } = "NA";
        [Required]
        [MaxLength(100)]
        public string City { get; set; } = default!;
        [Required]
        [MinLength(2)]
        [MaxLength(2)]
        public string Country { get; set; } = default!; //Country code
        [Required]
        [MaxLength(16)]
        public string PostalCode { get; set; } = default!;
    }
}
