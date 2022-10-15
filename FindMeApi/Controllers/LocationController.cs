using FindMeApi.Data;
using FindMeApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FindMeApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _dbContext;

        public LocationController(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(Location location)
        {
            if (location is null)
                return BadRequest();

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user is null)
                return NotFound();

            if (user.Locations is null)
                user.Locations = new List<Location>();

            user.Locations.Add(location);
            var result = await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
