using Xunit;
using xManik.Data;
using xManik.Controllers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using xManik.Models;
using System;

namespace xManikTest.ControllersTest
{
    public class ProvidersControllerTest
    {
        private static ProvidersController controller;
        private static ApplicationDbContext context;

        static ProvidersControllerTest()
        {
            InitializeControllerAndContext();
        }

        private static void InitializeControllerAndContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase("aspnet-xManik-AF8D5E9A-D650-4030-8051-C883BE4C81AC");
            context = new ApplicationDbContext(optionsBuilder.Options);
            InitializeContextWithDefaultData();
            controller = new ProvidersController(context);
        }

        private static void InitializeContextWithDefaultData()
        {
            var provider = new Provider()
            {
                Id = "123",
                Description = "test description",
                User = null,
                Portfolio = null,
                Marker = null,
                Rate = 33,
                Services = null

            };

            var user = new ApplicationUser()
            {
                Id = "123",
                UserName = "TestUserName",
                DateRegistered = DateTime.Now,
                Role = UserRole.Provider,
                Email = "test@gamil.com",
                Provider = provider
            };

            context.Users.Add(user);
            context.SaveChanges();
        }

        #region IndexTests
        // TODO : add tests for Providers/Index
        #endregion

        #region DetailsTests

        [Fact]
        public async Task Details_InvalidId_StatusNotFound()
        {
            var res = ((await controller.Details("not real id")) as StatusCodeResult);
            res.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task Details_ValidId_ExistingModel()
        {
            var model = ((await controller.Details("123")) as ViewResult).ViewData.Model;
            model.Should().NotBeNull().
                And.BeSameAs(await context.Providers.FirstOrDefaultAsync());
            model.As<Provider>().User.Should().NotBeNull();
            model.As<Provider>().User.Should().BeOfType<ApplicationUser>();
            model.As<Provider>().Id.Should().Be("123");
        }

        #endregion

        #region DeleteTests

        [Fact]
        public async Task DeleteConfirmed_ValidId_DeleteProviderById()
        {
            await controller.DeleteConfirmed("123");
            (await context.Providers.FindAsync("123")).Should().BeNull();
        }

        [Fact]
        public void DeleteConfirmed_InvalidId_ThrowApplicationException()
        {
            controller.Invoking(p => p.DeleteConfirmed("invalid id").Wait()).Should().Throw<ApplicationException>();
        }

        #endregion

    }
}
