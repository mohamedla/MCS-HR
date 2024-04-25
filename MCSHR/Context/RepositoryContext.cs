using MCSHR.Models;
using Microsoft.EntityFrameworkCore;

namespace MCSHR.Context
{
    public class RepositoryContext: DbContext
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
        { }

        public DbSet<Employee> employees { get; set; }
    }
}
