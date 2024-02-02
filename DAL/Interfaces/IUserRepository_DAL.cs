﻿namespace DAL.Interfaces
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
        public bool Update(Owner owner);
        public bool Update(User user);
        public bool DeleteUser(Guid personId);
        public bool DeleteOwner(Guid ownerId);
        public IEnumerable<User?> Get();
        public IEnumerable<Person?> GetPersonsByRole(Role personRole);
        public IEnumerable<Address?> GetAddresses();
    }
}
