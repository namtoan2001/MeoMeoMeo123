using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MeoMeoMeo
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "DangNhap",
               url: "Dang-Nhap",
               defaults: new { controller = "DangNhap", action = "Login", id = UrlParameter.Optional },
               new[] { "MeoMeoMeo.Controllers" }
           );

            routes.MapRoute(
               name: "DangKi",
               url: "Dang-Ki",
               defaults: new { controller = "DangNhap", action = "Dangki", id = UrlParameter.Optional },
               new[] { "MeoMeoMeo.Controllers" }
           );

            routes.MapRoute(
              name: "SanPham",
              url: "San-Pham",
              defaults: new { controller = "Home", action = "SanPham", id = UrlParameter.Optional },
              new[] { "MeoMeoMeo.Controllers" }
          );

            routes.MapRoute(
               name: "ChitietSP",
               url: "San-Pham/{id}",
               defaults: new { controller = "Home", action = "chitietSP", id = UrlParameter.Optional },
               new[] { "MeoMeoMeo.Controllers" }
           );

            routes.MapRoute(
              name: "GioHang",
              url: "Gio-Hang",
              defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
              new[] { "MeoMeoMeo.Controllers" }
          );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{Action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new [] {"MeoMeoMeo.Controllers"}
            );
            
        }
    }
}
