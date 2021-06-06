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

        public ActionResult UpdateCart([Microsoft.AspNetCore.Mvc.FromForm] int productid, [Microsoft.AspNetCore.Mvc.FromForm] int soluong)
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
        public ActionResult checkout()
        {

            return View();
        }
        [HttpPost]
        public ActionResult AddOrder(FormCollection fc)
        {
            int orderid = 0;
            cart = Session["cart"] as List<ChiTietDH>;
            DonHang dh = new DonHang()
            {
                MaKH = Convert.ToInt32(fc["MaKH"]),
                Nguoitao = fc["TenKH"],
                Ngaylap = DateTime.Now,
                Tinhtrang = "Ổn",
                ThanhTien = Convert.ToDouble(fc["thanhtien"])
            };
            db.DonHangs.Add(dh);
            db.SaveChanges();
            orderid = dh.MaDH;
            foreach (var item in cart)
            {
                ChiTietDH chitiet = new ChiTietDH()
                {
                    MaDH = orderid,                 
                    Gia = item.SanPham.Gia * item.SL,
                    MaSP = item.SanPham.MaSP,
                    SL = item.SL,
                    TenSP = item.SanPham.TenSP,
                    LoaiSP = null,
                };
            
                db.ChiTietDHs.Add(chitiet);
                db.SaveChanges();
            }
            Session["cart"] = null;
            return RedirectToAction("OrderSuccess");
        }
        public ActionResult OrderSuccess()
        {

            return View();
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
