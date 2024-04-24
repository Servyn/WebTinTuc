using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebTinTuc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            // Kiểm tra xem người dùng đã xác thực hay chưa
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    // Người dùng đã đăng nhập, bạn có thể thực hiện các hành động cần thiết ở đây
                }
                else
                {
                    // Người dùng chưa đăng nhập, bạn có thể thực hiện các hành động cần thiết ở đây
                }
            }
        }
    }
}
