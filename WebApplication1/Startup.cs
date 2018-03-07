using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using xManik.Data;
using xManik.Models;
using xManik.Services;


namespace xManik
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public async void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                           options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(
                config => config.SignIn.RequireConfirmedEmail = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration.GetSection("AppKeys"));
            services.AddMvc();

            await CreateRolesandUsersAsync(services.BuildServiceProvider());

        }

        
        private async Task CreateRolesandUsersAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.EnsureCreated();

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (!(await roleManager.RoleExistsAsync("Admin")))
            {
                var role = new IdentityRole
                {
                    Name = "Admin",
                   
                };
                await roleManager.CreateAsync(role);

                var user = new ApplicationUser
                {
                    UserName = Configuration.GetSection("UserSettings")["UserName"],
                    Email = Configuration.GetSection("UserSettings")["UserEmail"],
                    EmailConfirmed = true,
                    DateRegistered = DateTime.Now
                };

                string userPWD = Configuration.GetSection("UserSettings")["UserPassword"];

                var chkUser = await userManager.CreateAsync(user, userPWD);
                if (chkUser.Succeeded)
                {
                    var result1 = await userManager.AddToRoleAsync(user, "Admin");
                }
            }

            if (!(await roleManager.RoleExistsAsync("Client")))
            {
                var role = new IdentityRole
                {
                    Name = "Client"
                };
                await roleManager.CreateAsync(role);
            }

            if (!(await roleManager.RoleExistsAsync("Provider")))
            {
                var role = new IdentityRole
                {
                    Name = "Provider"
                };
                await roleManager.CreateAsync(role);
            }
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
