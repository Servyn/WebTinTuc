using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using WebTinTuc.Models;

namespace WebTinTuc.Models
{
    public class Article
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        public string Content { get; set; }

        [Required]
        public DateTime PublishDate { get; set; }

        public int CreatedById { get; set; }

        public virtual User CreatedBy { get; set; }

        [Required]
        public bool Status { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public int CategoryId { get; set; } // Thay đổi tên thành CreatedById để phản ánh Id của người tạo

        public virtual Category Category { get; set; } // Thêm thuộc tính để thiết lập quan hệ với User

    }
}
