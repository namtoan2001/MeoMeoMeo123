using MeoMeoMeo.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MeoMeoMeo.Controllers
{
    public class DangNhapController : Controller
    {

        CT25Team28Entities db = new CT25Team28Entities();
        // GET: DangNhap
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                var user = db.KhachHangs.Where(x => x.TenDN == login.TenDangNhap && x.MK == login.MatKhau).FirstOrDefault();
                if (user == null)
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không hợp lệ");
                }
                else
                {
                    Session["TenDangNhap"] = user.TenDN.ToString();
                    return RedirectToAction("Index","Home");
                    
                }
            }
            return View(login);
        }

        [HttpGet]
        public ActionResult Dangki()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangki(RegisterModel register)
        {
            if (ModelState.IsValid)
            {
                var kh = new KhachHang();
                if (kh.CheckUserName(register.TenDangNhap))
                {
                    ModelState.AddModelError("","Tên Đăng Nhập đã tồn tại");
                }
                else
                {
                    kh.TenDN = register.TenDangNhap;
                    kh.TenKH = register.TenNguoiDung;
                    kh.MK = register.MatKhau;
                    kh.Sdt = register.SoDienThoai;
                    kh.Diachi = register.DiaChi;
                    db.KhachHangs.Add(kh);
                    db.SaveChanges();

                    register = new RegisterModel();

                    ViewBag.Success = "Đăng kí thành công";

                    ModelState.Clear();
                }
            }
            return View(register);
        }
    }
}