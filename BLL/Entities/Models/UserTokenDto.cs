using DAL.Entities.Enumerations;

namespace BLL.Entities.Models
{
    public class UserTokenDto
    {
        public UserTokenDto()
        {
            
        }
        public UserTokenDto(Guid personId, string lastName, string email, Role personRole)
        {
            PersonId = personId;
            LastName = lastName;
            Email = email;
            PersonRole = personRole;
        }

        public Guid PersonId { get; set; }
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public Role PersonRole { get; set; }
    }
}
