using DAL.Entities.Enumerations;

namespace DAL.Entities
{
    public class User : Person

    {
        public User()
        {

        }
        //DAL ToUser
        public User(Guid personId, string lastName, string firstName, string email, string phone, string mobile, DateTime birthDate, string userPassword, Role personRole, Guid addressId) : base(personId, lastName, firstName, email, phone, mobile, birthDate, personRole, addressId)
        {
            UserPassword = userPassword;
        }
        //BLL ToUser
        // EDIT
        public User(string lastName, string firstName, string email, string phone, string mobile, DateTime birthDate, string userPassword, Guid addressId) : base(lastName, firstName, email, phone, mobile, birthDate, addressId)
        {
            UserPassword = userPassword;
        }
        // REGISTER
        public User(string lastName, string firstName, string email, string phone, string mobile, DateTime birthDate, string userPassword, Role personRole, Guid addressId) : base(lastName, firstName, email, phone, mobile, birthDate, personRole, addressId)
        {
            UserPassword = userPassword;
        }

        public string UserPassword { get; set; } = default!;
    }
}
