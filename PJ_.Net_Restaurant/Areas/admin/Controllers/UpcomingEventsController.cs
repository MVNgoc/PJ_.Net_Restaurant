using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PJ_.Net_Restaurant.Help;
using PJ_.Net_Restaurant.Models;

namespace PJ_.Net_Restaurant.Areas.admin.Controllers
{
    public class UpcomingEventsController : Controller
    {
        private RestaurantEntities db = new RestaurantEntities();

        // GET: admin/UpcomingEvents
        public ActionResult Index()
        {
            return View(db.UpcomingEvents.OrderBy(f => f.order).ToList());
        }

        // GET: admin/UpcomingEvents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UpcomingEvent upcomingEvent = db.UpcomingEvents.Find(id);
            if (upcomingEvent == null)
            {
                return HttpNotFound();
            }
            return View(upcomingEvent);
        }

        // GET: admin/UpcomingEvents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/UpcomingEvents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "id,img,eventdate,content,title,description,meta,hide,order,datebegin")] UpcomingEvent upcomingEvent, HttpPostedFileBase img)
        {
            try
            {
                var path = "";
                var filename = "";
                if (ModelState.IsValid)
                {
                    if (img != null)
                    {
                        //filename = Guid.NewGuid().ToString() + img.FileName;
                        filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + img.FileName;
                        path = Path.Combine(Server.MapPath("~/Uploads/images/events"), filename);
                        img.SaveAs(path);
                        upcomingEvent.img = filename; //Lưu ý
                    }
                    else
                    {
                        upcomingEvent.img = "default-event.jpg";
                    }
                    upcomingEvent.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    upcomingEvent.meta = Functions.ConvertToUnSign(upcomingEvent.meta); //convert Tiếng Việt không dấu
                    db.UpcomingEvents.Add(upcomingEvent);
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

            return View(upcomingEvent);
        }

        // GET: admin/UpcomingEvents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UpcomingEvent upcomingEvent = db.UpcomingEvents.Find(id);
            if (upcomingEvent == null)
            {
                return HttpNotFound();
            }
            return View(upcomingEvent);
        }

        // POST: admin/UpcomingEvents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "id,img,eventdate,content,title,description,meta,hide,order,datebegin")] UpcomingEvent upcomingEvent, HttpPostedFileBase img)
        {
            try
            {
                var path = "";
                var filename = "";
                UpcomingEvent temp = getById(upcomingEvent.id);
                if (ModelState.IsValid)
                {
                    if (img != null)
                    {
                        //filename = Guid.NewGuid().ToString() + img.FileName;
                        filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + img.FileName;
                        path = Path.Combine(Server.MapPath("~/Uploads/images/events"), filename);
                        img.SaveAs(path);
                        temp.img = filename; //Lưu ý
                    }
                    // temp.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());                   
                    temp.content = upcomingEvent.content;
                    temp.eventdate = upcomingEvent.eventdate;
                    temp.title = upcomingEvent.title;
                    temp.description = upcomingEvent.description;
                    temp.meta = Functions.ConvertToUnSign(upcomingEvent.meta); //convert Tiếng Việt không dấu
                    temp.order = upcomingEvent.order;
                    temp.hide = upcomingEvent.hide;
                    db.Entry(upcomingEvent).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Debug.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(upcomingEvent);
        }

        public UpcomingEvent getById(long id)
        {
            return db.UpcomingEvents.Where(x => x.id == id).FirstOrDefault();

        }

        // GET: admin/UpcomingEvents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UpcomingEvent upcomingEvent = db.UpcomingEvents.Find(id);
            if (upcomingEvent == null)
            {
                return HttpNotFound();
            }
            return View(upcomingEvent);
        }

        // POST: admin/UpcomingEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UpcomingEvent upcomingEvent = db.UpcomingEvents.Find(id);
            db.UpcomingEvents.Remove(upcomingEvent);
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
