#nullable disable

using DataAccess.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class ChefModel : Record
    {
        #region Entity Properties
        [DisplayName("Chef Name")]
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(200, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string Name { get; set; }
        #endregion

        #region Extra Properties
        public string Foods { get; set; }
        #endregion
    }
}
