namespace DAL.Entities
{
    public class Person
    {
        public Person()
        {

        }
        public Person(Guid userId, string lastName, string firstName, string email, string phone, string mobile, DateTime birthDate, Guid addressId)
        {
            UserId = userId;
            LastName = lastName;
            FirstName = firstName;
            Email = email;
            Phone = phone;
            Mobile = mobile;
            BirthDate = birthDate;
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
