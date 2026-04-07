using Microsoft.AspNetCore.Mvc;

namespace HRManagementSystem.Controllers
{
    public class AttendanceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
