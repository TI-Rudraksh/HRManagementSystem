using System.ComponentModel.DataAnnotations;

namespace CrudOperation_1.Models
{
    public class Department
    {
        [Key]
        public int Dept_Id { get; set; }
        [Required , StringLength(100)]
        public string DepartmentName { get; set; }
    }
}
