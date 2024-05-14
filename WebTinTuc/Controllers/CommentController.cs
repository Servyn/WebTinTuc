using System;
using System.Linq;
using System.Web.Mvc;
using WebTinTuc.Models;

namespace WebTinTuc.Controllers
{
    public class CommentController : BaseController
    {
        private readonly DataContext _dbContext;

        public CommentController()
        {
            _dbContext = new DataContext(); // Khởi tạo DataContext của bạn ở đây
        }

        // Các phương thức và thuộc tính từ BaseController có thể được sử dụng ở đây

        // Action để hiển thị danh sách các comment của một bài viết
        public ActionResult Index(int articleId)
        {
            var comments = _dbContext.Comments.Where(c => c.ArticleId == articleId).ToList();
            return View(comments);
        }

        // Action để thêm một comment mới
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(CommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var articleExists = _dbContext.Articles.Any(a => a.Id == model.ArticleId);
                if (!articleExists)
                {
                    ModelState.AddModelError("", "Article does not exist.");
                    return RedirectToAction("Index", "Home");
                }

                try
                {
                    var comment = new Comment
                    {
                        Content = model.Content,
                        UserId = GetCurrentUserId(),
                        CreatedDate = DateTime.Now,
                        ArticleId = model.ArticleId
                    };

                    _dbContext.Comments.Add(comment);
                    _dbContext.SaveChanges();

                    return RedirectToAction("Details", "Home", new { id = model.ArticleId });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Unable to add comment. Please try again.");
                }
            }

            return RedirectToAction("Details", "Home", new { id = model.ArticleId });
        }


        private void SaveCommentToDatabase(Comment comment)
        {
            // Thêm comment mới vào DbSet<Comment>
            _dbContext.Comments.Add(comment);

            // Lưu thay đổi vào cơ sở dữ liệu
            _dbContext.SaveChanges();
        }

    }
}
