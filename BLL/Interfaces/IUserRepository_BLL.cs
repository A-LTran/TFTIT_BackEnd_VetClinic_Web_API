using BLL.Entities.PersonForms;

namespace BLL.Interfaces
{
    public interface IUserRepository_BLL
    {
        //internal User Create(User user);
        public bool Create(UserRegisterForm form, Guid addressId);
        public bool Create(OwnerRegisterForm form, Guid addressId);
        public bool Create(AddressRegisterForm form);
        public bool UpdateUser(UserEditForm form, Guid userId, Role role);
        public bool UpdateOwner(OwnerEditForm form, Guid ownerId);
        public bool DeleteUser(Guid userId);
        public bool DeleteOwner(Guid ownerId);
        public IEnumerable<User?> Get();
        public IEnumerable<Person?> GetPersonsByRole(int role);
        public User? GetUserById(Guid userId);
        public User? GetUserByMail(string mail);
        public Owner? GetByOwnerId(Guid ownerId);
        public Owner? GetByOwnerMail(string mail);
        public IEnumerable<Address> GetAddresses();
        public Address? GetAddressByPersonId(Guid personId);
        public User? Login(UserLoginForm form);
    }
}
