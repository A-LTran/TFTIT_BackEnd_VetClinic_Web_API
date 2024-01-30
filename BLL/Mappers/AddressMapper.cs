namespace BLL.Mappers
{
    public static class AddressMapper
    {
        public static Address ToAddress(this AddressRegisterForm form)
        {
            return new Address(form.Address1, form.Address2, form.City, form.Country, form.PostalCode);
        }
    }
}
