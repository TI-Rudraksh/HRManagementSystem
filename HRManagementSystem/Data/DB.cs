using HRManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HRManagementSystem.Data
{
    public class DB : DbContext
    {
        public DB(DbContextOptions<DB> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Notification> Notifications { get; set; }
    }
}
