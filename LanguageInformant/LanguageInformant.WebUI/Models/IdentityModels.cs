using Microsoft.AspNet.Identity.EntityFramework;
using System;
namespace LanguageInformant.WebUI.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Email { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }       
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsLoggedIn { get; set; }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
    }
}