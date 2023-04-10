using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PJ_.Net_Restaurant
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("FoodMenu", "{type}/{meta}",
                new { controller = "FoodMenu", action = "Index", id = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    {"type", "Food-Menu"}
                },
                namespaces: new[] { "PJ_.Net_Restaurant.Controllers" }
            );

            routes.MapRoute("Login", "{type}",
                new { controller = "Public", action = "Index", id = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    {"type", "Login"}
                },
                namespaces: new[] { "PJ_.Net_Restaurant.Controllers" }
            );

            routes.MapRoute("Register", "{type}",
                new { controller = "Public", action = "Register", id = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    {"type", "Register"}
                },
                namespaces: new[] { "PJ_.Net_Restaurant.Controllers" }
            );

            routes.MapRoute("ForgotPass", "{type}",
                new { controller = "Public", action = "ForgotPass", id = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    {"type", "ForgotPass"}
                },
                namespaces: new[] { "PJ_.Net_Restaurant.Controllers" }
            );

            routes.MapRoute("ResetPass", "{type}",
                new { controller = "Public", action = "ResetPass", id = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    {"type", "ResetPass"}
                },
                namespaces: new[] { "PJ_.Net_Restaurant.Controllers" }
            );

            routes.MapRoute("logout", "{type}",
                new { controller = "Public", action = "logout", id = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    {"type", "logout"}
                },
                namespaces: new[] { "PJ_.Net_Restaurant.Controllers" }
            );

            routes.MapRoute("User", "{type}",
                new { controller = "User", action = "Index", id = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    {"type", "User"}
                },
                namespaces: new[] { "PJ_.Net_Restaurant.Controllers" }
            );

            routes.MapRoute("ShopCart", "{type}",
                new { controller = "ShopCart", action = "Index", id = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    {"type", "ShopCart"}
                },
                namespaces: new[] { "PJ_.Net_Restaurant.Controllers" }
            );

            routes.MapRoute("Bill", "User/BillHistory/{type}",
                new { controller = "Bill", action = "Index", id = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    {"type", "Bill"}
                },
                namespaces: new[] { "PJ_.Net_Restaurant.Controllers" }
            );

            routes.MapRoute("BillHistory", "User/{type}",
                new { controller = "BillHistory", action = "Index", id = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    {"type", "BillHistory"}
                },
                namespaces: new[] { "PJ_.Net_Restaurant.Controllers" }
            );

            routes.MapRoute("ChangePass", "User/{type}",
                new { controller = "User", action = "ChangePass", id = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    {"type", "ChangePass"}
                },
                namespaces: new[] { "PJ_.Net_Restaurant.Controllers" }
            );

            routes.MapRoute(
                name: "Home",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Default", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "PJ_.Net_Restaurant.Controllers" }
            );
        }
    }
}
