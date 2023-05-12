using PJ_.Net_Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PJ_.Net_Restaurant.Controllers
{
    public class EventController : Controller
    {
        RestaurantEntities db = new RestaurantEntities();
        // GET: Event
        public ActionResult Index(string meta)
        {
            var v = from t in db.UpcomingEvents
                    where t.meta == meta
                    select t;
            return View(v.FirstOrDefault());
        }
    }
}