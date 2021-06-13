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
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Services.UserAccountMapping;
using PagedList;

namespace MeoMeoMeo.Areas.Admin.Controllers
{

    public class SanPhamsController : Controller
    {
        CT25Team28Entities db = new CT25Team28Entities();

        // GET: Admin/SanPhams
        public ActionResult Index(int? page)
        {
            if (page == null) page = 1;
            var links = (from l in db.SanPhams
                         select l).OrderBy(x => x.MaSP);
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(links.ToPagedList(pageNumber, pageSize));
        }
        [HttpPost]
        public ActionResult Index(string search)
        {
            var link = from l in db.SanPhams
                       select l;
            if (!String.IsNullOrEmpty(search))
            {
                link = link.Where(s => s.TenSP.Contains(search));
            }
            return View(link);
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
        public ActionResult Create([Bind(Include = "MaSP,TenSP,Maloai,SL,Gia,Mota,Hinh_anh")] SanPham sanPham, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                LoaiSP sp = new LoaiSP();
                try
                {
                    if (img != null)
                    {
                        string fileName = System.IO.Path.GetFileName(img.FileName);
                        string filePath = "~/IMG SANPHAM/" + fileName;
                        img.SaveAs(Server.MapPath(filePath));
                        db.SanPhams.Add(new SanPham
                        {
                            TenSP = sanPham.TenSP,
                            Mota = sanPham.Mota,
                            SL = sanPham.SL,
                            Gia = sanPham.Gia,
                            Maloai = sanPham.Maloai,
                            Hinh_anh = fileName
                        });
                        db.SaveChanges();
                        return RedirectToAction("Index");

                    }
                    ViewBag.FileStatus = "File uploaded successfully.";
                }
                catch (Exception)
                {
                }
                
            }

            ViewBag.Maloai = new SelectList(db.LoaiSPs, "MaLoai", "TenLoai", sanPham.Maloai);

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
        public ActionResult Edit([Bind(Include = "MaSP,TenSP,Maloai,SL,Gia,Mota,Hinh_anh")] SanPham sanPham, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                var model = db.SanPhams.Find(sanPham.MaSP);
                string oldfilePath =model.Hinh_anh;
                if (img != null && img.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(img.FileName);
                    string path = System.IO.Path.Combine(
                    Server.MapPath("~/IMG SANPHAM/"), fileName);
                    img.SaveAs(path);
                    model.Hinh_anh = img.FileName;
                    string fullPath = Request.MapPath("~/IMG SANPHAM/" + oldfilePath);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                }
                model.TenSP = sanPham.TenSP;
                model.Mota = sanPham.Mota;
                model.SL = sanPham.SL;
                model.Gia = sanPham.Gia;
                model.Maloai = sanPham.Maloai;
                
                //db.Entry(sanPham).State = EntityState.Modified;
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
            var model = db.SanPhams.Find(sanPham.MaSP);
            string oldfilePath = model.Hinh_anh;
            string fullPath = Request.MapPath("~/IMG SANPHAM/" + oldfilePath);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
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
