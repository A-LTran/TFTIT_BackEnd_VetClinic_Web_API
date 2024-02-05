using DAL.Entities.Enumerations;

namespace BLL.Mappers
{
    internal static class UserMapper
    {
        // USER

        internal static User? ToUser(this UserRegisterForm form, Guid addressId)
        {
            return new User(form.LastName, form.FirstName, form.Email, form.Phone, form.Mobile, form.BirthDate, form.UserPassword, form.PersonRole, addressId);
        }

        internal static User? ToUser(this UserEditForm form, Guid addressId, Role role)
        {
            return new User(form.LastName, form.FirstName, form.Email, form.Phone, form.Mobile, form.BirthDate, form.UserPassword, role, addressId);
        }

        internal static User? ToUser(this UserEditForm form, Guid userId, Guid addressId, Role role)
        {
            return new User(userId, form.LastName, form.FirstName, form.Email, form.Phone, form.Mobile, form.BirthDate, form.UserPassword, role, addressId);
        }

        // OWNER

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
            if (person == null)
                return null;

            return new Owner(person.LastName, person.FirstName, person.Email, person.Phone, person.Mobile, person.BirthDate, person.PersonRole, person.AddressId);
        }

        // USER TO DISPLAY

        internal static UserForDisplay ToUserForDisplay(this User user)
        {
            if (user == null)
                return null;
            return new UserForDisplay(user.PersonId, user.LastName, user.FirstName, user.Email, user.Phone, user.Mobile, user.BirthDate);
        }

        internal static UserForDisplay ToUserForDisplay(this Owner owner)
        {
            if (owner == null)
                return null;
            return new UserForDisplay(owner.PersonId, owner.LastName, owner.FirstName, owner.Email, owner.Phone, owner.Mobile, owner.BirthDate);
        }

        internal static UserForDisplay ToUserForDisplay(this Person person)
        {
            if (person == null)
                return null;
            return new UserForDisplay(person.PersonId, person.LastName, person.FirstName, person.Email, person.Phone, person.Mobile, person.BirthDate);
        }
    }
}
