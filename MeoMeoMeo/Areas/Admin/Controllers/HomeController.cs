using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MeoMeoMeo.Models;

namespace MeoMeoMeo.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        CT25Team28Entities db = new CT25Team28Entities();
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(LoginModel login)
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
                    Session["Admin"] = user.TenKH;
                    Session["UserRoles"] = user.MaUR;
                    if (Convert.ToInt32(Session["UserRoles"]) == 1)
                    {
                        return RedirectToAction("Index", "SanPhams");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Tài khoản không có quyền truy cập");
                    }
                }
            }
            return View(login);
        }
        public ActionResult Logout()
        {
            Session["Admin"] = null;
            return RedirectToAction("/");
        }
    }
}