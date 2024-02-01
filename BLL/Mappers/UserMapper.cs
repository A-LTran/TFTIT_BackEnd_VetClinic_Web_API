using BLL.Entities.PersonForms;

namespace BLL.Mappers
{
    internal static class UserMapper
    {
        internal static User? ToUser(this UserRegisterForm form, Guid addressId)
        {
            return new User(form.LastName, form.FirstName, form.Email, form.Phone, form.Mobile, form.BirthDate, form.UserPassword, form.PersonRole, addressId);
        }

        internal static User? ToUser(this UserEditForm form, Guid addressId)
        {
            return new User(form.LastName, form.FirstName, form.Email, form.Phone, form.Mobile, form.BirthDate, form.UserPassword, addressId);
        }

        internal static Owner? ToOwner(this OwnerRegisterForm form, Guid addressId)
        {
            return new Owner(form.LastName, form.FirstName, form.Email, form.Phone, form.Mobile, form.BirthDate, form.PersonRole, addressId);
        }
        internal static Owner? ToOwner(this OwnerEditForm form, Guid addressId)
        {
            return new Owner(form.LastName, form.FirstName, form.Email, form.Phone, form.Mobile, form.BirthDate, addressId);
        }

        internal static Owner? ToOwner(this Person person)
        {
            return new Owner(person.LastName, person.FirstName, person.Email, person.Phone, person.Mobile, person.BirthDate, person.PersonRole, person.AddressId);
        }
    }
}
