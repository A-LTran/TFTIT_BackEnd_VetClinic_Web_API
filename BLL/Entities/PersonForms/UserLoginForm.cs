using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BLL.Entities.PersonForms
{
    public class UserLoginForm
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;
        [Required]
        [PasswordPropertyText]
        public string UserPassword { get; set; } = default!;
    }
}
