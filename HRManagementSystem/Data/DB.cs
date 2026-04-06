using CrudOperation_1.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudOperation_1.Data
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
