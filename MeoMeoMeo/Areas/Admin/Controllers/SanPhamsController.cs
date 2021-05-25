﻿using System;
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

namespace MeoMeoMeo.Areas.Admin.Controllers
{

    public class SanPhamsController : Controller
    {
        CT25Team28Entities db = new CT25Team28Entities();

        // GET: Admin/SanPhams
        public ActionResult Index()
        {
            var sanPhams = db.SanPhams.Include(s => s.LoaiSP);
            return View(sanPhams.ToList());
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
                //db.SanPhams.Add(sanPham);
                //db.SaveChanges();
                //img.SaveAs("E:/Project C#/MeoMeoMeo/MeoMeoMeo/IMG SANPHAM/" + sanPham.MaSP.ToString() + sanPham.Hinh_anh);
                //img.SaveAs(Server.MapPath(Url.Content("~/IMG SANPHAM/" + sanPham.MaSP.ToString() + sanPham.Hinh_anh)));
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
