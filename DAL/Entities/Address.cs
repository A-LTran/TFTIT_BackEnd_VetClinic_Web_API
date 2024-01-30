namespace DAL.Entities
{
    public class Address
    {
        public Address(string address1, string? address2, string city, string country, string postalCode)
        {
            Address1 = address1;
            Address2 = address2;
            City = city;
            Country = country;
            PostalCode = postalCode;
        }

        public Guid AddressId { get; set; } = Guid.NewGuid();
        public string Address1 { get; set; } = default!;
        public string? Address2 { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string PostalCode { get; set; } = default!;
    }
}
