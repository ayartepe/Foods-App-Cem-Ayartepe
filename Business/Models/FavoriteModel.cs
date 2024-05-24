#nullable disable

using System.ComponentModel;

namespace Business.Models
{
    public class FavoriteModel
    {
        public int GameId { get; set; }

        [DisplayName("Chef Name")]
        public string UserName { get; set; }

        [DisplayName("Food Name")]
        public string GameName { get; set; }

        public decimal? SalesPrice { get; set; }

        [DisplayName("Price")]
        public string SalesPriceOutput { get; set; }
    }
}
