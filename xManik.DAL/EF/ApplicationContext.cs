using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Entity;
using xManik.DAL.Entities;

namespace xManik.DAL.EF
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Service> Services { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<PortfolioItem> PortfolioItems {get;set;}

        static ApplicationContext()
        {
            Database.SetInitializer<ApplicationContext>(new StoreDbInitializer());
        }
        public ApplicationContext(string connectionString)
            : base(connectionString)
        {
        }
    }

    public class StoreDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationContext>
    {
        protected override void Seed(ApplicationContext db)
        {
            db.Services.Add(new Service { Description = "Some service", Duration = new TimeSpan(30), DatePublished = DateTime.Now, Price = 700, IsPromoted = false });
            db.Services.Add(new Service { Description = "Another service", Duration = new TimeSpan(100), DatePublished = DateTime.Now, Price = 1000, IsPromoted = true });
            db.Services.Add(new Service { Description = "One more service", Duration = new TimeSpan(50), DatePublished = DateTime.Now, Price = 100, IsPromoted = false });
            db.SaveChanges();
        }
    }
}
