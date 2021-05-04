using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MeoMeoMeo.Controllers
{
    public class DangNhapController : Controller
    {
        // GET: DangNhap
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Dangki()
        {
            return View();
        }
    }
}