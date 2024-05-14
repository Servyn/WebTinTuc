using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebTinTuc.Models
{
    public class CommentViewModel
    {
        [Required(ErrorMessage = "Nội dung comment là bắt buộc")]
        public string Content { get; set; }

        [Display(Name = "Người gửi")]
        public int UserId { get; set; }

        [Display(Name = "Bài viết")]
        public int ArticleId { get; set; }
    }
}
