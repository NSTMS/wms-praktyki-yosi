﻿using System.ComponentModel.DataAnnotations;

namespace wms_praktyki_yosi_api.Models
{
    public class UserLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
