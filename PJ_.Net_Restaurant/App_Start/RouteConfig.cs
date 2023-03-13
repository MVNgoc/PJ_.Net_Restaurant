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
                new[] { "PJ_.Net_Restaurant.Controllers" }
            );

            routes.MapRoute("Home", "{type}",
                new { controller = "Default", action = "Index", id = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    {"type", "Home"}
                },
                new[] { "PJ_.Net_Restaurant.Controllers" }
            );

            routes.MapRoute("Register", "{type}",
                new { controller = "Public", action = "Register", id = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    {"type", "Register"}
                },
                new[] { "PJ_.Net_Restaurant.Controllers" }
            );

            routes.MapRoute(
                name: "Login",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Public", action = "Index", id = UrlParameter.Optional }
            );

            
        }
    }
}
