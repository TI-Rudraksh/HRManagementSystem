using HRManagementSystem.Data;
using HRManagementSystem.Models;
using HRManagementSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HRManagementSystem.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly DB _context;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(DB context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await _context.Employees.Include(e => e.Department).ToListAsync();
            return View(employees);
        }


        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = new SelectList(_context.Departments, "Dept_Id", "DepartmentName");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee emp)
        {
            if (ModelState.IsValid)
            {
                if (emp.ImageFile != null)
                {
                    string folder = Path.Combine(_env.WebRootPath, "employeeImages");

                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(emp.ImageFile.FileName);

                    string filePath = Path.Combine(folder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        emp.ImageFile.CopyTo(stream);
                    }

                    emp.Emp_Image = "/employeeImages/" + fileName;
                }

                _context.Employees.Add(emp);
                _context.SaveChanges();

                _context.Notifications.Add(new Notification
                {
                    Message = $"New employee '{emp.Emp_Name}' added successfully.",
                    Icon = "bi-person-plus-fill",
                    Color = "text-success"
                });
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.Departments = new SelectList(_context.Departments, "Dept_Id", "DepartmentName");

            return View(emp);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp == null)
            {
                return NotFound();
            }
            ViewBag.Departments = new SelectList(_context.Departments, "Dept_Id", "DepartmentName", emp.Dept_Id);
            return View(emp);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Employee emp)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Departments = new SelectList(_context.Departments, "Dept_Id", "DepartmentName", emp.Dept_Id);
                return View(emp);
            }

            var existing = await _context.Employees.FindAsync(emp.Emp_Id);
            if (existing == null) return NotFound();

            if (emp.ImageFile != null)
            {
                string folder = Path.Combine(_env.WebRootPath, "employeeImages");
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                string fileName = Guid.NewGuid() + Path.GetExtension(emp.ImageFile.FileName);
                using var stream = new FileStream(Path.Combine(folder, fileName), FileMode.Create);

                await emp.ImageFile.CopyToAsync(stream);
                existing.Emp_Image = "/employeeImages/" + fileName;
            }

            existing.Emp_Name = emp.Emp_Name;
            existing.Emp_Age = emp.Emp_Age;
            existing.Emp_Email = emp.Emp_Email;
            existing.Emp_Gender = emp.Emp_Gender;
            existing.Emp_Mobile = emp.Emp_Mobile;
            existing.Emp_Salary = emp.Emp_Salary;
            existing.Emp_Status = emp.Emp_Status;
            existing.Dept_Id = emp.Dept_Id;

            _context.Notifications.Add(new Notification
            {
                Message = $"Employee '{emp.Emp_Name}' updated successfully.",
                Icon = "bi-pencil-fill",
                Color = "text-warning"
            });

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult DepartmentWiseEmployees()
        {
            var data = _context.Departments
                .Select(d => new DepartmentEmployeeViewModel
                {
                    DepartmentName = d.DepartmentName,
                    Employees = _context.Employees
                        .Where(e => e.Dept_Id == d.Dept_Id)
                        .ToList()
                }).ToList();

            return View(data);
        }
    }
}
