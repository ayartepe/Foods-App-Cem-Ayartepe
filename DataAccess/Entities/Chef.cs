#nullable disable

using DataAccess.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Chef : Record
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public List<Food> Foods { get; set; }
    }
}
