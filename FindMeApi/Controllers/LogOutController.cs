using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using FindMeApi.Data;
using FindMeApi.Models;

namespace FindMeApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LogOutController : ControllerBase
    {
        private readonly Microsoft.AspNetCore.Identity.SignInManager<ApplicationUser> _signInManager;

        public LogOutController(SignInManager<ApplicationUser> signInManager)
        {
            this._signInManager = signInManager;
        }


        [HttpPost]
        public async Task<IActionResult> Post()
        {
            await _signInManager.SignOutAsync();

            return Ok();
        }

    }
}
