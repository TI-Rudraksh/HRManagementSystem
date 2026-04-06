using HRManagementSystem.Data;
using HRManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRManagementSystem.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly DB _context;

        public DepartmentController(DB context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var departments = await _context.Departments.ToListAsync();
            return View(departments);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Departments.Add(department);
                await _context.SaveChangesAsync();

                _context.Notifications.Add(new Notification
                {
                    Message = $"New department '{department.DepartmentName}' added.",
                    Icon = "bi-buildings-fill",
                    Color = "text-success"
                });
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var dept = await _context.Departments.FindAsync(id);
            if (dept == null)
            {
                return NotFound();
            }
            return View(dept);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Department dept)
        {
            if (!ModelState.IsValid)
            {
                return View(dept);
            }


            _context.Departments.Update(dept);

            _context.Notifications.Add(new Notification
            {
                Message = $"Department '{dept.DepartmentName}' updated successfully.",
                Icon = "bi-pencil-fill",
                Color = "text-warning"
            });

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
