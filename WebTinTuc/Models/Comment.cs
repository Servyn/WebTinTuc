using System;
using System.ComponentModel.DataAnnotations;

namespace WebTinTuc.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "N?i dung comment là b?t bu?c")]
        public string Content { get; set; }

        [Display(Name = "Ng??i g?i")]
        public int UserId { get; set; }

        [Display(Name = "Th?i gian g?i")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Bài vi?t")]
        public int ArticleId { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        public virtual Article Article { get; set; }
    }
}
