using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebStore.Controllers;
using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void Index_Returns_View()
        {
            //A-A-A = Arrange - Act - Assert

            var controller = new HomeController();

            var result = controller.Index();
            
            //Assert.IsInstanceOfType(result,typeof(ViewResult)); // MSTest

            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void SecondAction_Returns_View()
        {
            //A-A-A = Arrange - Act - Assert

            var controller = new HomeController();

            var expected_content = "Second action";

            var result = controller.SecondAction();

            //Assert.IsInstanceOfType(result,typeof(ViewResult)); // MSTest

           var content_result = Assert.IsType<ContentResult>(result);

           Assert.Equal(expected_content, content_result.Content);
        }

        [TestMethod]
        public void Blogs_Returns_View()
        {
            //A-A-A = Arrange - Act - Assert

            var controller = new HomeController();

            var result = controller.Blogs();

            //Assert.IsInstanceOfType(result,typeof(ViewResult)); // MSTest

            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void BlogSingle_Returns_View()
        {
            //A-A-A = Arrange - Act - Assert

            var controller = new HomeController();

            var result = controller.BlogSingle();

            //Assert.IsInstanceOfType(result,typeof(ViewResult)); // MSTest

            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void Contacts_Returns_View()
        {
            //A-A-A = Arrange - Act - Assert

            var controller = new HomeController();

            var result = controller.ContactUs();

            //Assert.IsInstanceOfType(result,typeof(ViewResult)); // MSTest

            Assert.IsType<ViewResult>(result);
        }

        [TestMethod, ExpectedException(typeof(ApplicationException))]
        public void Throw_Thrown_ApplicationException()
        {
            var controller = new HomeController();
            
            var expected_exception_message = "Test";

            var result = controller.Throw(expected_exception_message);

        }
    }
}
