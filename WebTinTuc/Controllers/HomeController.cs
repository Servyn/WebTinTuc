
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebTinTuc.Models;

namespace WebTinTuc.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _dbContext;

        public HomeController()
        {
            _dbContext = new DataContext();
        }


        public ActionResult Index()
        {
            List<Article> articles;

            using (var context = new DataContext()) // Thay thế YourDataContext bằng tên của DataContext của bạn
            {
                articles = context.Articles.ToList();
            }

            return View(articles);
        }




        public ActionResult Logout()
        {
            // Xử lý việc đăng xuất ở đây, ví dụ: xóa session, cookie, ...

            // Chuyển hướng về trang đăng nhập
            return RedirectToAction("Login", "Account");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

       
        public ActionResult Contact()
        {
            
            return View();
        }
        
        [HttpPost]
        public ActionResult Contact(Contacts vm)
        {
            if (ModelState.IsValid)
            {
                
                //using (var db = new DataContext())
                //{
                //    db.Contacts.Add(vm);
                //    db.SaveChanges();
                //}
                if (!IsValidEmail(vm.Email))
                {
                    // Xử lý khi địa chỉ email không hợp lệ, ví dụ: hiển thị thông báo lỗi
                    ModelState.AddModelError("Email", "Địa chỉ email không hợp lệ");
                    return View();
                }

                //Gửi email
                var recipientEmail = vm.Email;  // Lấy địa chỉ email từ đối tượng Contacts
                
                var message = new MailMessage();
                message.From = new MailAddress("testwebtintuc@gmail.com");

                message.To.Add(recipientEmail);
                message.Subject = "Subject of the email";
                message.Body = "Content of the email";

                using (var smtpClient = new SmtpClient("smtp.example.com", 587))
                {
                    smtpClient.Credentials = new NetworkCredential("testwebtintuc@gmail.com", "@abc123@");
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(message);
                }

                return RedirectToAction("ThankYou");
            }

            return View();
        }
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Kiểm tra định dạng email bằng biểu thức chính quy
                var regex = new Regex(@"^[\w\.-]+@([\w-]+\.)+[\w-]{2,4}$");
                return regex.IsMatch(email);
            }
            catch
            {
                return false;
            }
        }
        public ActionResult ThankYou()
        {
            return View();
        }

        //public ActionResult Details()
        //{

        //    return View();
        //}
        private List<Comment> GetCommentsByArticleId(int articleId)
        {
            using (var dbContext = new DataContext())
            {
                // Lấy danh sách các bình luận của bài viết có articleId tương ứng từ cơ sở dữ liệu
                return dbContext.Comments.Where(c => c.ArticleId == articleId).ToList();
            }
        }

        public ActionResult Details(int id)
        {
            var article = _dbContext.Articles.FirstOrDefault(a => a.Id == id);
            var comments = _dbContext.Comments.Where(c => c.ArticleId == id).ToList();
            var viewModel = new ArticleWithCommentsViewModel
            {
                Article = article,
                Comments = comments
            };
            return View(viewModel);
        }



        private (string Title, string Content, DateTime PublishDate) GetArticleById(int id)
        {
            using (var db = new DataContext())
            {
                // Lấy bài viết có id tương ứng từ cơ sở dữ liệu
                var article = db.Articles.FirstOrDefault(u => u.Id == id);

                if (article != null)
                {
                    // Trả về tiêu đề và nội dung của bài viết
                    return (article.Title, article.Content, article.PublishDate);
                }
            }

            // Trường hợp không tìm thấy bài viết, trả về giá trị mặc định hoặc xử lý theo ý muốn của bạn
            return (string.Empty, string.Empty, DateTime.Now);
        }

    }
}