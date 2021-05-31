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
using System.Data.SqlClient;
using System.Configuration;
using DocumentFormat.OpenXml.Office.CustomUI;

namespace MeoMeoMeo.Controllers
{
    public class HomeController : Controller
    {
        private CT25Team28Entities db = new CT25Team28Entities();
        private const string CartSession = "cart";
        public ActionResult Index()
        {
            return View();

        }
        public ActionResult SanPham()
        {
            var sp = db.SanPhams.Include(s => s.LoaiSP);
            return View(sp.ToList());
        }
        [HttpPost]
        public ActionResult sanPham(string search)
        {
            var link = from l in db.SanPhams 
                        select l;
            if (!String.IsNullOrEmpty(search)) 
            {
                link = link.Where(s => s.TenSP.Contains(search));
            }
            return View(link);
        }
        public ActionResult Logout()
        {
            Session["TenDangNhap"] = null;
            return RedirectToAction("/");
        }
        public ActionResult chitietSP(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }
        
    }
}