﻿using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ECommerce.WebAPI.Dtos
{
    public class SignUpDto
    {
        public string? Username { get; set; }
        public string? Password { get; set; } 
        public string? Email { get; set; }
        public string? Role { get; set; }
    }
}
