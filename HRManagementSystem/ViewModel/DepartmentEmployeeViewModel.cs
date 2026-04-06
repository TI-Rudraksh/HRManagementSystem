using HRManagementSystem.Models;

namespace HRManagementSystem.ViewModel
{
    public class DepartmentEmployeeViewModel
    {
        public string DepartmentName { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
