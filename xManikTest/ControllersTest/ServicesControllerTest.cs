using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using xManik.Controllers;
using xManik.Data;
using xManik.Models;
using Xunit;

namespace xManikTest.ControllersTest
{
    public class ServicesControllerTest
    {
        private static ServicesController controller;
        private static ApplicationDbContext context;

        static ServicesControllerTest()
        {
            InitializeControllerAndContext();
        }

        private static void InitializeControllerAndContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase("aspnet-xManik-AF8D5E9A-D650-4030-8051-C883BE4C81AC");
            context = new ApplicationDbContext(optionsBuilder.Options);
            InitializeContextWithDefaultData();
            controller = new ServicesController(context);
        }

        private static void InitializeContextWithDefaultData()
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

        #region IndexTests

        [Fact]
        public void Index_NoUser_ThrowApplicationException()
        {
            controller.Invoking(p => p.Index().Wait()).Should().Throw<ApplicationException>();
        }

        #endregion
    }
}
