using CrudApp.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudApp.Data
{
    public class CrudAppContext : DbContext
    {
        public CrudAppContext(DbContextOptions<CrudAppContext> options)
            : base(options)
        {
        }

        public DbSet<CrudApp.Model.User> User { get; set; } = default!;
    }
}
