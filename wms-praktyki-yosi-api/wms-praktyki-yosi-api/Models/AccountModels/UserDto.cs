using System.ComponentModel.DataAnnotations;
using wms_praktyki_yosi_api.Enitities;

namespace wms_praktyki_yosi_api.Models.AccountModels
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
    }
}
