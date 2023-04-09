using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlTypes;
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
    public class BannersController : Controller
    {
        private RestaurantEntities db = new RestaurantEntities();

        // GET: admin/Banners
        public ActionResult Index()
        {
            return View(db.Banners.OrderBy(banner => banner.order).ToList());
        }

        // GET: admin/Banners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner banner = db.Banners.Find(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        // GET: admin/Banners/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/Banners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,img,headtitle,title,content,link,meta,hide,order,datebegin")] Banner banner, HttpPostedFileBase img)
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
                        path = Path.Combine(Server.MapPath("~/Uploads/images/slide"), filename);
                        img.SaveAs(path);
                        banner.img = filename; //Lưu ý
                    }
                    else
                    {
                        banner.img = "slide.jpg";
                    }
                    banner.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    banner.meta = Functions.ConvertToUnSign(banner.meta); //convert Tiếng Việt không dấu
                    db.Banners.Add(banner);
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

            return View(banner);
        }

        // GET: admin/Banners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner banner = db.Banners.Find(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        // POST: admin/Banners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,img,headtitle,title,content,link,meta,hide,order,datebegin")] Banner banner, HttpPostedFileBase img)
        {
            try
            {
                var path = "";
                var filename = "";
                Banner temp = getById(banner.id);
                if (ModelState.IsValid)
                {
                    if (img != null)
                    {
                        //filename = Guid.NewGuid().ToString() + img.FileName;
                        filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + img.FileName;
                        path = Path.Combine(Server.MapPath("~/Uploads/images/slide"), filename);
                        img.SaveAs(path);
                        temp.img = filename; //Lưu ý
                    }
                    // temp.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());                   
                    temp.headtitle = banner.headtitle;
                    temp.title = banner.title;
                    temp.content = banner.content;
                    temp.meta = Functions.ConvertToUnSign(banner.meta); //convert Tiếng Việt không dấu
                    temp.order = banner.order;
                    temp.hide = banner.hide;
                    db.Entry(temp).State = EntityState.Modified;
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
            return View(banner);
        }

        public Banner getById(long id)
        {
            return db.Banners.Where(x => x.id == id).FirstOrDefault();

        }

        // GET: admin/Banners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner banner = db.Banners.Find(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        // POST: admin/Banners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Banner banner = db.Banners.Find(id);

            // Lấy đường dẫn tới folder chứa file hình ảnh
            string imagePath = Server.MapPath("~/Uploads/images/slide");

            // Xóa file hình ảnh có tên tương ứng với id của banner
            string imageName = banner.img;
            Console.Error.WriteLine(imageName);
            string fullPath = Path.Combine(imagePath, imageName);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            db.Banners.Remove(banner);
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
