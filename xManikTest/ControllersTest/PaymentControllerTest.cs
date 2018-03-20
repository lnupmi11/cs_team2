using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ObjectPool;
using System;
using System.Collections.Generic;
using System.Text;
using xManik.Controllers;
using xManik.Data;
using xManik.Models;
using Xunit;

namespace xManikTest.ControllersTest
{
    public class PaymentControllerTest
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly UserManager<ApplicationUser> _userManager;

        public PaymentControllerTest()
        {
            var serviceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

            var services = new ServiceCollection();
            services
                .AddDbContext<ApplicationDbContext>(b => b.UseInMemoryDatabase("aspnet-xManik-AF8D5E9A-D650-4030-8051-C883BE4C81AC").UseInternalServiceProvider(serviceProvider));
            _serviceProvider = services.BuildServiceProvider();
            _userManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        }

        [Fact]
        public async void Index_UserNotFound()
        {
            // Arrange
            var dbContext = _serviceProvider.GetRequiredService<ApplicationDbContext>();
            var controller = new PaymentController(dbContext, _userManager);

            // Act
            var result = await controller.Index(null) as ViewResult;

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        private static void InitializeContextWithDefaultData(ApplicationDbContext context)
        {
            var s = new Service() { Id = "1", DatePublished = DateTime.Now, Description = "short description1", Duration = 1, Price = 1 };
            var provider = new Provider()
            {
                Id = "1233",
                Description = "test description",
                User = null,
                Portfolio = null,
                Marker = null,
                Rate = 33,
                Services = new List<Service>()
            };
            s.Provider = provider;
            provider.Services.Add(s);
            var user = new ApplicationUser()
            {
                Id = "1123",
                UserName = "TestUserName",
                DateRegistered = DateTime.Now,
                Role = UserRole.Provider,
                Email = "test@gamil.com",
                Provider = provider
            };
            provider.User = user;
            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}
