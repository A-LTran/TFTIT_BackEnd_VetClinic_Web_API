using System.ComponentModel.DataAnnotations;

namespace BLL.Entities.AddressForms
{
    public class AddressEditForm
    {
        [MaxLength(120)]
        public string Address1 { get; set; } = default!;
        [MaxLength(120)]
        public string? Address2 { get; set; } = "NA";
        [MaxLength(100)]
        public string City { get; set; } = default!;
        [MinLength(2)]
        [MaxLength(2)]
        public string Country { get; set; } = default!; //Country code
        [MaxLength(16)]
        public string PostalCode { get; set; } = default!;
    }
}
