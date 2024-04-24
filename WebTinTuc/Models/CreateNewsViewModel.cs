using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebTinTuc.Models
{
    public class CreateNewsViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        public string Content { get; set; }

        // Các thuộc tính khác nếu cần thiết
    }
}
