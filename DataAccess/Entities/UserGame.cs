#nullable disable

using DataAccess.Records.Bases;

namespace DataAccess.Entities
{
    public class UserGame : Record
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int GameId { get; set; }
        public Food Game { get; set; }
    }
}
