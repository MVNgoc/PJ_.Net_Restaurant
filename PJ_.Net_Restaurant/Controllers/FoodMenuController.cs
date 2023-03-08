using PJ_.Net_Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PJ_.Net_Restaurant.Controllers
{
    public class FoodMenuController : Controller
    {
        RestaurantEntities db = new RestaurantEntities();
        // GET: FoodMenu
        public ActionResult Index(string meta)
        {
            var v = from t in db.FoodStyles
                    where t.meta == meta
                    select t;
            return View(v.FirstOrDefault());
        }

        public ActionResult getFoods(string name)
        {
            ViewBag.meta = "Food-Menu";
            var v = from t in db.Foods
                    where t.footstyle == name && t.hide == true
                    orderby t.order ascending
                    select t;

            return PartialView(v.ToList());
        }

        public ActionResult getFoodsStyleBar(string meta)
        {
            ViewBag.meta = meta;
            var v = from t in db.FoodStyles
                    where t.hide == true
                    orderby t.order ascending
                    select t;

            return PartialView(v.ToList());
        }
    }
}