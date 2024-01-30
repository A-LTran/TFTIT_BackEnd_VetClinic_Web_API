namespace BLL.Mappers
{
    internal static class UserMapper
    {
        internal static User? ToUser(this UserRegisterForm form)
        {
            return new User(form.LastName, form.FirstName, form.Email, form.Phone, form.Mobile, form.BirthDate, form.UserPassword, form.UserRole);
        }

        internal static Owner? ToOwner(this OwnerRegisterForm form)
        {
            return new Owner(form.LastName, form.FirstName, form.Email, form.Phone, form.Mobile, form.BirthDate, form.UserRole);
        }
    }
}
