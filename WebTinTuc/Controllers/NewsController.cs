using System;
using System.Linq;
using System.Web.Mvc;
using WebTinTuc.Models;

namespace WebTinTuc.Controllers
{
    public class NewsController : BaseController
    {
        private readonly ApplicationDbContext _dbContext;

        public NewsController()
        {
            _dbContext = new ApplicationDbContext();
        }

        // GET: News/Create
        // GET: News/Create
        public ActionResult Create()
        {
            var model = new CreateNewsViewModel();
            return View(model);
        }

        // POST: News/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateNewsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currentUserId = GetCurrentUserId();
                if (currentUserId != -1)
                {
                    var article = new Article
                    {
                        Title = model.Title,
                        Content = model.Content,
                        PublishDate = DateTime.Now,
                        CreatedById = currentUserId,
                        Status = false
                    };

                    _dbContext.Articles.Add(article);
                    _dbContext.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Current user not found.");
                    return View(model);
                }
            }

            return View(model);
        }


        public ActionResult Edit(int id)
        {
            var article = _dbContext.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }

            if (article.CreatedById != GetCurrentUserId())
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new EditArticleViewModel
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var article = _dbContext.Articles.Find(model.Id);
                if (article == null)
                {
                    return HttpNotFound();
                }

                if (article.CreatedById != GetCurrentUserId())
                {
                    return RedirectToAction("Index", "Home");
                }

                article.Title = model.Title;
                article.Content = model.Content;

                _dbContext.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            // Lấy bài viết từ cơ sở dữ liệu
            var article = _dbContext.Articles.Find(id);

            // Kiểm tra xem bài viết có tồn tại không
            if (article == null)
            {
                return HttpNotFound();
            }

            // Kiểm tra xem người dùng hiện tại có vai trò là "admin" hay không
            var currentUser = _dbContext.Users.FirstOrDefault(u => u.Username == User.Identity.Name);
            if (currentUser == null)
            {
                // Xử lý nếu không tìm thấy thông tin người dùng
                return HttpNotFound(); // Hoặc trả về một trang lỗi
            }

            // Kiểm tra xem người dùng hiện tại có vai trò là "admin" hay không
            if (currentUser.Role == "admin")
            {
                // Nếu là admin, cho phép xóa bài viết của tất cả người dùng
                try
                {
                    _dbContext.Articles.Remove(article);
                    _dbContext.SaveChanges();
                    return RedirectToAction("AdminProfile", "Account");
                }
                catch (Exception ex)
                {
                    // Xử lý ngoại lệ nếu có
                    // Ở đây bạn có thể ghi log lỗi hoặc trả về một trang lỗi
                    return View("Error");
                }
            }
            else
            {
                // Nếu không phải là admin, kiểm tra xem người dùng có phải là tác giả của bài viết không
                if (article.CreatedById == currentUser.Id)
                {
                    // Nếu là tác giả của bài viết, cho phép xóa bài viết
                    try
                    {
                        _dbContext.Articles.Remove(article);
                        _dbContext.SaveChanges();
                        return RedirectToAction("UserProfile", "Account");
                    }
                    catch (Exception ex)
                    {
                        // Xử lý ngoại lệ nếu có
                        // Ở đây bạn có thể ghi log lỗi hoặc trả về một trang lỗi
                        return View("Error");
                    }
                }
                else
                {
                    // Người dùng không có quyền xóa bài viết
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Approve(int id)
        {
            var article = _dbContext.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }

            // Đặt trạng thái của bài viết thành "Approved"
            article.Status = true;

            _dbContext.SaveChanges();

            return RedirectToAction("ApproveArticle");
        }
        public ActionResult ApproveArticle()
        {
            // Lấy danh sách các bài viết chưa được xác nhận
            var pendingArticles = _dbContext.Articles.Where(a => !a.Status).ToList();
            return View(pendingArticles);
        }

    }
}