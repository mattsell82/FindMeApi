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
    public class LoginController : ControllerBase
    {
        private readonly Microsoft.AspNetCore.Identity.SignInManager<ApplicationUser> _signInManager;

        public LoginController(SignInManager<ApplicationUser> signInManager)
        {
            this._signInManager = signInManager;
        }


        [HttpPost]
        public async Task<IActionResult> Post(Login login)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return Ok();
            }
            if (result.RequiresTwoFactor)
            {
                return BadRequest();
                //return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
            }
            if (result.IsLockedOut)
            {
                //_logger.LogWarning("User account locked out.");
                //return RedirectToPage("./Lockout");
                return BadRequest();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Unauthorized();
            }

            return BadRequest();
        }
    }
}
