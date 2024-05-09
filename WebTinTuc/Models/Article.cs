using System;
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
    }
}
