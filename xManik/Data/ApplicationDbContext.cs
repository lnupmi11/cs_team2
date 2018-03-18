﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using xManik.Models;

namespace xManik.Data
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Artwork> Artworks { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<Provider>()
      .HasMany(c => c.Services)
      .WithOne(e => e.Provider);

            builder.Entity<ApplicationUser>()
        .HasOne(a => a.Client)
        .WithOne(b => b.User)
        .HasForeignKey<Client>(b => b.Id);

            builder.Entity<ApplicationUser>()
       .HasOne(a => a.Provider)
       .WithOne(b => b.User)
       .HasForeignKey<Provider>(b => b.Id);
        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
    }
}
