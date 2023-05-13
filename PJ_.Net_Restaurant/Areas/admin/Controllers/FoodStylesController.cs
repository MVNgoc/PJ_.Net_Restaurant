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
    public class FoodStylesController : Controller
    {
        private RestaurantEntities db = new RestaurantEntities();

        // GET: admin/FoodStyles
        public ActionResult Index()
        {
            return View(db.FoodStyles.OrderBy(banner => banner.order).ToList());
        }

        // GET: admin/FoodStyles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodStyle foodStyle = db.FoodStyles.Find(id);
            if (foodStyle == null)
            {
                return HttpNotFound();
            }
            return View(foodStyle);
        }

        // GET: admin/FoodStyles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/FoodStyles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,img,link,meta,hide,order,datebegin")] FoodStyle foodStyle, HttpPostedFileBase img)
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
                        path = Path.Combine(Server.MapPath("~/Uploads/images/dishtype"), filename);
                        img.SaveAs(path);
                        foodStyle.img = filename; //Lưu ý
                    }
                    else
                    {
                        foodStyle.img = "default.png";
                    }
                    foodStyle.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    foodStyle.meta = Functions.ConvertToUnSign(foodStyle.meta); //convert Tiếng Việt không dấu
                    db.FoodStyles.Add(foodStyle);
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

            return View(foodStyle);
        }

        // GET: admin/FoodStyles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodStyle foodStyle = db.FoodStyles.Find(id);
            if (foodStyle == null)
            {
                return HttpNotFound();
            }
            return View(foodStyle);
        }

        // POST: admin/FoodStyles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,img,link,meta,hide,order,datebegin")] FoodStyle foodStyle, HttpPostedFileBase img)
        {
            try
            {
                var path = "";
                var filename = "";
                FoodStyle temp = getById(foodStyle.id);
                if (ModelState.IsValid)
                {
                    if (img != null)
                    {
                        //filename = Guid.NewGuid().ToString() + img.FileName;
                        filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + img.FileName;
                        path = Path.Combine(Server.MapPath("~/Uploads/images/dishtype"), filename);
                        img.SaveAs(path);
                        temp.img = filename; //Lưu ý
                    }
                    // temp.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());                   
                    temp.name = foodStyle.name;
                    temp.meta = Functions.ConvertToUnSign(foodStyle.meta); //convert Tiếng Việt không dấu
                    temp.order = foodStyle.order;
                    temp.hide = foodStyle.hide;
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
            return View(foodStyle);
        }

        public FoodStyle getById(long id)
        {
            return db.FoodStyles.Where(x => x.id == id).FirstOrDefault();

        }

        // GET: admin/FoodStyles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodStyle foodStyle = db.FoodStyles.Find(id);
            if (foodStyle == null)
            {
                return HttpNotFound();
            }
            return View(foodStyle);
        }

        // POST: admin/FoodStyles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FoodStyle foodStyle = db.FoodStyles.Find(id);

            // Lấy đường dẫn tới folder chứa file hình ảnh
            string imagePath = Server.MapPath("~/Uploads/images/dishtype");

            // Xóa file hình ảnh có tên tương ứng với id của banner
            string imageName = foodStyle.img;
            // Console.Error.WriteLine(imageName);
            string fullPath = Path.Combine(imagePath, imageName);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            db.FoodStyles.Remove(foodStyle);
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
