using BLL.Entities.PersonForms;
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
                return (_userService.GetIsActiveByMail(u.Email)) ? false : _userService.SetIsActiveOn(u.PersonId);
            }

            if (!form.UserPassword.IsNullOrEmpty())
                form.UserPassword = BCrypt.Net.BCrypt.HashPassword(form.UserPassword);

            return _userService.Create(form.ToUser(addressId)!);
        }

        public bool Create(OwnerRegisterForm form, Guid addressId)
        {
            if (_userService.GetOwnerByMail(form.Email) is not null)
                return false;

            return _userService.Create(form.ToOwner(addressId)!);
        }

        public bool Create(AddressRegisterForm form)
        {
            return _userService.Create(form.ToAddress());
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

        public IEnumerable<User?> Get()
        {
            return _userService.Get();
        }
        public IEnumerable<Person?> GetPersonsByRole(int role)
        {
            Role personRole = (Role)role;
            return _userService.GetPersonsByRole(personRole);
        }

        public User? GetUserById(Guid userId)
        {
            return _userService.GetUserById(userId);
        }

        public User? GetUserByMail(string mail)
        {
            return _userService.GetUserByMail(mail);
        }

        public Owner? GetByOwnerId(Guid ownerId)
        {
            return _userService.GetOwnerById(ownerId);
        }

        public Owner? GetByOwnerMail(string mail)
        {
            return _userService.GetOwnerByMail(mail);
        }

        public IEnumerable<Address> GetAddresses()
        {
            return _userService.GetAddresses()!;
        }

        public Address? GetAddressByPersonId(Guid personId)
        {
            return _userService.GetAddressByPersonId(personId);
        }

        //*****************************************************************************//
        //                                    PATCH                                    //
        //*****************************************************************************//

        public bool UpdateUser(UserEditForm form, Guid userId, Role role)
        {
            Guid userAddressId = _userService.GetAddressByPersonId(userId).AddressId;
            return (_userService.GetUserById(userId) is not null) ? _userService.Update(form.ToUser(userAddressId, role)!) : false;
        }
        public bool UpdateOwner(OwnerEditForm form, Guid ownerId)
        {
            Guid ownerAddressId = _userService.GetAddressByPersonId(ownerId).AddressId;
            return (_userService.GetOwnerById(ownerId) is not null) ? _userService.Update(form.ToOwner(ownerId, ownerAddressId)) : false;
        }

        //*****************************************************************************//
        //                                   DELETE                                    //
        //*****************************************************************************//

        public bool DeleteOwner(Guid ownerId)
        {
            return _userService.DeleteOwner(ownerId);
        }
        public bool DeleteUser(Guid userId)
        {
            return _userService.DeleteUser(userId);
        }
    }
}
