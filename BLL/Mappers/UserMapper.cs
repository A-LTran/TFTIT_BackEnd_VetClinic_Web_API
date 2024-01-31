namespace BLL.Mappers
{
    internal static class UserMapper
    {
        internal static User? ToUser(this UserRegisterForm form, Guid addressId)
        {
            return new User(form.LastName, form.FirstName, form.Email, form.Phone, form.Mobile, form.BirthDate, form.UserPassword, form.PersonRole, addressId);
        }

        internal static Owner? ToOwner(this OwnerRegisterForm form, Guid addressId)
        {
            return new Owner(form.LastName, form.FirstName, form.Email, form.Phone, form.Mobile, form.BirthDate, form.PersonRole, addressId);
        }
    }
}
