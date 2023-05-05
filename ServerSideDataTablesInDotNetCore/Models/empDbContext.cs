using Microsoft.EntityFrameworkCore;

namespace ServerSideDataTablesInDotNetCore.Models
{
    public class empDbContext : DbContext
    {
        public empDbContext(DbContextOptions options) : base (options)  {}
        public DbSet<Employees> Employees  { get; set; }

    }
}
