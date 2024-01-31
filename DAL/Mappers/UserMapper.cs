namespace DAL.Mappers
{
    internal static class UserMapper
    {
        internal static User? ToUser(this Guid personId, string lastName, string firstName, string email, string phone, string mobile, DateTime birthDate, string userPassword, Role personRole, Guid addressId)
        {

            //if (Convert.IsDBNull(phone))
            //    phone = default!;
            //if (Convert.IsDBNull(mobile))
            //    mobile = default!;
            //if (Convert.IsDBNull(birthDate))
            //    birthDate = default!;

            return new User(personId, lastName, firstName, email, phone, mobile, birthDate, userPassword, personRole, addressId);
        }

        internal static Owner? ToOwner(this Guid personId, string lastName, string firstName, string email, string phone, string mobile, DateTime birthDate, Role personRole, Guid addressId)
        {

            //if (Convert.IsDBNull(phone))
            //    phone = default!;
            //if (Convert.IsDBNull(mobile))
            //    mobile = default!;
            //if (Convert.IsDBNull(birthDate))
            //    birthDate = default!;

            return new Owner(personId, lastName, firstName, email, phone, mobile, birthDate, personRole, addressId);
        }
    }
}
