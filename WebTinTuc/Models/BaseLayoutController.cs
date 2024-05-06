using System.Linq;
using System.Web.Mvc;
using WebTinTuc.Models;

namespace WebTinTuc.Controllers
{
    public class BaseLayoutController : Controller
    {
        protected int GetCurrentUserId()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return -1; // Hoặc một giá trị khác để chỉ ra rằng không có người dùng được xác thực
            }

            // Lấy tên người dùng từ cookie xác thực
            var username = User.Identity.Name;

            // Tìm người dùng có tên người dùng tương ứng trong cơ sở dữ liệu
            using (var db = new RegisterDbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Username == username);
                if (user != null)
                {
                    return user.Id; // Trả về Id của người dùng tìm thấy
                }
            }

            return -1; // Trả về -1 nếu không tìm thấy người dùng
        }
    }
}
