using FindMeApi.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindMeApi.Data
{
    public class ApplicationUser : IdentityUser
    {
        public List<Location>? Locations { get; set; }
        public List<ApplicationUser>? Followers { get; set; }
        public List<ApplicationUser>? Following { get; set; }
    }
}
