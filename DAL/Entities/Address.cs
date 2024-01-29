namespace DAL.Entities
{
    public class Address
    {
        public Guid AddressId { get; set; } = Guid.NewGuid();
        public string Address1 { get; set; } = default!;
        public string? Address2 { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string PostalCode { get; set; } = default!;
    }
}
