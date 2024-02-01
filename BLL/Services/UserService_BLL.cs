using Microsoft.IdentityModel.Tokens;

namespace BLL.Services
{
    public class UserService_BLL : IUserRepository_BLL
    {
        private readonly IUserRepository_DAL _userRepository;

        public UserService_BLL(IUserRepository_DAL userRepository)
        {
            _userRepository = userRepository;
        }

        // POST

        public bool Create(UserRegisterForm form, Guid addressId)
        {
            if (!form.UserPassword.IsNullOrEmpty())
                form.UserPassword = BCrypt.Net.BCrypt.HashPassword(form.UserPassword);
            return _userRepository.Create(form.ToUser(addressId)!);
        }

        public bool Create(OwnerRegisterForm form, Guid addressId)
        {
            return _userRepository.Create(form.ToOwner(addressId)!);
        }

        public bool Create(AddressRegisterForm form)
        {
            return _userRepository.Create(form.ToAddress());
        }
        public User? Login(UserLoginForm form)
        {
            User? u = _userRepository.GetByMail(form.Email);
            if (u is null)
                return null;
            return BCrypt.Net.BCrypt.Verify(form.UserPassword, u.UserPassword) ? u : null;
        }

        // GET

        public IEnumerable<User?> Get()
        {
            return _userRepository.Get();
        }
        public IEnumerable<Person?> GetPersonsByRole(int role)
        {
            Role personRole = (Role)role;
            return _userRepository.GetPersonsByRole(personRole);
        }

        public User? GetUserById(Guid userId)
        {
            return _userRepository.GetById(userId);
        }

        public User? GetUserByMail(string mail)
        {
            return _userRepository.GetByMail(mail);
        }

        public IEnumerable<Address> GetAddresses()
        {
            return _userRepository.GetAddresses();
        }

        // PATCH

        public bool UpdateUser(UserEditForm form, Guid userId)
        {
            return _userRepository.Update(form.ToUser(userId));
        }
        public bool UpdateOwner(OwnerEditForm form, Guid ownerId)
        {
            return _userRepository.Update(form.ToOwner(ownerId));
        }

        // DELETE

        public bool DeleteOwner(Guid ownerId)
        {
            return _userRepository.DeleteOwner(ownerId);
        }
        public bool DeleteUser(Guid userId)
        {
            return _userRepository.DeleteUser(userId);
        }
    }
}
