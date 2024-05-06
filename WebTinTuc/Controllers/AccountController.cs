using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using WebTinTuc.Controllers;
using WebTinTuc.Models;

public class AccountController : BaseController
{
    private readonly RegisterDbContext _dbContext;

    public AccountController()
    {
        _dbContext = new RegisterDbContext();
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
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
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
            var user = _dbContext.Users.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(model.Username, false);
                // Xác nhận đăng nhập thành công, thực hiện các hành động tiếp theo, ví dụ: thiết lập session, cookie, vv.
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password.");
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
        // Xóa cookie xác thực và đăng xuất người dùng
        FormsAuthentication.SignOut();
        // Chuyển hướng người dùng về trang chủ
        return RedirectToAction("Index", "Home");
    }

    // Phương thức để hiển thị thông tin người dùng
    // Phương thức để hiển thị thông tin người dùng và các bài viết của họ
    public ActionResult UserProfile()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login");
        }

        // Lấy danh sách bài viết của người dùng hiện tại
        var currentUserId = GetCurrentUserId();
        var userArticles = _dbContext.Articles.Where(a => a.CreatedById == currentUserId).ToList();

        return View(userArticles);
    }
    public ActionResult AdminProfile()
    {
        // Kiểm tra quyền truy cập của người dùng
        if (!User.Identity.IsAuthenticated)
        {
            // Nếu không được phép truy cập, chuyển hướng về trang đăng nhập
            return RedirectToAction("Login");
        }

        // Lấy thông tin của người dùng hiện tại
        var username = User.Identity.Name;

        // Kiểm tra vai trò của người dùng
        var role = "";
        using (var db = new RegisterDbContext())
        {
            var user = db.Users.FirstOrDefault(u => u.Username == username);
            if (user != null)
            {
                role = user.Role;
            }
        }

        // Nếu vai trò là admin, truyền danh sách bài viết của người dùng admin vào view
        if (role == "admin")
        {
            // Lấy danh sách bài viết của người dùng hiện tại
            var currentUserId = GetCurrentUserId();
            var userArticles = _dbContext.Articles.Where(a => a.CreatedById == currentUserId).ToList();

            // Truyền danh sách bài viết vào view AdminProfile
            return View(userArticles);
        }
        else
        {
            // Nếu không phải admin, chuyển hướng về trang UserProfile
            return RedirectToAction("UserProfile");
        }

    }
    public ActionResult UserList()
    {
        // Lấy danh sách các người dùng từ cơ sở dữ liệu, loại bỏ các người dùng có vai trò là admin
        var userList = _dbContext.Users.Where(u => u.Role != "admin").ToList();
        return View(userList);
    }
    public ActionResult UserArticles(int userId)
    {
        var userArticles = _dbContext.Articles.Where(a => a.CreatedById == userId).ToList();
        return View(userArticles);
    }
}
