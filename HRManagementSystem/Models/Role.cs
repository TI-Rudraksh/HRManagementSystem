using System.ComponentModel.DataAnnotations;

namespace HRManagementSystem.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        public string RoleName { get; set; }
    }
}
