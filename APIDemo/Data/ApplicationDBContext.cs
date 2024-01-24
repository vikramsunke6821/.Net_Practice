using Microsoft.EntityFrameworkCore;

namespace APIDemo.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) :  base(options)
        {
        }

        public DbSet<StudentModel> StudentTable {  get; set; }
    }
}
