using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class OwnerEditForm
    {
        [MaxLength(50)]
        [MinLength(2)]
        public string LastName { get; set; } = default!;
        [MaxLength(50)]
        [MinLength(2)]
        public string FirstName { get; set; } = default!;
        [EmailAddress]
        public string Email { get; set; } = default!;
        [Phone]
        public string Phone { get; set; } = "0000000000";
        [Phone]
        public string Mobile { get; set; } = "0000000000"!;
        [DateRangeBeforeTodayAndAfter100Y]
        public DateTime BirthDate { get; set; } 
    }
}
