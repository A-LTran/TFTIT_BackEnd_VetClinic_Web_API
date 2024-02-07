using DAL.Entities.DTO;
using DAL.Entities.Enumerations;

namespace DAL.Interfaces
{
    public interface IUserRepository_DAL
    {
        public bool Create(User user);
        public bool Create(Owner owner);
        public bool Create(Address address);
        public User? GetUserById(Guid id);
        public User? GetUserByMail(string mail);
        public Owner? GetOwnerById(Guid ownerId);
        public Owner? GetOwnerByMail(string mail);
        public string? GetUserPasswordByMail(string mail);
        public bool PersonExistsCheckById(Guid personId);
        public bool PersonExistsCheckByMail(string mail);
        public bool AddressExistsCheckById(Guid id);
        public UserTokenDto_DAL? GetUserDtoByMail(string mail);
        public bool GetIsActiveByMail(string mail);
        public Address? GetAddressById(Guid addressId);
        public IEnumerable<Address?> GetAddressByAddressInfo(Address address);
        public bool Update(Owner owner);
        public bool Update(User user);
        public bool Update(Address address);
        public bool SetIsActiveOn(Guid personId);
        public bool SetIsActiveOff(Guid personId);
        public bool DeleteUser(Guid personId);
        public bool DeleteOwner(Guid ownerId);
        public bool DeleteAddress(Guid addressId);
        public IEnumerable<User?> Get();
        public IEnumerable<Person?> GetPersonsByRole(Role personRole);
        public IEnumerable<Address?> GetAddresses();
        public Address? GetAddressByPersonId(Guid personId);
    }
}
