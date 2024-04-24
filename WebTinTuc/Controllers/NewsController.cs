using System;
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
        public ActionResult Create()
        {
            var model = new CreateNewsViewModel(); // Tạo một thể hiện của CreateNewsViewModel
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
                        CreatedById = currentUserId // Sét CreatedById là Id của người dùng hiện tại
                    };

                    _dbContext.Articles.Add(article);
                    _dbContext.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Xử lý trường hợp không tìm thấy người dùng
                    ModelState.AddModelError("", "Current user not found.");
                    return View(model);
                }
            }

            return View(model);
        }
    }
}
