using DAL.Entities.Enumerations;

namespace DAL.Entities.DTO
{
    public class UserTokenDto_DAL
    {
        public Guid PersonId { get; set; }
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public Role PersonRole { get; set; }
    }
}
