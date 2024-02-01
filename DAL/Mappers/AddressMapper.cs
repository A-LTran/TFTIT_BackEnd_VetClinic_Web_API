namespace DAL.Mappers
{
    public static class AddressMapper
    {
        public static Address ToAddress(this Guid addressId, string address1, string address2, string city, string country, string postalCode)
        {
            return new Address(addressId, address1, address2, city, country, postalCode);
        }
    }
}
