using Microsoft.EntityFrameworkCore;

namespace EFDemo.Models.Domain
{
    public class EmpDemo : DbContext
    {
        public EmpDemo(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
 
    }
}
