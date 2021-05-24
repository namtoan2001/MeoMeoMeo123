using System;
using System.Web.Mvc;
using MeoMeoMeo.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject
{
    [TestClass]
    public class LoginTest
    {
        [TestMethod]
        public void CheckLogin_WithNameandPassword()
        {

            //Arrange
            DangNhapController controller = new DangNhapController();
            //Action
            ViewResult result = controller.Login() as ViewResult;
            //Assert
            Assert.IsNotNull(result);
        }
    }
}
