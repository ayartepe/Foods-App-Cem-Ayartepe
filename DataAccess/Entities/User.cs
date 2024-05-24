#nullable disable

using DataAccess.Enums;
using DataAccess.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class User : Record
    {
        [Required]
        [StringLength(15)]
        public string UserName { get; set; }

        [Required]
        [StringLength(10)]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        public Statuses Status { get; set; } // commis, sous, executive

        public int RoleId { get; set; }

        public Role Role { get; set; }

        public List<UserFood> UserFoods { get; set; }
    }
}
