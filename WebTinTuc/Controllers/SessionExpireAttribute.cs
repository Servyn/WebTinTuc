
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace WebTinTuc.Controllers
{
    public class SessionExpireAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Kiểm tra xem người dùng đã xác thực và có vai trò cần thiết hay không
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated ||
                !IsInRole(filterContext.HttpContext, "admin")) // Đổi "Admin" thành vai trò mà bạn muốn kiểm tra
            {
                // Thiết lập thông báo lỗi trong TempData
                filterContext.Controller.TempData["error"] = "Bạn không có quyền";

                // Chuyển hướng đến trang Home/Index
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary
                    {
                    { "controller", "Home" },
                    { "action", "Index" }
                    });

                return;
            }

            base.OnActionExecuting(filterContext);
        }
        // Phương thức kiểm tra xem người dùng có vai trò cần thiết hay không
        private bool IsInRole(HttpContextBase httpContext, string role)
        {
            // Lấy vai trò của người dùng từ cookie
            var authCookie = httpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                var authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                if (authTicket != null)
                {
                    var userData = authTicket.UserData;
                    // Ở đây, userData có thể chứa các thông tin liên quan đến người dùng, bao gồm vai trò
                    // Trong trường hợp này, mình giả định userData chứa vai trò của người dùng
                    var roles = userData.Split(',');
                    return Array.Exists(roles, element => element.Trim() == role);
                }
            }
            return false;
        }
    }
}
