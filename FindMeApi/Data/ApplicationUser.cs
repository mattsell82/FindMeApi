using FindMeApi.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindMeApi.Data
{
    public class ApplicationUser : IdentityUser
    {
        public List<Location>? Locations { get; set; } = new List<Location>();
        public List<ApplicationUser>? Followers { get; set; } = new List<ApplicationUser>();
        public List<ApplicationUser>? Following { get; set; } = new List<ApplicationUser>();
    }
}
