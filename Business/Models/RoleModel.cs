#nullable disable

using DataAccess.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class RoleModel : Record
    {
        #region Entity Properties
        [DisplayName("Admin or User")]
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "{0} must be minimum {2} maximum {1} characters!")]
        public string Name { get; set; }
        #endregion

        #region Extra Properties
        [DisplayName("User Count")]
        public int UserCount { get; set; }

        public string Users { get; set; }
        #endregion
    }
}
