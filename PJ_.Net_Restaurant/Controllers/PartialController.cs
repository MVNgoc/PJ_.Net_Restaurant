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
        
        /*----------------------- HEADER ---------------------------*/
        public ActionResult getNavBar()
        {
            var v = from t in db.Navbars
                    where t.hide == true
                    orderby t.order ascending
                    select t;

            return PartialView(v.ToList());
        }

        public ActionResult getLogo()
        {
            var v = from t in db.Logoes
                    where t.hide == true
                    select t;

            return PartialView(v.FirstOrDefault());
        }

        public ActionResult getHeader()
        {
            var v = from t in db.Contacts
                    where t.hide == true
                    select t;

            return PartialView(v.FirstOrDefault());
        }

        public ActionResult getVisitUs()
        {
            var v = from t in db.Contacts
                    where t.hide == true
                    select t;

            return PartialView(v.FirstOrDefault());
        }
        /*-----------------------------------------------------------*/

        /*----------------------- FOOTER ---------------------------*/
        public ActionResult getSocial()
        {
            var v = from t in db.Socials
                    where t.hide == true
                    orderby t.order ascending
                    select t;

            return PartialView(v.ToList());
        }

        public ActionResult getOffer()
        {
            var v = from t in db.Offers
                    where t.hide == true
                    orderby t.order ascending
                    select t;

            return PartialView(v.ToList());
        }

        public ActionResult getFooterMenu()
        {
            var v = from t in db.Navbars
                    where t.hide == true
                    orderby t.order ascending
                    select t;

            return PartialView(v.ToList());
        }

        public ActionResult getInfoRestaurant()
        {
            var v = from t in db.Contacts
                    where t.hide == true
                    select t;

            return PartialView(v.FirstOrDefault());
        }
        /*-----------------------------------------------------------*/
    }
}