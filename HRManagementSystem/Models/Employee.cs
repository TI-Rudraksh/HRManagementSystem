using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRManagementSystem.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public string? PhoneNumber { get; set; }
        public string? Photo { get; set; }
        public decimal Salary { get; set; }
        public string? BankDetails { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
