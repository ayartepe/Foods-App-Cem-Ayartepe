#nullable disable

using DataAccess.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
	public class FoodModel : Record
	{
		#region Entity Properties
		[Required(ErrorMessage = "{0} is required!")]

		[MinLength(2, ErrorMessage = "{0} must be minimum {1} characters!")]
		[MaxLength(100, ErrorMessage = "{0} must be maximum {1} characters!")]
		[DisplayName("Food Name")]
		public string Name { get; set; }

		[DisplayName("Order Date")]
		public DateTime? OrderDate { get; set; }

		[DisplayName("Price")]
		[Range(0, double.MaxValue, ErrorMessage = "{0} must be positive!")]
		public decimal? SalesPrice { get; set; }

		[DisplayName("Chef")]
		public int? ChefId { get; set; }
		#endregion

		#region Extra Properties
		[DisplayName("Order Date")]
		public string OrderDateOutput { get; set; }

        [DisplayName("Price")]
        public string SalesPriceOutput { get; set; }

        [DisplayName("Chefs")]
        public string ChefOutput { get; set; }

		[DisplayName("Users")]
        public List<int> UsersInput { get; set; }

        public List<UserModel> Users { get; set; }
        #endregion
    }
}
