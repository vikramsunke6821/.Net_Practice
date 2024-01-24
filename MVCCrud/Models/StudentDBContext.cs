using Microsoft.EntityFrameworkCore;
using MVCCrud.Models.Domain;


namespace MVCCrud.Models
{
    public class StudentDBContext : DbContext
    {
        public StudentDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Student> Studenttable {  get; set ; }
    }
}
 