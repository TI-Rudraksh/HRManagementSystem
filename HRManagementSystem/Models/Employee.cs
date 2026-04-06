using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudOperation_1.Models
{
    public class Employee
    {
        [Key]
        public int Emp_Id { get; set; }

        [Required, StringLength(200)]
        public string Emp_Name { get; set; }

        [Required, Range(18, 65)]
        public int Emp_Age { get; set; }

        [Required]
        public string Emp_Gender { get; set; }

        [Required, StringLength(50)]
        public string Emp_Mobile { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int Emp_Salary { get; set; }

        [Required, StringLength(100)]
        public string Emp_Email { get; set; }

        public bool Emp_Status { get; set; }

        public string? Emp_Image { get; set; }


        [ForeignKey("Department")]
        [Required(ErrorMessage = "Please select department")]
        public int Dept_Id { get; set; }

        public Department? Department { get; set; }


        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
