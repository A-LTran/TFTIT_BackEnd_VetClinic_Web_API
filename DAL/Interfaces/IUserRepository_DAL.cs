namespace DAL.Interfaces
{
    public interface IUserRepository_DAL
    {
        public bool Create(User user);
        public bool Create(Owner owner);
        public bool Create(Address address);
        //public bool Update(User user);
        //public bool Delete(User user);
        //public User? GetByMail(string mail);
        //public User? GetById(int id);
        public IEnumerable<User?> Get();
        public IEnumerable<Person?> GetPersonsByRole(Role personRole);
    }
}
