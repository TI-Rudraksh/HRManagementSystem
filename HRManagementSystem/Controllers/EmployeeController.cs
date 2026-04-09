using HRManagementSystem.Data;
using HRManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace HRManagementSystem.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly DB _context;

        public EmployeeController(DB context)
        {
            _context = context;
        }

        public IActionResult Profile()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var employee = _context.Employees
                .FirstOrDefault(e => e.UserId == userId);

            if (employee == null)
            {
                employee = new Employee
                {
                    UserId = userId
                };
            }

            ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name");

            return View(employee);
        }

        [HttpPost]
        public IActionResult Profile(Employee model, IFormFile PhotoFile)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var employee = _context.Employees
                .FirstOrDefault(e => e.UserId == userId);

            if (PhotoFile != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(PhotoFile.FileName);
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    PhotoFile.CopyTo(stream);
                }

                model.Photo = "/images/" + fileName;
            }

            if (employee == null)
            {
                model.UserId = userId;
                _context.Employees.Add(model);
            }
            else
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.PhoneNumber = model.PhoneNumber;
                employee.Salary = model.Salary;
                employee.BankDetails = model.BankDetails;
                employee.DepartmentId = model.DepartmentId;

                if (model.Photo != null)
                    employee.Photo = model.Photo;
            }

            _context.SaveChanges();

            return RedirectToAction("Profile");
        }
    }
}
