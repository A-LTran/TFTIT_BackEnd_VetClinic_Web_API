namespace DAL.Entities
{
    public class User : Person

    {
        public User()
        {

        }
        public User(Guid userId, string lastName, string firstName, string email, string phone, string mobile, DateTime birthDate, string userPassword, Guid addressId)
        {
            UserId = userId;
            LastName = lastName;
            FirstName = firstName;
            Email = email;
            Phone = phone;
            Mobile = mobile;
            BirthDate = birthDate;
            UserPassword = userPassword;
            AddressId = addressId;
        }
        public string UserPassword { get; set; } = default!;
    }
}
