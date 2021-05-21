using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MeoMeoMeo.Models
{
    public class RegisterModel
    {
        [Key]
        public int MaKH { get; set; }

        [Display(Name = "Tên Đăng Nhập")]
        [Required(ErrorMessage = "Yêu cầu nhập tên đăng nhập")]
        public string TenDangNhap { get; set; }


        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Yêu cầu nhập mật khẩu")]
        [StringLength(20,MinimumLength = 6,ErrorMessage ="Độ dài mật khẩu ít nhất 6 kí tự")]

        public string MatKhau { get; set; }

        [Display(Name = "Nhập lại Mật khẩu")]
        [Required(ErrorMessage = "Yêu cầu nhập lại mật khẩu")]
        [Compare("MatKhau",ErrorMessage = "Xác nhận mật khẩu không đúng")]
        public string NhapLaiMK { get; set; }

        [Display(Name = "Họ và Tên")]
        [Required(ErrorMessage = "Yêu cầu nhập họ và tên")]
        public string TenNguoiDung { get; set; }

        [Display(Name = "Số Điện Thoại")]
        [Required(ErrorMessage = "Yêu cầu nhập số điện thoại")]
        public string SoDienThoai { get; set; }

        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "Yêu cầu nhập địa chỉ")]
        public string DiaChi { get; set; }
    }
}