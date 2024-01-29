namespace BLL.Interfaces
{
    public interface IUserRepository_BLL
    {
        //internal User Create(User user);
        //internal bool Update(User user);
        //internal bool Delete(User user);
        //internal User? GetByMail(string mail);
        //internal User? GetById(int id);
        public IEnumerable<User> Get();
        public IEnumerable<Person> GetUsersByRole(int role);
    }
}
