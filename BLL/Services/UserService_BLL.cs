using DAL.Entities.Enumerations;
using Microsoft.IdentityModel.Tokens;

namespace BLL.Services
{
    public class UserService_BLL : IUserRepository_BLL
    {

        private readonly IUserRepository_DAL _userService;

        public UserService_BLL(IUserRepository_DAL userRepository)
        {
            _userService = userRepository;
        }

        //*****************************************************************************//
        //                                    POST                                     //
        //*****************************************************************************//

        public bool Create(UserRegisterForm form, Guid addressId)
        {
            User? u = _userService.GetUserByMail(form.Email);

            if (u is not null)
            {
                bool isActive = _userService.GetIsActiveByMail(u.Email);
                if (ToolSet.ObjectExistsCheck(isActive, "User"))
                    return false;

                if (!ToolSet.ObjectExistsCheck(isActive, "User", "User has been reactivated! Please update your information."))
                    return false;
            }

            if (!ToolSet.ObjectExistsCheck(!form.UserPassword.IsNullOrEmpty(), "Password"))
                return false;

            form.UserPassword = BCrypt.Net.BCrypt.HashPassword(form.UserPassword);

            if (!ToolSet.SucceessCheck(_userService.Create(form.ToUser(addressId)!), "User", "created"))
                return false;

            return true;
        }

        public bool Create(OwnerRegisterForm form, Guid addressId)
        {
            if (ToolSet.ObjectExistsCheck(_userService.GetOwnerByMail(form.Email) is not null, "Owner"))
                return false;

            if (!ToolSet.SucceessCheck(_userService.Create(form.ToOwner(addressId)!), "Owner", "created"))
                return false;

            return true;
        }


        public bool Create(AddressRegisterForm form)
        {
            List<Address?> addresses = _userService.GetAddressByAddressInfo(form.ToAddress()).ToList();
            if (!ToolSet.ObjectExistsCheck(addresses.Count > 0, "Address"))
                return false;

            if (!ToolSet.SucceessCheck(_userService.Create(form.ToAddress()), "Address", "created"))
                return false;

            return true;
        }

        public User? Login(UserLoginForm form)
        {
            User? u = _userService.GetUserByMail(form.Email);
            if (u is null)
                return null;
            return BCrypt.Net.BCrypt.Verify(form.UserPassword, u.UserPassword) ? u : null;
        }

        //*****************************************************************************//
        //                                    GET                                      //
        //*****************************************************************************//

        public IEnumerable<UserForDisplay?> Get()
        {
            foreach (User? u in _userService.Get())
                yield return u?.ToUserForDisplay();
        }
        public IEnumerable<UserForDisplay> GetPersonsByRole(int role)
        {
            Role personRole = (Role)role;

            foreach (Person person in _userService.GetPersonsByRole(personRole))
                yield return person.ToUserForDisplay();
        }

        public UserForDisplay? GetUserById(Guid userId)
        {
            UserForDisplay? u = _userService.GetUserById(userId).ToUserForDisplay();
            if (!ToolSet.ObjectExistsCheck(u is not null, "User"))
                return null;

            return u;

        }

        public UserForDisplay? GetUserByMail(string mail)
        {
            UserForDisplay? u = _userService.GetUserByMail(mail).ToUserForDisplay();
            if (!ToolSet.ObjectExistsCheck(u is not null, "User"))
                return null;

            return u;
        }

        public UserForDisplay? GetOwnerById(Guid ownerId)
        {
            UserForDisplay? o = _userService.GetOwnerById(ownerId).ToUserForDisplay();
            if (!ToolSet.ObjectExistsCheck(o is not null, "Owner"))
                return null;

            return o;
        }

        public UserForDisplay? GetOwnerByMail(string mail)
        {
            UserForDisplay? o = _userService.GetOwnerByMail(mail).ToUserForDisplay();
            if (!ToolSet.ObjectExistsCheck(o is not null, "Owner"))
                return null;

            return o;
        }

        public IEnumerable<Address> GetAddresses()
        {
            return _userService.GetAddresses()!;
        }

        public Address? GetAddressByPersonId(Guid personId)
        {
            Address? a = _userService.GetAddressByPersonId(personId);
            if (!ToolSet.ObjectExistsCheck(a is not null, "Address"))
                return null;
            return a;
        }

        //*****************************************************************************//
        //                                    PATCH                                    //
        //*****************************************************************************//

        public bool UpdateUser(UserEditForm form, Guid userId, Role role)
        {
            if (!ToolSet.ObjectExistsCheck(_userService.GetUserById(userId) is not null, "User"))
                return false;

            Address? address = _userService.GetAddressByPersonId(userId);

            if (!ToolSet.ObjectExistsCheck(address is not null, "Address"))
                return false;

            form.UserPassword = BCrypt.Net.BCrypt.HashPassword(form.UserPassword!);

            if (!ToolSet.SucceessCheck(_userService.Update(form.ToUser(userId, address.AddressId, role)!), "User", "updated"))
                return false;

            return true;
        }

        public bool UpdateOwner(OwnerEditForm form, Guid ownerId)
        {
            if (!ToolSet.ObjectExistsCheck(_userService.GetUserById(ownerId) is not null, "Owner"))
                return false;

            Address? address = _userService.GetAddressByPersonId(ownerId);

            if (!ToolSet.ObjectExistsCheck(address is not null, "Address"))
                return false;

            if (!ToolSet.SucceessCheck(_userService.Update(form.ToOwner(ownerId, address.AddressId)!), "Owner", "updated"))
                return false;

            return true;
        }

        public bool UpdateAddress(AddressEditForm form, Guid addressId)
        {
            if (!ToolSet.ObjectExistsCheck(_userService.GetAddressById(addressId) is not null, "Address"))
                return false;

            if (!ToolSet.SucceessCheck(_userService.Update(form.ToAddress(addressId)), "Address", "updated"))
                return false;

            return true;
        }

        //*****************************************************************************//
        //                                   DELETE                                    //
        //*****************************************************************************//

        public bool DeleteOwner(Guid ownerId)
        {
            if (!ToolSet.ObjectExistsCheck(_userService.GetOwnerById(ownerId) is not null, "Owner"))
                return false;

            if (!ToolSet.SucceessCheck(_userService.DeleteOwner(ownerId), "Owner", "deleted"))
                return false;

            return true;
        }
        public bool DeleteUser(Guid userId)
        {
            if (!ToolSet.ObjectExistsCheck(_userService.GetUserById(userId) is not null, "User"))
                return false;

            if (!ToolSet.SucceessCheck(_userService.DeleteUser(userId), "User", "deleted"))
                return false;

            return true;
        }

        public bool DeleteAddress(Guid addressId)
        {
            if (!ToolSet.ObjectExistsCheck(_userService.GetAddressById(addressId) is not null, "Address"))
                return false;

            if (!ToolSet.SucceessCheck(_userService.DeleteAddress(addressId), "Address", "deleted"))
                return false;

            return true;
        }
    }
}
