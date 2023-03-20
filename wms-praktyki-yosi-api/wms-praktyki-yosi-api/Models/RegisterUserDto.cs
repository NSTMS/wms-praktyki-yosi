using System.ComponentModel.DataAnnotations;

namespace wms_praktyki_yosi_api.Models
{
    public class RegisterUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
