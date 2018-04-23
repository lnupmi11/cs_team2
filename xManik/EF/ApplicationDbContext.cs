using xManik.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using xManik.Models.BloggerViewModels;

namespace xManik.EF
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>()
                .HasOne(e => e.UserProfile)
                .WithOne(e => e.ApplicationUser)
                .HasForeignKey<UserProfile>(e => e.Id)
                .IsRequired();
        }

        public DbSet<Assigment> Assigments { get; set; }
        public DbSet<Chanel> Chanels { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<News> News { get; set; }
    }
}
