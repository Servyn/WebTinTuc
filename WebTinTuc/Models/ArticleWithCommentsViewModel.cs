using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebTinTuc.Models
{
    public class ArticleWithCommentsViewModel
    {
        public int Id { get; set; } // Thêm trường Id của bài viết
        public Article Article { get; set; }
        public List<Comment> Comments { get; set; }
    }
}