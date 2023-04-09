using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using PJ_.Net_Restaurant.Help;
using PJ_.Net_Restaurant.Models;

namespace PJ_.Net_Restaurant.Areas.admin.Controllers
{
    public class menusController : Controller
    {
        private RestaurantEntities db = new RestaurantEntities();

        // GET: admin/menus
        public ActionResult Index()
        {
            return View(db.Navbars.OrderBy(navbar => navbar.order).ToList());
        }

        // GET: admin/menus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Navbar navbar = db.Navbars.Find(id);
            if (navbar == null)
            {
                return HttpNotFound();
            }
            return View(navbar);
        }

        // GET: admin/menus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/menus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,link,meta,hide,order,datebegin")] Navbar navbar)
        {
            if (ModelState.IsValid)
            {
                navbar.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                navbar.meta = Functions.ConvertToUnSign(navbar.meta); //convert Tiếng Việt không dấu
                db.Navbars.Add(navbar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(navbar);
        }

        // GET: admin/menus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Navbar navbar = db.Navbars.Find(id);
            if (navbar == null)
            {
                return HttpNotFound();
            }
            return View(navbar);
        }

        // POST: admin/menus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,link,meta,hide,order,datebegin")] Navbar navbar)
        {
            try
            {
                Navbar temp = getById(navbar.id);
                if (ModelState.IsValid)
                {
                    // temp.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());                   
                    temp.name = navbar.name;
                    temp.meta = Functions.ConvertToUnSign(navbar.meta); //convert Tiếng Việt không dấu
                    temp.hide = navbar.hide;
                    temp.order = navbar.order;
                    db.Entry(temp).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(navbar);
        }

        public Navbar getById(long id)
        {
            return db.Navbars.Where(x => x.id == id).FirstOrDefault();

        }

        // GET: admin/menus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Navbar navbar = db.Navbars.Find(id);
            if (navbar == null)
            {
                return HttpNotFound();
            }
            return View(navbar);
        }

        // POST: admin/menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Navbar navbar = db.Navbars.Find(id);
            db.Navbars.Remove(navbar);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
