using System;
using System.Web.Mvc;
using MeoMeoMeo.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MeoMeoMeo.Models;
using Moq;
namespace TestProject
{
    /// <summary>
    /// Summary description for DangKiTests
    /// </summary>
    [TestClass]
    public class DangKiTests
    {
        public DangKiTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMethod1()
        {
            DangNhapController controller = new DangNhapController();
            LoginModel login = new LoginModel()
            {
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
