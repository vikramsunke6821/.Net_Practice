using EFDemo.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EFDemo.Data
{
    public class MVCDemo : DbContext
    {
        public MVCDemo(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
