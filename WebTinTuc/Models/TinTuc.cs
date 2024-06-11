using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebTinTuc.Models
{
    public class TinTuc
    {
        public int Id { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public DateTime NgayDang { get; set; }
        public int SoLanXemTT { get; set; }
        // Thêm các thuộc tính khác của tin tức tại đây
    }
}