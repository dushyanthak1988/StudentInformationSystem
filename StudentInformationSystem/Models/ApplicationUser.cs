﻿using Microsoft.AspNetCore.Identity;

namespace StudentInformationSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }

    public class ApplicationRole : IdentityRole
    {

    }
}
