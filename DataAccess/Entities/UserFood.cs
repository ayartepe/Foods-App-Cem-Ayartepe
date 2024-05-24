#nullable disable

using DataAccess.Records.Bases;

namespace DataAccess.Entities
{
    public class UserFood : Record
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int FoodId { get; set; }
        public Food Game { get; set; }
    }
}
