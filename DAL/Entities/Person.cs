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
        //Edit form
        public Person(string lastName, string firstName, string email, string phone, string mobile, DateTime birthDate, Guid addressId) : this(lastName, firstName, email, phone, mobile, birthDate)
        {
            AddressId = addressId;

        }

        // Register Form
        public Person(string lastName, string firstName, string email, string phone, string mobile, DateTime birthDate, Role personRole, Guid addressId) : this(lastName, firstName, email, phone, mobile, birthDate)
        {
            AddressId = addressId;
            PersonRole = personRole;

        }

        // User.cs DAL_ToUser & Owner.cs DAL_ToOwnr
        public Person(Guid personId, string lastName, string firstName, string email, string phone, string mobile, DateTime birthDate, Role personRole, Guid addressId) : this(lastName, firstName, email, phone, mobile, birthDate)
        {
            PersonId = personId;
            PersonRole = personRole;
            AddressId = addressId;
        }

        public Guid PersonId { get; set; } = Guid.NewGuid();
        public string LastName { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string Mobile { get; set; } = default!;
        public DateTime BirthDate { get; set; }
        public Role PersonRole { get; set; }
        public Guid AddressId { get; set; } = default!;
    }
}
