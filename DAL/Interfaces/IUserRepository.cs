namespace DAL.Interfaces
{
    public interface IUserRepository
    {
        public User Create(User user);
        public bool Update(User user);
        public bool Delete(User user);
        public User? GetByMail(string mail);
        public User? GetById(int id);
        public IEnumerable<User> GetAll();
    }
}
