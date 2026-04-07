using Microsoft.AspNetCore.Mvc;

namespace HRManagementSystem.Controllers
{
    public class PayrollController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult Create() => View();
    }

    public class AppraisalController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult Create() => View();
    }

    public class ReimbursementController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult Create() => View();
    }

    public class RolesController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult Create() => View();
        public IActionResult Assign() => View();
    }
}
