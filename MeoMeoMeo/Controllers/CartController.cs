using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MeoMeoMeo.Models;

namespace MeoMeoMeo.Controllers
{
    public class CartController : Controller
    {
        CT25Team28Entities db = new CT25Team28Entities();
        private List<ChiTietDH> cart = null;
        public CartController()
        {
            var Session = System.Web.HttpContext.Current.Session;
            if (Session["cart"] != null)
                cart = Session["cart"] as List<ChiTietDH>;
            else
            {
                cart = new List<ChiTietDH>();
                Session["cart"] = cart;
            }
        }

        // GET: Cart
        public ActionResult Index()
        {
            var hashtable = new Hashtable();
            foreach(var ChitietDH in cart)
            {
                if (hashtable[ChitietDH.SanPham.MaSP] != null)
                {
                    (hashtable[ChitietDH.SanPham.MaSP] as ChiTietDH).SL += ChitietDH.SL;
                }
                else
                {
                    hashtable[ChitietDH.SanPham.MaSP] = ChitietDH;
                }
            }
            cart.Clear();
            foreach(ChiTietDH chitietDH in hashtable.Values)
            {
                cart.Add(chitietDH);
            }
            ViewBag.Success = "Không có sản phẩm nào trong giỏ hàng";
            return View(cart);
        }

        [HttpPost]
        public ActionResult AddtoCart(int productid,int soluong=1)
        {
            var product = db.SanPhams.Find(productid);
            cart.Add(new ChiTietDH
            {
                SanPham = product,
                SL = soluong
            });
            return RedirectToAction("Index");
        }

        public ActionResult UpdateCart([FromForm] int productid, [FromForm] int soluong)
        {
            cart = (List<ChiTietDH>)Session["cart"];
            var product = cart.Find(p => p.SanPham.MaSP == productid);
            if (product != null)
            {
                product.SL = soluong;
            }
            Session["cart"] = cart;
            return RedirectToAction("Index");
        }
        // GET: Cart/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDH chiTietDH = db.ChiTietDHs.Find(id);
            if (chiTietDH == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDH = new SelectList(db.DonHangs, "MaDH", "Tinhtrang", chiTietDH.MaDH);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", chiTietDH.MaSP);
            return View(chiTietDH);
        }

        // POST: Cart/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDH,MaSP,LoaiSP,TenSP,SL,Gia")] ChiTietDH chiTietDH)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietDH).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDH = new SelectList(db.DonHangs, "MaDH", "Tinhtrang", chiTietDH.MaDH);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", chiTietDH.MaSP);
            return View(chiTietDH);
        }
        public ActionResult xoaSP(int id)
        {
            cart = (List<ChiTietDH>)Session["cart"];
            var product = cart.Find(p => p.SanPham.MaSP == id);
            if (product != null)
            {
                cart.Remove(product);
            }
            Session["cart"] = cart;
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
