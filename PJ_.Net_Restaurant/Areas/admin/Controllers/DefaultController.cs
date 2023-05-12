using PJ_.Net_Restaurant.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PJ_.Net_Restaurant.Areas.admin.Controllers
{
    public class DefaultController : BaseController
    {
        // GET: admin/Default
        public ActionResult Index()
        {
            return View();
        }

    }
}