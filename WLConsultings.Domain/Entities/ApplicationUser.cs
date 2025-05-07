using Microsoft.AspNetCore.Identity;
using System;

namespace WLConsultings.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string Name { get; set; }
        public string Role { get; set; } = "Admin";
    }
}
