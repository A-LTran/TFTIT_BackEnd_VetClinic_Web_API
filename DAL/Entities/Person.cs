namespace DAL.Entities
{
    public class Person
    {
        public Person()
        {

        }
        public Person(string lastName, string firstName, string email, string phone, string mobile, DateTime birthDate)
        {
            LastName = lastName;
            FirstName = firstName;
            Email = email;
            Phone = phone;
            Mobile = mobile;
            BirthDate = birthDate;

        }

        // User.cs BLL_ToUser & Owner.cs BLL_ToOwner
        public Person(string lastName, string firstName, string email, string phone, string mobile, DateTime birthDate, Role userRole, Guid addressId) : this(lastName, firstName, email, phone, mobile, birthDate)
        {
            AddressId = addressId;
            UserRole = userRole;

        }

        // User.cs DAL_ToUser & Owner.cs DAL_ToOwnr
        public Person(Guid userId, string lastName, string firstName, string email, string phone, string mobile, DateTime birthDate, Guid addressId) : this(lastName, firstName, email, phone, mobile, birthDate)
        {
            UserId = userId;
            AddressId = addressId;
        }

        public Guid UserId { get; set; } = Guid.NewGuid();
        public string LastName { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string Mobile { get; set; } = default!;
        public DateTime BirthDate { get; set; }
        public Role UserRole { get; set; }
        public Guid AddressId { get; set; } = default!;
    }
}
