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
                if (ObjectExistsCheck(isActive, "User"))
                    return false;

                if (!ObjectExistsCheck(isActive, "User", "User has been reactivated! Please update your information."))
                    return false;
            }

            if (!ObjectExistsCheck(!form.UserPassword.IsNullOrEmpty(), "Password"))
                return false;

            form.UserPassword = BCrypt.Net.BCrypt.HashPassword(form.UserPassword);

            if (!SucceessCheck(_userService.Create(form.ToUser(addressId)!), "User", "created"))
                return false;

            return true;
        }

        public bool Create(OwnerRegisterForm form, Guid addressId)
        {
            if (ObjectExistsCheck(_userService.GetOwnerByMail(form.Email) is not null, "Owner"))
                return false;

            if (!SucceessCheck(_userService.Create(form.ToOwner(addressId)!), "Owner", "created"))
                return false;

            return true;
        }


        public bool Create(AddressRegisterForm form)
        {
            if (!SucceessCheck(_userService.Create(form.ToAddress()), "Address", "created"))
                return false;

            return true;
        }

        public bool VerifyPassword(UserLoginForm form)
        {
            string oldPassword = _userService.GetUserPasswordByMail(form.Email);
            if (oldPassword.IsNullOrEmpty())
                return false;
            return BCrypt.Net.BCrypt.Verify(form.UserPassword, oldPassword) ? true : false;
        }

        //*****************************************************************************//
        //                                    GET                                      //
        //*****************************************************************************//

        public IEnumerable<UserDto?> Get()
        {
            foreach (User? u in _userService.Get())
                yield return u?.ToUserForDisplay();
        }
        public IEnumerable<UserDto> GetPersonsByRole(int role)
        {
            Role personRole = (Role)role;

            foreach (Person person in _userService.GetPersonsByRole(personRole))
                yield return person.ToUserForDisplay();
        }

        public UserDto? GetUserById(Guid userId)
        {
            UserDto? u = _userService.GetUserById(userId).ToUserForDisplay();
            if (!ObjectExistsCheck(u is not null, "User"))
                return null;

            return u;

        }

        public UserDto? GetUserByMail(string mail)
        {
            UserDto? u = _userService.GetUserByMail(mail).ToUserForDisplay();
            if (!ObjectExistsCheck(u is not null, "User"))
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
            if (!ObjectExistsCheck(o is not null, "Owner"))
                return null;

            return o;
        }

        public UserDto? GetOwnerByMail(string mail)
        {
            UserDto? o = _userService.GetOwnerByMail(mail).ToUserForDisplay();
            if (!ObjectExistsCheck(o is not null, "Owner"))
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
            if (!ObjectExistsCheck(a is not null, "Address"))
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

            if (!ObjectExistsCheck(address is not null, "Address"))
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

            if (!SucceessCheck(_userService.Update(currentUser), "User", "updated"))
                return false;

            return true;
        }

        public bool UpdateOwner(OwnerEditForm form, Guid ownerId)
        {
            Address? address = _userService.GetAddressByPersonId(ownerId);

            if (!ObjectExistsCheck(address is not null, "Address"))
                return false;

            Owner? newOwner = form.ToOwner(ownerId, address.AddressId);
            Owner currentOwner = _userService.GetOwnerById(ownerId);

            Type type = typeof(Owner);
            foreach (var prop in type.GetProperties())
            {
                if (!(prop.GetValue(newOwner) is null || prop.GetValue(newOwner) == default))
                {
                    prop.SetValue(currentOwner, prop.GetValue(newOwner));
                }
            }

            if (!SucceessCheck(_userService.Update(currentOwner), "Owner", "updated"))
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

            if (!SucceessCheck(_userService.Update(currentAddress), "Address", "updated"))
                return false;

            return true;
        }

        //*****************************************************************************//
        //                                   DELETE                                    //
        //*****************************************************************************//

        public bool DeleteOwner(Guid ownerId)
        {
            if (!SucceessCheck(_userService.DeleteOwner(ownerId), "Owner", "deleted"))
                return false;

            return true;
        }
        public bool DeleteUser(Guid userId)
        {
            if (!SucceessCheck(_userService.DeleteUser(userId), "User", "deleted"))
                return false;

            return true;
        }

        public bool DeleteAddress(Guid addressId)
        {
            if (!ObjectExistsCheck(_userService.GetAddressById(addressId) is not null, "Address"))
                return false;

            if (!SucceessCheck(_userService.DeleteAddress(addressId), "Address", "deleted"))
                return false;

            return true;
        }
    }
}
