using HRManagementSystem.Data;
using HRManagementSystem.Hepler;
using HRManagementSystem.Models;
using HRManagementSystem.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HRManagementSystem.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly DB _context;

        public UsersController(DB context)
        {
            _context = context;
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult Index()
        {
            var users = _context.Users
                .Include(u => u.Role) 
                .ToList();

            return View(users);
        }

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            ViewBag.Roles = new SelectList(_context.Roles, "RoleId", "RoleName");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create(CreateUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = new SelectList(_context.Roles, "RoleId", "RoleName");
                return View(model);
            }

            if (_context.Users.Any(x => x.Email == model.Email))
            {
                ModelState.AddModelError("", "Email already exists");
                ViewBag.Roles = new SelectList(_context.Roles, "RoleId", "RoleName");
                return View(model);
            }

            var user = new User
            {
                Name = model.Name,
                Email = model.Email,
                PasswordHash = PasswordHelper.Encrypt(model.Password),
                RoleId = model.RoleId
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


    }
}