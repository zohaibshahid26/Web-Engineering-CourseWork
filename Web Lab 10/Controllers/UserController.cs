using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Text.Json;

namespace Web_Lab_10.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            if(Request.Cookies.ContainsKey("Login") && Request.Cookies["Login"] == "True")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Login(string Email, string Password,string Color)
        {
           
            var CookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(10)
            };
            Response.Cookies.Append("Login", "True", CookieOptions);
            Response.Cookies.Append("Color", Color, CookieOptions);
            

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("Login");
            return RedirectToAction("Index", "Home");
        }
    }
}
