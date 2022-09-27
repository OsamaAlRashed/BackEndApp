﻿using Microsoft.AspNetCore.Identity;

namespace BackEndApp.Models
{
    public class User : IdentityUser<int>
    {
        public string FullName { get; set; }
    }
}
