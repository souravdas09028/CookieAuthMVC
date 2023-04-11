using CookieAuthMVC.Models.Request;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CookieAuthMVC.Controllers
{
    public class AccessController : Controller
    {
        public IActionResult LogIn()
        {
            ClaimsPrincipal user = HttpContext.User;

            if (user.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LogInRequest logReq)
        {
            if (logReq != null && logReq.UserID == "admin" && logReq.Password == "admin")
            {
                List<Claim> claims = new List<Claim>()
                { new Claim(ClaimTypes.NameIdentifier, logReq.UserID),
                new Claim(ClaimTypes.Role, "admin")};

                ClaimsIdentity identity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = false
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity), properties);

                return RedirectToAction("Index", "Home");
            }

            ViewData["ValidateMessage"] = "User not found";
            return View();
        }
    }
}
