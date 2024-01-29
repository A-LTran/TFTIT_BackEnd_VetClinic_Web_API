namespace DAL.Mappers
{
    internal static class UserMapper
    {
        internal static User? ToUser(this Guid userId, string lastName, string firstName, string email, string phone, string mobile, DateTime birthDate, string userPassword, Guid addressId)
        {
            //if (!Enum.TryParse(userRole, out Role role))
            //{
            //    role = Role.Anonymous;
            //}
            if (Convert.IsDBNull(phone))
                phone = default!;
            if (Convert.IsDBNull(mobile))
                mobile = default!;
            if (Convert.IsDBNull(birthDate))
                birthDate = default!;

            return new User(userId, lastName, firstName, email, phone, mobile, birthDate, userPassword, addressId);
        }

        internal static Owner? ToOwner(this Guid userId, string lastName, string firstName, string email, string phone, string mobile, DateTime birthDate, Guid addressId)
        {
            //    if (!Enum.TryParse(userRole, out Role role))
            //    {
            //        role = Role.Anonymous;
            //    }
            if (Convert.IsDBNull(phone))
                phone = default!;
            if (Convert.IsDBNull(mobile))
                mobile = default!;
            if (Convert.IsDBNull(birthDate))
                birthDate = default!;

            return new Owner(userId, lastName, firstName, email, phone, mobile, birthDate, addressId);
        }
    }
}
