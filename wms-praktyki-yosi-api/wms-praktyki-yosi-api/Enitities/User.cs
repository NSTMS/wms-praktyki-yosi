using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace wms_praktyki_yosi_api.Enitities
{
    public class User : IdentityUser
    {
         public string CustomName { get; set; }
        //[Required]
        //public string Email { get; set; }
        //[Required]
        //public string PasswordHash { get; set; }
        //public int RoleId { get; set; }
        //public virtual Role Role { get; set; }*/
    }
}
