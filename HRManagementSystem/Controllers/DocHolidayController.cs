using Microsoft.AspNetCore.Mvc;

namespace HRManagementSystem.Controllers
{
    public class DocumentsController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult Create() => View();
    }

    public class HolidaysController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult Create() => View();
    }
}
