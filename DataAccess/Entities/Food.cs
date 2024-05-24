#nullable disable

using DataAccess.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Food : Record
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public DateTime? OrderDate { get; set; } 
        public decimal? SalesPrice { get; set; } 

        public int? ChefId { get; set; }
        public Chef Chef { get; set; }

        public List<UserFood> UserFoods { get; set; }
    }
}
