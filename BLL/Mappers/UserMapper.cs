using BLL.Entities.PersonForms;

namespace BLL.Mappers
{
    internal static class UserMapper
    {
        internal static User? ToUser(this UserRegisterForm form, Guid addressId)
        {
            return new User(form.LastName, form.FirstName, form.Email, form.Phone, form.Mobile, form.BirthDate, form.UserPassword, form.PersonRole, addressId);
        }

        internal static User? ToUser(this UserEditForm form, Guid addressId, Role role)
        {
            return new User(form.LastName, form.FirstName, form.Email, form.Phone, form.Mobile, form.BirthDate, form.UserPassword, role, addressId);
        }

        internal static Owner? ToOwner(this OwnerRegisterForm form, Guid addressId)
        {
            return new Owner(form.LastName, form.FirstName, form.Email, form.Phone, form.Mobile, form.BirthDate, form.PersonRole, addressId);
        }
        internal static Owner? ToOwner(this OwnerEditForm form, Guid ownerId, Guid addressId)
        {
            return new Owner(ownerId, form.LastName, form.FirstName, form.Email, form.Phone, form.Mobile, form.BirthDate, Role.Owner, addressId);
        }

        internal static Owner? ToOwner(this Person person)
        {
            return new Owner(person.LastName, person.FirstName, person.Email, person.Phone, person.Mobile, person.BirthDate, person.PersonRole, person.AddressId);
        }
    }
}
