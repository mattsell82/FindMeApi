using FindMeApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FindMeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;

        [HttpGet]
        [Authorize]
        public async Task<Test> Get()
        {
            return new Test { Email = "mattias.sellden@gmail.com", Password = "hejtest" };
        }
    }


    public class Test
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}