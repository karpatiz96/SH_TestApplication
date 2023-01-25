using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SH_TestApplication.Models;
using SH_TestApplication.Services;
using System.Security.Claims;

namespace SH_TestApplication.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IUserService userService;

        public AccountController(IUserService UserService)
        {
            userService = UserService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginViewModel loginViewModel = new LoginViewModel();
            return View(loginViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await ValidateUser(loginViewModel.UserName, loginViewModel.Password);
                if(user == null)
                {
                    ViewBag.Message = "Invalid Username or Password!";
                    return View(loginViewModel);
                } else {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim("Id", user.Id.ToString())
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principals = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principals, 
                        new AuthenticationProperties 
                        {
                            IsPersistent = false,
                            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(20),
                            IssuedUtc = DateTimeOffset.UtcNow,
                            AllowRefresh = true
                        });

                    return RedirectToAction("Index", "User");
                }
            }
            return View(loginViewModel);
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return LocalRedirect("/Account/Login");
        }

        private async Task<UserViewModel> ValidateUser(string Username, string Password)
        {
            var users = await userService.GetUsers();

            var user = users.FirstOrDefault(x => x.UserName == Username);

            if(user != null && user.Password == Password)
            {
                return user;
            }

            return null;
        }
    }
}
