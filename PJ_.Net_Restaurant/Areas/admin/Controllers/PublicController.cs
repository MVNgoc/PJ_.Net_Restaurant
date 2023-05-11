using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PJ_.Net_Restaurant.Areas.admin.Controllers
{
    public class PublicController : Controller
    {
        // GET: admin/Pubblic
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ForgotPass()
        {
            return View();
        }
        public ActionResult ChangePass()
        {
            return View();
        }
    }
}