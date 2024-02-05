using DAL.Entities.Enumerations;

namespace BLL.Interfaces
{
    public interface IUserRepository_BLL
    {
        public bool Create(UserRegisterForm form, Guid addressId);
        public bool Create(OwnerRegisterForm form, Guid addressId);
        public bool Create(AddressRegisterForm form);
        public bool UpdateUser(UserEditForm form, Guid userId, Role role);
        public bool UpdateOwner(OwnerEditForm form, Guid ownerId);
        public bool UpdateAddress(AddressEditForm form, Guid addressId);
        public bool DeleteUser(Guid userId);
        public bool DeleteOwner(Guid ownerId);
        public bool DeleteAddress(Guid addressId);
        public IEnumerable<UserForDisplay?> Get();
        public IEnumerable<UserForDisplay> GetPersonsByRole(int role);
        public UserForDisplay? GetUserById(Guid userId);
        public UserForDisplay? GetUserByMail(string mail);
        public UserForDisplay? GetOwnerById(Guid ownerId);

        public UserForDisplay? GetOwnerByMail(string mail);
        public IEnumerable<Address> GetAddresses();
        public Address? GetAddressByPersonId(Guid personId);
        public User? Login(UserLoginForm form);
    }
}
