using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using WebTinTuc.Models;

public class AccountController : Controller
{
    private readonly RegisterDbContext _dbContext;

    public AccountController()
    {
    }
    // Phương thức để hiển thị form đăng ký
    public ActionResult Register()
    {
        var user = new User();
        return View(user);
    }

    // Phương thức POST để xử lý yêu cầu đăng ký
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Register(User user)
    {
        if (ModelState.IsValid)
        {
            using (var db = new RegisterDbContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
            return RedirectToAction("Login", "Account");
        }
        // Nếu dữ liệu không hợp lệ, hiển thị lại trang đăng ký với các thông báo lỗi
        return View(user);
    }


    // Phương thức để hiển thị form đăng nhập
    public ActionResult Login()
    {
        return View();
    }

    // Phương thức POST để xử lý yêu cầu đăng nhập
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            using (var db = new RegisterDbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);
                if (user != null)
                {
                    System.Web.Security.FormsAuthentication.SetAuthCookie(model.Username, false);
                    // Xác nhận đăng nhập thành công, thực hiện các hành động tiếp theo, ví dụ: thiết lập session, cookie, vv.
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }
        }
        // Nếu dữ liệu đăng nhập không hợp lệ, hiển thị lại trang đăng nhập với các thông báo lỗi
        return View(model);
    }

    // Phương thức POST để xử lý yêu cầu đăng xuất
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Logout()
    {
        System.Diagnostics.Debug.WriteLine("Logout method is called.");
        // Xóa cookie xác thực và đăng xuất người dùng
        FormsAuthentication.SignOut();
        // Chuyển hướng người dùng về trang chủ
        return RedirectToAction("Index", "Home");
    }
    public ActionResult UserProfile()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login");
        }

        return View();
    }

}
