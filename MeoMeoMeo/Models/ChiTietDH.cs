//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MeoMeoMeo.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ChiTietDH
    {
        public int MaDH { get; set; }
        public int MaSP { get; set; }
        public string LoaiSP { get; set; }
        public string TenSP { get; set; }
        public Nullable<int> SL { get; set; }
        public Nullable<decimal> Gia { get; set; }
    
        public virtual DonHang DonHang { get; set; }
        public virtual SanPham SanPham { get; set; }
    }
}
