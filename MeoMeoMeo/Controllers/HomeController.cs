using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MeoMeoMeo.Models;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace MeoMeoMeo.Controllers
{
    public class HomeController : Controller
    {
        private CT25Team28Entities db = new CT25Team28Entities();
        public ActionResult Index()
        {
            return View();

        }
        public ActionResult SanPham()
        {
            var sp = db.SanPhams.Include(s => s.LoaiSP);
            return View(sp.ToList());
        }
    }
}