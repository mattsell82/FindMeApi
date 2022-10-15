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
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;
using System.Text;

namespace FindMeApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Register : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;

        public Register(
            SignInManager<ApplicationUser> signInManager,
            IUserStore<ApplicationUser> userStore,
            UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userStore = userStore;
            _userManager = userManager;
            _emailStore = GetEmailStore();
        }


        [HttpPost]
        public async Task<IActionResult> Post(Login login)
        {
            var user = CreateUser();

            user.Locations = new List<Location>();
            user.Followers = new List<ApplicationUser>();
            user.Following = new List<ApplicationUser>();


            await _userStore.SetUserNameAsync(user, login.Email, CancellationToken.None);
            await _emailStore.SetEmailAsync(user, login.Email, CancellationToken.None);
            var result = await _userManager.CreateAsync(user, login.Password);

            if (result.Succeeded)
            {
                return Created("User created", new {});
                //_logger.LogInformation("User created a new account with password.");

/*                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmail",
                pageHandler: null,
                    values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                    protocol: Request.Scheme);

                await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                if (_userManager.Options.SignIn.RequireConfirmedAccount)
                {
                    return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                }
                else
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }*/
            }
            else
            {
                return BadRequest(result.Errors);
            }


        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }

    }
}
