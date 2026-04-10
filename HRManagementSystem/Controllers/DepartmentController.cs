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
            return View(await _context.Departments.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Departments.Add(department);

                _context.Notifications.Add(new Notification
                {
                    Message = $"New Department '{department.DepartmentName}' added",
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
            if (dept == null) return NotFound();

            return View(dept);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Department dept)
        {
            if (ModelState.IsValid)
            {
                _context.Departments.Update(dept);

                _context.Notifications.Add(new Notification
                {
                    Message = $"Department '{dept.DepartmentName}' updated",
                    Icon = "bi-pencil-fill",
                    Color = "text-warning"
                });

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dept);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var dept = await _context.Departments.FindAsync(id);
            if (dept != null)
            {
                _context.Departments.Remove(dept);

                _context.Notifications.Add(new Notification
                {
                    Message = $"Department '{dept.DepartmentName}' deleted",
                    Icon = "bi-trash-fill",
                    Color = "text-danger"
                });

                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}