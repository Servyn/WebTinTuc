using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebTinTuc.Models
{
    public class EditArticleViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter content")]
        [AllowHtml]
        public string Content { get; set; }

        [Required(ErrorMessage = "Please enter a publish date")]
        public DateTime PublishDate { get; set; }
    }
}
