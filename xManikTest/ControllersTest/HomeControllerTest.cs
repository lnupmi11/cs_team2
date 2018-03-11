using Microsoft.AspNetCore.Mvc;
using xManik.Controllers;
using Xunit;

namespace xManikTest.ControllersTests
{
    public class HomeControllerTest
    {
        private readonly HomeController controller;

        public HomeControllerTest()
        {
            controller = new HomeController();
        }

        [Fact]
        public void Index_ViewName_Null()
        {
            var result = controller.Index() as ViewResult;
            Assert.Null(result.ViewName);
        }

        [Fact]
        public void About_ViewInformation_MessageFromView()
        {
            var result = controller.About() as ViewResult;
            Assert.Equal("Your application description page.", result.ViewData["Message"]);
        }

        [Fact]
        public void Contact_ViewInformation_MessageFromViewAndNullModel()
        {
            var result = controller.Contact() as ViewResult;
            Assert.Equal("Your contact page.", result.ViewData["Message"]);
            Assert.Null(result.Model);
        }
    }
}
