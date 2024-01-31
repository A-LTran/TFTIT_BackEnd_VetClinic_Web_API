namespace DAL.Interfaces
{
    public interface IUserRepository_DAL
    {
        public bool Create(User user);
        public bool Create(Owner owner);
        public bool Create(Address address);
        public User? GetById(Guid id);
        public User? GetByMail(string mail);
        //public bool Update(User user);
        //public bool Delete(User user);

        public IEnumerable<User?> Get();
        public IEnumerable<Person?> GetPersonsByRole(Role personRole);
    }
}
