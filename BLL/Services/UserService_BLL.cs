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

        public IEnumerable<User?> Get()
        {
            return _userRepository.Get();
        }
        public IEnumerable<Person?> GetPersonsByRole(int role)
        {
            Role PersonRole = (Role)role;
            return _userRepository.GetPersonsByRole(PersonRole);
        }

        public User? GetUserById(Guid userId)
        {
            return _userRepository.GetById(userId);
        }

        public User? GetUserByMail(string mail)
        {
            return _userRepository.GetByMail(mail);
        }
    }
}
