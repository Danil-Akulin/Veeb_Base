using Microsoft.EntityFrameworkCore;
using veeb2.Models;

namespace veeb2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Tooted> Toodes { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
