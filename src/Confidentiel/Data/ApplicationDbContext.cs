using Confidentiel.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Confidentiel.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Secret> Secrets { get; set; }

        public ApplicationDbContext(DbContextOptions options) : 
            base(options)
        {
        }
    }
}
