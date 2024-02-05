using Microsoft.IdentityModel.Tokens;

namespace BLL.Services
{
    public class UserService_BLL : IUserRepository_BLL
    {

        private readonly IUserRepository_DAL _userService;

        private string _message;
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

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
                if (_userService.GetIsActiveByMail(u.Email))
                {
                    Message = "User already exists!";
                    return false;
                }
                else
                {
                    Message = "User has been reactivated! Please update your information.";
                    return _userService.SetIsActiveOn(u.PersonId);
                }
            }

            if (!form.UserPassword.IsNullOrEmpty())
                form.UserPassword = BCrypt.Net.BCrypt.HashPassword(form.UserPassword);

            if (_userService.Create(form.ToUser(addressId)!))
            {
                Message = "User has been created.";
                return true;
            }
            else
            {
                Message = "Something went wrong!";
                return false;
            }
        }

        public bool Create(OwnerRegisterForm form, Guid addressId)
        {
            if (_userService.GetOwnerByMail(form.Email) is not null)
            {
                Message = "Owner already exists!";
                return false;
            }
            if (_userService.Create(form.ToOwner(addressId)!))
            {
                Message = "Owner has been created!";
                return true;
            }

            Message = "Something went wrong!";
            return false;
        }

        public bool Create(AddressRegisterForm form)
        {
            List<Address?> addresses = _userService.GetAddressByAddressInfo(form.ToAddress()).ToList();
            if (addresses is not null)
            {
                Message = "Address already exists!";
                return false;
            }

            if (_userService.Create(form.ToAddress()))
            {
                Message = "Address created";
                return true;
            }
            Message = "Something went wrong!";
            return false;
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
            if (_userService.GetUserById(userId) is null)
            {
                Message = "User doesn't exist!";
            }

            Address? address = _userService.GetAddressByPersonId(userId);
            if (address is null)
            {
                Message = "This person doens't have a valid address.";
                return false;
            }

            form.UserPassword = BCrypt.Net.BCrypt.HashPassword(form.UserPassword!);

            if (_userService.Update(form.ToUser(userId, address.AddressId, role)!))
            {
                Message = "User has been updated.";
                return true;
            }

            Message = "Something went wrong!";
            return false;

        }

        public bool UpdateOwner(OwnerEditForm form, Guid ownerId)
        {
            if (_userService.GetUserById(ownerId) is null)
            {
                Message = "User doesn't exist!";
            }

            Address? address = _userService.GetAddressByPersonId(ownerId);
            if (address is null)
            {
                Message = "This owner doens't have a valid address.";
                return false;
            }

            if (_userService.Update(form.ToOwner(ownerId, address.AddressId)!))
            {
                Message = "User has been updated.";
                return true;
            }

            Message = "Something went wrong!";
            return false;
        }

        public bool UpdateAddress(AddressEditForm form, Guid addressId)
        {
            if (_userService.GetAddressById(addressId) is null)
            {
                Message = "Address doesn't exist!";
                return false;
            }

            if (_userService.Update(form.ToAddress(addressId)))
            {
                Message = "Address has been updated.";
                return true;
            }

            Message = "Something went wrong!";
            return false;
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

        //*****************************************************************************//
        //                                    TOOL                                     //
        //*****************************************************************************//


        public string GetMessage()
        {
            return Message;
        }
    }
}
