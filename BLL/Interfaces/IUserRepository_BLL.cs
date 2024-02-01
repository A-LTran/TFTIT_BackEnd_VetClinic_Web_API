namespace BLL.Interfaces
{
    public interface IUserRepository_BLL
    {
        //internal User Create(User user);
        public bool Create(UserRegisterForm form, Guid addressId);
        public bool Create(OwnerRegisterForm form, Guid addressId);
        public bool Create(AddressRegisterForm form);
        //internal bool Update(User user);
        //internal bool Delete(User user);
        //internal User? GetByMail(string mail);
        //internal User? GetById(int id);
        public IEnumerable<User?> Get();
        public IEnumerable<Person?> GetPersonsByRole(int role);
        public User? GetUserById(Guid userId);
        public User? GetUserByMail(string mail);
        public IEnumerable<Address> GetAddresses();
        public User? Login(UserLoginForm form);
    }
}
