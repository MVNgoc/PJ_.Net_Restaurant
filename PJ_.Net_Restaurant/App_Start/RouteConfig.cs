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

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Default", action = "Index", id = UrlParameter.Optional }
            );

            
        }
    }
}
