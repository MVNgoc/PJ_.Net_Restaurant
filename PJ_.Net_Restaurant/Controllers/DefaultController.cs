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

        /*public ActionResult getFoodStyle()
        {
            var v = from t in db.Navbars
                    where t.hide == true
                    orderby t.order ascending
                    select t;

            return PartialView(v.ToList());
        }*/
    }
}