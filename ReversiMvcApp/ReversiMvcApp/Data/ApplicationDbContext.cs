using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ReversiMvcApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed roles
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "Speler", NormalizedName = "SPELER" },
                new IdentityRole { Id = "2", Name = "Mediator", NormalizedName = "MEDIATOR" },
                new IdentityRole { Id = "3", Name = "Beheerder", NormalizedName = "BEHEERDER" }
            );
        }
    }
}