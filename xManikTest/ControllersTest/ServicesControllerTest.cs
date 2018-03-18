using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using xManik.Controllers;
using xManik.Data;
using xManik.Models;
using Xunit;

namespace xManikTest.ControllersTest
{
    public class ServicesControllerTest
    {
        //New context for each test case
        private DbContextOptions<ApplicationDbContext> GetDbContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase("aspnet-xManik-AF8D5E9A-D650-4030-8051-C883BE4C81AC")
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        private async Task<ApplicationDbContext> GetInitializedContextWithDefaultDataAsync()
        {
            ApplicationDbContext context = new ApplicationDbContext(GetDbContextOptions());
            var service = new Service() { Id = "1", DatePublished = DateTime.Now, Description = "short description1", Duration = 1, Price = 1 };
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

            service.Provider = provider;
            provider.Services.Add(service);
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
            await context.SaveChangesAsync();
            return context;
        }

        #region IndexTests

        [Fact]
        public void Index_NoUser_ThrowApplicationException()
        {
            using (var context = new ApplicationDbContext(GetDbContextOptions()))
            {
                var controller = new ServicesController(context);
                controller.Invoking(p => p.Index().Wait()).Should().Throw<ApplicationException>();
            }
        }

        [Fact]
        public async Task Index_InvalidNameIdentifier_ReturnNotFound()
        {
            using (var context = await GetInitializedContextWithDefaultDataAsync())
            {
                var controller = new ServicesController(context);
                var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                 new Claim(ClaimTypes.NameIdentifier, "testId"),
                 new Claim(ClaimTypes.Name, "testName")
                }));
                var httpContextMock = new Mock<HttpContext>();
                httpContextMock.Setup(p => p.User).Returns(user);
                controller.ControllerContext.HttpContext = httpContextMock.Object;

                var result = ((await controller.Index()) as StatusCodeResult);
                result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
            }
        }

        [Fact]
        public async Task Index_ValidNameIdentifier_ReturnExistingModel()
        {
            using (var context = await GetInitializedContextWithDefaultDataAsync())
            {
                var controller = new ServicesController(context);
                var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                 new Claim(ClaimTypes.NameIdentifier, "1123"),
                 new Claim(ClaimTypes.Name, "testName")
                }));
                var httpContextMock = new Mock<HttpContext>();
                httpContextMock.Setup(p => p.User).Returns(user);
                controller.ControllerContext.HttpContext = httpContextMock.Object;

                var model = ((await controller.Index()) as ViewResult).Model as List<Service>;
                model.Count.Should().Be(1);
                model[0].Id.Should().Be("1");
                model[0].Provider.Id.Should().Be("1123");
                model[0].Price.Should().Be(1);
            }
        }

        #endregion

        #region DetailsTests

        [Fact]
        public void Details_NoUser_ThrowApplicationException()
        {
            using (var context = new ApplicationDbContext(GetDbContextOptions()))
            {
                var controller = new ServicesController(context);
                controller.Invoking(p => p.Details("").Wait()).Should().Throw<ApplicationException>();
            }
        }

        [Fact]
        public async Task Details_InvalidNameIdentifier_ReturnNotFound()
        {
            using (var context = await GetInitializedContextWithDefaultDataAsync())
            {
                var controller = new ServicesController(context);
                var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                 new Claim(ClaimTypes.NameIdentifier, "testId"),
                 new Claim(ClaimTypes.Name, "testName")
                }));
                var httpContextMock = new Mock<HttpContext>();
                httpContextMock.Setup(p => p.User).Returns(user);
                controller.ControllerContext.HttpContext = httpContextMock.Object;

                var result = ((await controller.Index()) as StatusCodeResult);
                result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
            }
        }

        [Fact]
        public async Task Details_ValidNameIdentifier_ReturnExistingModel()
        {
            using (var context = await GetInitializedContextWithDefaultDataAsync())
            {
                var controller = new ServicesController(context);
                var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                 new Claim(ClaimTypes.NameIdentifier, "1123"),
                 new Claim(ClaimTypes.Name, "testName")
                }));

                var httpContextMock = new Mock<HttpContext>();
                httpContextMock.Setup(p => p.User).Returns(user);
                controller.ControllerContext.HttpContext = httpContextMock.Object;

                var result = ((await controller.Details("32")) as StatusCodeResult);
                result.StatusCode.Should().Be(StatusCodes.Status404NotFound);

                var model = ((await controller.Details("1")) as ViewResult).Model as Service;
                model.Should().NotBeNull();
                model.Id.Should().Be("1");
                model.Provider.Id.Should().Be("1123");
                model.Price.Should().Be(1);
            }
        }

        #endregion

        #region CreateTests

        [Fact]
        public void Create_NoUser_ThrowApplicationException()
        {
            using (var context = new ApplicationDbContext(GetDbContextOptions()))
            {
                Service service = new Service() { Price = 10, Description = "test", Duration = 12 };
                var controller = new ServicesController(context);
                controller.Invoking(p => p.Create(service).Wait()).Should().Throw<ApplicationException>();
            }
        }

        #endregion
    }
}
