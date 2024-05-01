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
                        CreatedById = currentUserId
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
            var article = _dbContext.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }

            if (article.CreatedById != GetCurrentUserId())
            {
                return RedirectToAction("Index", "Home");
            }

            _dbContext.Articles.Remove(article);
            _dbContext.SaveChanges();

            // Trả về một phản hồi JSON để thông báo xóa thành công
            return Json(new { success = true });
        }

    }
}
