using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PJ_.Net_Restaurant.Models;

namespace PJ_.Net_Restaurant.Controllers
{
    public class PartialController : Controller
    {
        RestaurantEntities db = new RestaurantEntities();
        // GET: Partial
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult getNavBar()
        {
            var v = from t in db.Navbars
                    where t.hide == true
                    orderby t.order ascending
                    select t;

            return PartialView(v.ToList());
        }

        public ActionResult getHeader()
        {
            var v = from t in db.Headers
                    where t.hide == true
                    select t;

            return PartialView(v.ToList());
        }
    }
}