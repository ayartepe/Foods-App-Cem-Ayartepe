#nullable disable

using DataAccess.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Role : Record
    {
        // Way 1: you should put nullable disable on top of the class
        //[Required]
        //public string Name { get; set; }

        // Way 3:
        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        public ICollection<User> Users { get; set; } // List
    }
}
