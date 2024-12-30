using System.Security.Claims;
using Cartel_Search_Products.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace Cartel_Search_Products.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;

        private readonly IConfiguration _configuration;
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // When user clicks the "Join Cartel" button, user being redirected to Join view
        public IActionResult Join()
        {
            return View("Join");
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                _context.User.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }


        // When User completed the Login Form, retrieve the username and the password user typed and authenticate
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            using var connection = new MySqlConnection(_configuration.GetConnectionString("Default"));
            UserModel um = new UserModel(connection);

            try
            {
                User user = um.authenticate(username, password);
                connection.Close();

                if (user != null)
                {
                    // Create claims for the user
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.username),
                new Claim(ClaimTypes.Email, user.email),
                // Add any other claims you need
            };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    // Sign in the user
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        principal,
                        new AuthenticationProperties
                        {
                            IsPersistent = true, // This will make the cookie persistent
                            ExpiresUtc = DateTime.UtcNow.AddDays(30) // Cookie will expire in 30 days
                        });

                    return RedirectToAction("Index", "Home");
                }

                TempData["errorMessage"] = "Invalid Login Credentials";
                return View("Join");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View("Join");
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return View();
        }
    }
}
