using Cartel_Search_Products.Helpers;
using Cartel_Search_Products.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cartel_Search_Products.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Login(string username, string passowrd)
        {
            User user = null;

            //var user = auth(username, passowrd); Validate credentials
            if (user != null)
            {
                // Store user in the session
                HttpContext.Session.SetObjectAsJson("currentuser", user);
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ErrorMessage = "Invalid username or password";
            return View();
        }
    }
}
