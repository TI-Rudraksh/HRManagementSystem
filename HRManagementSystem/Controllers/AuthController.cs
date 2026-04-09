using HRManagementSystem.Data;
using HRManagementSystem.Hepler;
using HRManagementSystem.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HRManagementSystem.Controllers
{
    public class AuthController : Controller
    {
        private readonly DB _context;

        public AuthController(DB context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _context.Users.FirstOrDefault(x => x.Email == model.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid credentials");
                return View(model);
            }

            var decrypted = PasswordHelper.Decrypt(user.PasswordHash); 

            if (decrypted != model.Password)
            {
                ModelState.AddModelError("", "Invalid credentials");
                return View(model);
            }

            var role = _context.Roles.Find(user.RoleId)?.RoleName;

            var claims = new List<Claim>
{
    new Claim(ClaimTypes.Name, user.Name),
    new Claim(ClaimTypes.Email, user.Email),
    new Claim(ClaimTypes.Role, role),
    new Claim("UserId", user.UserId.ToString())
};

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
