using FindMeApi.Data;
using FindMeApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [Authorize]
        [HttpGet("/mylocations")]
        public async Task<ActionResult<List<LocationDto>>> GetMyLocations()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user is null)
                return NotFound();

            var result = await _dbContext.Locations
                .Where(l => l.User == user)
                .Take(20)
                .Select(l => new LocationDto
                {
                    Accuracy = l.Accuracy,
                    Latitude = l.Latitude,
                    Longitude = l.Longitude,
                    TimeStamp = l.TimeStamp,
                    UserName = user.UserName
                })
                .ToListAsync();

            if (result is null)
                return NotFound();

            return result;
        }

        [Authorize]
        [HttpGet("/friendlocation/{friendUserName}")]
        public async Task<ActionResult<List<Location>>> GetFriendLocations(string friendUserName)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user is null)
                return NotFound();

            var friend = await _userManager.FindByNameAsync(friendUserName);

            if (friend is null)
                return NotFound();


            //checking if the friend has the user on his/her followers list.
            if (friend.Followers.Any(f => f == user))
            {
                var result = await _dbContext.Locations.Where(l => l.User == friend).Take(20).ToListAsync();

                if (result is null)
                    return NotFound();

                //TODO ReturnDTO. 
                return result;
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpPost("/addfollower")]
        public async Task<IActionResult> AddFollower(UserName followerDto)
        {
            if (followerDto is null)
            {
                return BadRequest();
            }

            var user = await _dbContext.Users
                .Include(u => u.Followers)
                .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            //var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user is null)
                return NotFound();

            var follower = await _userManager.FindByNameAsync(followerDto.Email);

            if (follower is null)
                return NotFound();

            if (user == follower)
                return BadRequest(new ReasonDto { Reason = "User can not follow him/her self." });


            if (user.Followers.Contains(follower))
            {
                return BadRequest(new ReasonDto { Reason = "Friend is already a follower."});
            }

            user.Followers.Add(follower);

            await _dbContext.SaveChangesAsync();

            return Ok();
        }


        [Authorize]
        [HttpPost("/removefollower")]
        public async Task<IActionResult> RemoveFollower(UserName followerUserName)
        {
            if (followerUserName is null)
            {
                return BadRequest();
            }

            var user = await _dbContext.Users
                .Include(u => u.Followers)
                .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            if (user is null)
                return NotFound();

            var follower = await _userManager.FindByNameAsync(followerUserName.Email);

            if (follower is null)
                return NotFound();

            if (!user.Followers.Contains(follower))
            {
                return BadRequest(new ReasonDto { Reason = "Friend is not a follower." });
            }

            user.Followers.Remove(follower);

            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        //TODO Get followers.
        [Authorize]
        [HttpGet("/followers")]
        public async Task<ActionResult<List<UserName>>> GetFollowers()
        {
            var user = await _dbContext.Users
                .Include(u => u.Followers)
                .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            if (user is null)
                return NotFound();

            if (user.Followers is null)
                return NotFound();
            
            return user.Followers.Select(u => new UserName { Email = u.Email }).ToList();
        }

        [Authorize]
        [HttpGet("/following")]
        public async Task<ActionResult<List<UserName>>> GetFollowing()
        {
            var user = await _dbContext.Users
                .Include(u => u.Following)
                .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            if (user is null)
                return NotFound();

            if (user.Following is null)
                return NotFound();

            return user.Following.Select(u => new UserName { Email = u.Email }).ToList();
        }

    }

    public class ReasonDto
    {
        public string Reason { get; set; }
    }
}
