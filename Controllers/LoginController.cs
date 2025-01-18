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
        /* Gets the inputs from the register form and calls the model to register user */
        [HttpPost]
        public async Task<IActionResult> Register(string companyName, string email, string ssn, string username, string password, string confirmPassword, string conditions)
        {
            // Validate all inputs
            if (!(companyName.Length > 0 & email.Length > 0 & ssn.Length == 9 & username.Length >= 4 & password.Length >= 8 & confirmPassword.Equals(password) & conditions != null))
            {
                TempData["errorMessage"] = "There are errors in the register form";
                return View("Join");
            }
            
            using var connection = new MySqlConnection(_configuration.GetConnectionString("Default"));
            UserModel um = new UserModel(connection);

            try
            {
                User user = new User
                {
                    name = companyName,
                    email = email,
                    ssn = ssn,
                    username = username,
                    password = password,
                    joined = DateTime.Now,
                };
                bool registered = um.Register(user);



                if (registered)
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
                else
                {
                    TempData["errorMessage"] = "Something went wrong...";
                    return View("Join");
                }

            } catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View("Join");
            }


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
        /* Logout and clear session */
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return View();
        }
    }
}
