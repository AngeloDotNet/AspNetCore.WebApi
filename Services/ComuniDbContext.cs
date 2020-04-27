using AEP_WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AEP_WebApi.Services
{
    public class ComuniDbContext : DbContext
    {
        public ComuniDbContext(DbContextOptions<ComuniDbContext> options) : base(options)
        {
        }
        public virtual DbSet<Comuni> Comuni { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comuni>()
                .HasKey(a => new { a.IdComune });
        }
    }
}