namespace DAL.Entities
{
    internal class Address
    {
        internal Guid AddressId { get; set; } = Guid.NewGuid();
        internal string Address1 { get; set; } = default!;
        internal string? Address2 { get; set; } = default!;
        internal string City { get; set; } = default!;
        internal string Country { get; set; } = default!;
        internal string PostalCode { get; set; } = default!;
    }
}
