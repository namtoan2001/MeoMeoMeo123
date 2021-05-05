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

namespace MeoMeoMeo.Areas.Admin.Controllers
{
    public class SanPhamsController : Controller
    {
        private CT25Team28Entities db = new CT25Team28Entities();

        // GET: Admin/SanPhams
        public ActionResult Index()
        {
            var sanPhams = db.SanPhams.Include(s => s.LoaiSP);
            return View(sanPhams.ToList());
        }

        // GET: Admin/SanPhams/Details/5
        public ActionResult Details(int? id)
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

        // GET: Admin/SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.Maloai = new SelectList(db.LoaiSPs, "MaLoai", "TenLoai");
            return View();
        }

        // POST: Admin/SanPhams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,TenSP,Maloai,SL,Gia,Mota,Hinh_anh")] SanPham sanPham, IEnumerable<HttpPostedFileBase> files)
        {
            //string filename = Path.GetFileName(sanPham.imgfile.FileName);
            //string fileExtention = Path.GetExtension(sanPham.imgfile.FileName);
            //filename = DateTime.Now.ToString("yyyyMMdd") + "-" + filename.Trim() + fileExtention;
            //string UploadPath = ConfigurationManager.AppSettings["UserImagePath"].ToString();
            //sanPham.Hinh_anh = UploadPath + filename;
            //sanPham.imgfile.SaveAs(sanPham.Hinh_anh);


            foreach (var file in files)
            {
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/IMG SANPHAM"), fileName);
                    file.SaveAs(path);
                }
            }


            if (ModelState.IsValid)
            { 
                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Maloai = new SelectList(db.LoaiSPs, "MaLoai", "TenLoai", sanPham.Maloai);

            //string filename = Path.GetFileNameWithoutExtension(sanPham.imgfile.FileName);
            //string fileExtention = Path.GetExtension(sanPham.imgfile.FileName);
            //filename = DateTime.Now.ToString("yyyyMMdd") + "-" + filename.Trim() + fileExtention;
            //string UploadPath = ConfigurationManager.AppSettings["UserImagePath"].ToString();
            //sanPham.Hinh_anh = UploadPath + filename;
            //sanPham.imgfile.SaveAs(sanPham.Hinh_anh);

            return View(sanPham);
        }

        // GET: Admin/SanPhams/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.Maloai = new SelectList(db.LoaiSPs, "MaLoai", "TenLoai", sanPham.Maloai);
            return View(sanPham);
        }

        // POST: Admin/SanPhams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,TenSP,Maloai,SL,Gia,Mota,Hinh_anh")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Maloai = new SelectList(db.LoaiSPs, "MaLoai", "TenLoai", sanPham.Maloai);
            return View(sanPham);
        }

        // GET: Admin/SanPhams/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Admin/SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
