using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MeoMeoMeo.Controllers;
using System.Web.Mvc;
using System.Collections.Generic;
using MeoMeoMeo.Models;
using System.Linq;

namespace TestProject
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void TestSanPham()
        {
            var controller = new HomeController();
            var result = controller.sanPham() as ViewResult;
            var model = result.Model as List<SanPham>;
            Assert.IsNotNull(result);
            Assert.IsNotNull(model);

            var db = new CT25Team28Entities();
            Assert.AreEqual(db.SanPhams.Count(),model.Count);
        }
    }
}
