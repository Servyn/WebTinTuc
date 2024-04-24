using System.Web.Mvc;
using System.Web.Routing;

public class RouteConfig
{
    public static void RegisterRoutes(RouteCollection routes)
    {
        routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

        routes.MapRoute(
            name: "Login",
            url: "Account/Login",
            defaults: new { controller = "Account", action = "Login" }
        );

        routes.MapRoute(
            name: "Register",
            url: "Account/Register",
            defaults: new { controller = "Account", action = "Register" }
        );

        routes.MapRoute(
            name: "Logout",
            url: "Account/Logout",
            defaults: new { controller = "Account", action = "Logout" }
        );

        // Thêm các route khác nếu cần
        routes.MapRoute(
            name: "Details",
            url: "News/Details",
            defaults: new { controller = "News", action = "Details" }
        );

        routes.MapRoute(
            name: "Default",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
        );
    }
}
