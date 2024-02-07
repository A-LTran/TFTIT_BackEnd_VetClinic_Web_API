namespace BLL.Entities.Models
{
    public class UserDto
    {
        public UserDto()
        {

        }
        public UserDto(Guid personId, string lastName, string firstName, string email, string phone, string mobile, DateTime birthDate)
        {
            PersonId = personId;
            LastName = lastName;
            FirstName = firstName;
            Email = email;
            Phone = phone;
            Mobile = mobile;
            BirthDate = birthDate;
        }

        public Guid PersonId { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string Mobile { get; set; } = default!;
        public DateTime BirthDate { get; set; }
    }
}
