namespace BLL.Services
{
    public class UserService_BLL : IUserRepository_BLL
    {
        private readonly IUserRepository_DAL _userRepository;

        public UserService_BLL(IUserRepository_DAL userRepository)
        {
            _userRepository = userRepository;
        }

        public bool Create(UserRegisterForm form)
        {
            return _userRepository.Create(form.ToUser());
        }

        public bool Create(OwnerRegisterForm form)
        {
            return _userRepository.Create(form.ToOwner());
        }

        public bool Create(AddressForm form)
        {
            return _userRepository.Create(form.ToAddress());
        }

        public IEnumerable<User> Get()
        {
            return _userRepository.Get();
        }
        public IEnumerable<Person> GetUsersByRole(int role)
        {
            Role PersonRole = (Role)role;
            return _userRepository.GetUsersByRole(PersonRole);
        }
    }
}
