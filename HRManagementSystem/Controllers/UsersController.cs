using Microsoft.AspNetCore.Mvc;

namespace HRManagementSystem.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }
    }
}
