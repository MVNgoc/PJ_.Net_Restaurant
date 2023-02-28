using PJ_.Net_Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PJ_.Net_Restaurant.Controllers
{
    public class DefaultController : Controller
    {
        RestaurantEntities db = new RestaurantEntities();

        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult getBanner()
        {
            var v = from t in db.Banners
                    where t.hide == true
                    orderby t.order ascending
                    select t;

            return PartialView(v.ToList());
        }

        public ActionResult getFoodStyle()
        {
            var v = from t in db.FoodStyles
                    where t.hide == true
                    orderby t.order ascending
                    select t;

            return PartialView(v.ToList());
        }

        public ActionResult getFoodStory()
        {
            var v = from t in db.Stories
                    where t.hide == true
                    orderby t.order ascending
                    select t;

            return PartialView(v.FirstOrDefault());
        }

        public ActionResult getSpecialDish()
        {
            var v = from t in db.Foods
                    where t.hide == true
                    where t.type == "SPECIAL"
                    select t;

            return PartialView(v.FirstOrDefault());
        }

        public ActionResult getFoodMenu()
        {
            var v = (from t in db.Foods
                    where t.hide == true
                    orderby t.sales descending
                    select t).Take(6);

            return PartialView(v.ToList());
        }

        public ActionResult getContact()
        {
            var v = from t in db.Contacts
                    where t.hide == true
                    select t;

            return PartialView(v.FirstOrDefault());
        }

        public ActionResult getOurStrength()
        {
            var v = from t in db.OurStrengths
                    where t.hide == true
                    orderby t.order ascending
                    select t;

            return PartialView(v.ToList());
        }

        public ActionResult getUpcomingEvent()
        {
            var v = from t in db.UpcomingEvents
                    where t.hide == true
                    orderby t.order ascending
                    select t;

            return PartialView(v.ToList());
        }
    }
}