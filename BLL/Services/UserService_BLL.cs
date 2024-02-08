using DAL.Entities.Enumerations;
using Microsoft.IdentityModel.Tokens;

namespace BLL.Services
{
    public class UserService_BLL : IUserRepository_BLL
    {
        private readonly IUserRepository_DAL _userService;
        private readonly ToolSet _toolSet;

        private string _message;

        public string Message
        {
            get { return _message; }
            private set { _message = value; }
        }

        public UserService_BLL(IUserRepository_DAL userRepository)
        {
            _userService = userRepository;
            _toolSet = new(LogMessage);
        }

        private void LogMessage(string message)
        {
            Message = message;
        }

        public string GetMessage()
        {
            return Message;
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
                if (_toolSet.ObjectExistsCheck(isActive, "User"))
                    return false;

                if (!_toolSet.ObjectExistsCheck(isActive, "User", "User has been reactivated! Please update your information."))
                    return false;
            }

            if (!_toolSet.ObjectExistsCheck(!form.UserPassword.IsNullOrEmpty(), "Password"))
                return false;

            form.UserPassword = BCrypt.Net.BCrypt.HashPassword(form.UserPassword);

            if (!_toolSet.SuccessCheck(_userService.Create(form.ToUser(addressId)!), "User", "created"))
                return false;

            return true;
        }

        public bool Create(OwnerRegisterForm form, Guid addressId)
        {
            if (_toolSet.ObjectExistsCheck(_userService.GetOwnerByMail(form.Email) is not null, "Owner"))
                return false;

            if (!_toolSet.SuccessCheck(_userService.Create(form.ToOwner(addressId)!), "Owner", "created"))
                return false;

            return true;
        }


        public bool Create(AddressRegisterForm form)
        {
            if (!_toolSet.SuccessCheck(_userService.Create(form.ToAddress()), "Address", "created"))
                return false;

            return true;
        }

        public bool VerifyPassword(UserLoginForm form)
        {
            string? oldPassword = _userService.GetUserPasswordByMail(form.Email);
            if (!_toolSet.ObjectExistsCheck(!oldPassword.IsNullOrEmpty(), "Password"))
                return false;

            if (!_toolSet.SuccessCheck(BCrypt.Net.BCrypt.Verify(form.UserPassword, oldPassword), "Password", "verified"))
                return false;

            return true;
        }

        //*****************************************************************************//
        //                                    GET                                      //
        //*****************************************************************************//

        public IEnumerable<UserDto?> Get()
        {
            foreach (User? u in _userService.Get())
                yield return u?.ToUserForDisplay();
        }
        public IEnumerable<UserDto?> GetPersonsByRole(int role)
        {
            Role personRole = (Role)role;

            foreach (Person person in _userService.GetPersonsByRole(personRole))
                yield return person.ToUserForDisplay();
        }

        public UserDto? GetUserById(Guid userId)
        {
            UserDto? u = _userService.GetUserById(userId).ToUserForDisplay();
            if (!_toolSet.ObjectExistsCheck(u is not null, "User"))
                return null;

            return u;

        }

        public UserDto? GetUserByMail(string mail)
        {
            UserDto? u = _userService.GetUserByMail(mail).ToUserForDisplay();
            if (!_toolSet.ObjectExistsCheck(u is not null, "User"))
                return null;

            return u;
        }

        public string? GetUserPasswordByMail(string mail)
        {
            return _userService.GetUserPasswordByMail(mail);
        }

        public UserTokenDto? GetUserDtoByMail(string mail)
        {
            return _userService.GetUserDtoByMail(mail).ToUserTokenDto();
        }

        // OWNER

        public UserDto? GetOwnerById(Guid ownerId)
        {
            UserDto? o = _userService.GetOwnerById(ownerId).ToUserForDisplay();
            if (!_toolSet.ObjectExistsCheck(o is not null, "Owner"))
                return null;

            return o;
        }

        public UserDto? GetOwnerByMail(string mail)
        {
            UserDto? o = _userService.GetOwnerByMail(mail).ToUserForDisplay();
            if (!_toolSet.ObjectExistsCheck(o is not null, "Owner"))
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
            if (!_toolSet.ObjectExistsCheck(a is not null, "Address"))
                return null;
            return a;
        }

        public bool AddressExistsCheckById(Guid id)
        {
            return _userService.AddressExistsCheckById(id);
        }

        public bool AddressExistsCheckByAddressInfo(AddressRegisterForm form)
        {
            return _userService.GetAddressByAddressInfo(form.ToAddress()).ToList().Count > 0;
        }

        public bool PersonExistsCheckById(Guid id)
        {
            return _userService.PersonExistsCheckById(id);
        }

        public bool PersonExistsCheckByMail(string mail)
        {
            return _userService.PersonExistsCheckByMail(mail);
        }

        //*****************************************************************************//
        //                                    PATCH                                    //
        //*****************************************************************************//

        public bool UpdateUser(UserEditForm form, Guid userId, Role role)
        {
            Address? address = _userService.GetAddressByPersonId(userId);

            if (!_toolSet.ObjectExistsCheck(address is not null, "Address"))
                return false;

            form.UserPassword = BCrypt.Net.BCrypt.HashPassword(form.UserPassword!);

            User? newUser = form.ToUser(userId, address.AddressId, role);
            User currentUser = _userService.GetUserById(userId);

            Type type = typeof(User);
            foreach (var prop in type.GetProperties())
            {
                if (!(prop.GetValue(newUser) is null || prop.GetValue(newUser) == default))
                {
                    prop.SetValue(currentUser, prop.GetValue(newUser));
                }
            }

            if (!_toolSet.SuccessCheck(_userService.Update(currentUser), "User", "updated"))
                return false;

            return true;
        }

        public bool UpdateOwner(OwnerEditForm form, Guid ownerId)
        {
            Address? address = _userService.GetAddressByPersonId(ownerId);

            if (!_toolSet.ObjectExistsCheck(address is not null, "Address"))
                return false;

            Owner? newOwner = form.ToOwner(ownerId, address.AddressId);
            Owner currentOwner = _userService.GetOwnerById(ownerId);

            Type type = typeof(Owner);
            foreach (var prop in type.GetProperties())
            {
                if (!(prop.GetValue(newOwner) is null || prop.GetType() == typeof(string) && prop.GetValue(newOwner) == "empty"))
                {
                    prop.SetValue(currentOwner, prop.GetValue(newOwner));
                }
            }

            if (!_toolSet.SuccessCheck(_userService.Update(currentOwner), "Owner", "updated"))
                return false;

            return true;
        }

        public bool UpdateAddress(AddressEditForm form, Guid addressId)
        {
            Address? newAddress = form.ToAddress(addressId);
            Address currentAddress = _userService.GetAddressById(addressId);

            Type type = typeof(Address);
            foreach (var prop in type.GetProperties())
            {
                if (!(prop.GetValue(newAddress) is null || prop.GetValue(newAddress) == default))
                {
                    prop.SetValue(currentAddress, prop.GetValue(newAddress));
                }
            }

            if (!_toolSet.SuccessCheck(_userService.Update(currentAddress), "Address", "updated"))
                return false;

            return true;
        }

        //*****************************************************************************//
        //                                   DELETE                                    //
        //*****************************************************************************//

        public bool DeleteOwner(Guid ownerId)
        {
            if (!_toolSet.SuccessCheck(_userService.DeleteOwner(ownerId), "Owner", "deleted"))
                return false;

            return true;
        }
        public bool DeleteUser(Guid userId)
        {
            if (!_toolSet.SuccessCheck(_userService.DeleteUser(userId), "User", "deleted"))
                return false;

            return true;
        }

        public bool DeleteAddress(Guid addressId)
        {
            if (!_toolSet.ObjectExistsCheck(_userService.GetAddressById(addressId) is not null, "Address"))
                return false;

            if (!_toolSet.SuccessCheck(_userService.DeleteAddress(addressId), "Address", "deleted"))
                return false;

            return true;
        }
    }
}
