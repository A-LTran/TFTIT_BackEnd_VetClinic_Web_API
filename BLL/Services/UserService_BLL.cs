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
    }
}
