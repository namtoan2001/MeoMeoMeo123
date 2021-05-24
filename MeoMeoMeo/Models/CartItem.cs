    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeoMeoMeo.Models
{
    [Serializable]
    public class CartItem
    { 
        public SanPham id { get; set; }
        public int SoLuong { get; set; }
    }
}