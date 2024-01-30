namespace DAL.Entities
{
    public class User : Person

    {
        public User()
        {

        }
        //DAL ToUser
        public User(Guid userId, string lastName, string firstName, string email, string phone, string mobile, DateTime birthDate, string userPassword, Guid addressId) : base(userId, lastName, firstName, email, phone, mobile, birthDate, addressId)
        {
            UserPassword = userPassword;
        }
        //BLL ToUser
        public User(string lastName, string firstName, string email, string phone, string mobile, DateTime birthDate, string userPassword, Role userRole, Guid addressId) : base(lastName, firstName, email, phone, mobile, birthDate, userRole, addressId)
        {
            UserPassword = userPassword;
        }

        public string UserPassword { get; set; } = default!;
    }
}
