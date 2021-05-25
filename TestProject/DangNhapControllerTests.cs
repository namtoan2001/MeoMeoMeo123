using System;
using System.Web.Mvc;
using MeoMeoMeo.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MeoMeoMeo.Models;
using Moq;

namespace TestProject
{
    [TestClass]
    public class DangNhapControllerTests
    {
        [TestMethod]
        public void CheckLogin_WithNameandPassword_Success()
        {
            //Arrange
            DangNhapController controller = new DangNhapController();
            LoginModel login = new LoginModel() {
                TenDangNhap = "Admin",
                MatKhau = "Admin"
            };
            var mock = new Mock<ControllerContext>();
            var mockSession = new Mock<System.Web.HttpSessionStateBase>();
            mock.Setup(p => p.HttpContext.Session).Returns(mockSession.Object);
            controller.ControllerContext = mock.Object;
            //Action
            var result = controller.Login(login) as RedirectToRouteResult;
            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }
    }
}
