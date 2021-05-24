using MeoMeoMeo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MeoMeoMeo.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        CT25Team28Entities db = new CT25Team28Entities();
        private const string CartSession = "cart";
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }
        public ActionResult AddItem(int Masp, int Soluong)
        {
            var cart = Session[CartSession];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.id.MaSP == Masp))
                {
                    foreach (var item in list)
                    {
                        if (item.id.MaSP == Masp)
                        {
                            item.SoLuong += Soluong;
                        }
                    }
                }
                else
                {
                    var item = new CartItem();
                    item.id.MaSP = Masp;
                    item.SoLuong = Soluong;
                    list.Add(item);
                }
                Session[CartSession] = list;
            }
            else
            {
                var item = new CartItem();
                item.id.MaSP = Masp;
                item.SoLuong = Soluong;
                var list = new List<CartItem>();
                list.Add(item);
                Session[CartSession] = list;
            }
            return RedirectToAction("Index");
        }
    }
}