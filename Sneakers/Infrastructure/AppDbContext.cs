using Microsoft.EntityFrameworkCore;

namespace Sneakers.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet <EMPLOYEE> EMPLOYEE { get; set; }
        public DbSet<WAREHOUSE> WAREHOUSE { get; set; }
        public DbSet<SNEAKERS> SNEAKERS { get; set; }
        public DbSet<SNEAKERS_TYPE> SNEAKERS_TYPE { get; set; }
        public DbSet<SNEAKERS_BRAND> SNEAKERS_BRAND { get; set; }
        public DbSet<SNEAKERS_MODEL> SNEAKERS_MODEL { get; set; }
        public DbSet<SIZE> SIZE { get; set; }
        public DbSet<SIZE_SNEAKERS_CONNECTION> SIZE_SNEAKERS_CONNECTION { get; set; }




    }
}
