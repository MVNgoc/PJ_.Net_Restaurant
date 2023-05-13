using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Drawing;
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
    public class FoodsController : Controller
    {
        private RestaurantEntities db = new RestaurantEntities();

        // GET: admin/Foods
        public ActionResult Index()
        {
            return View(db.Foods.OrderBy(t => t.footstyle).ThenBy(t => t.order).ToList());
        }


        // GET: admin/Foods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // GET: admin/Foods/Create
        public ActionResult Create()
        {
            List<FoodStyle> foodStyles = null;
            foodStyles = db.FoodStyles.ToList();
            List<SelectListItem> selectListItems = foodStyles.Select(fs => new SelectListItem
            {
                Text = fs.name,
                Value = fs.name
            }).ToList();

            ViewBag.FoodStyleItems = selectListItems;
            return View();
        }

        // POST: admin/Foods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,img,price,discountprice,describe,sales,type,footstyle,link,meta,hide,order,datebegin")] Food food, HttpPostedFileBase img)
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
                        path = Path.Combine(Server.MapPath("~/Uploads/images/foods"), filename);
                        img.SaveAs(path);
                        food.img = filename; //Lưu ý
                    }
                    else
                    {
                        food.img = "default.png";
                    }
                    food.sales = 0;
                    food.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());                   
                    food.meta = Functions.ConvertToUnSign(food.meta); //convert Tiếng Việt không dấu
                    db.Foods.Add(food);
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

            return View(food);
        }

        // GET: admin/Foods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }

            List<FoodStyle> foodStyles = null;
            foodStyles = db.FoodStyles.ToList();
            List<SelectListItem> selectListItems = foodStyles.Select(fs => new SelectListItem
            {
                Text = fs.name,
                Value = fs.name
            }).ToList();

            ViewBag.FoodStyleItems = selectListItems;

            return View(food);
        }

        // POST: admin/Foods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,img,price,discountprice,describe,sales,type,footstyle,link,meta,hide,order,datebegin")] Food food, HttpPostedFileBase img)
        {
            try
            {
                var path = "";
                var filename = "";
                Food temp = getById(food.id);
                if (ModelState.IsValid)
                {
                    if (img != null)
                    {
                        //filename = Guid.NewGuid().ToString() + img.FileName;
                        filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + img.FileName;
                        path = Path.Combine(Server.MapPath("~/Uploads/images/foods"), filename);
                        img.SaveAs(path);
                        temp.img = filename; //Lưu ý
                    }
                    // temp.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());                   
                    temp.name = food.name;
                    temp.price = food.price;
                    temp.discountprice = food.discountprice;
                    temp.describe = food.describe;
                    temp.sales = food.sales;
                    temp.type = food.type;
                    temp.footstyle = food.footstyle;
                    temp.meta = Functions.ConvertToUnSign(food.meta); //convert Tiếng Việt không dấu
                    temp.order = food.order;
                    temp.hide = food.hide;
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
            return View(food);
        }

        public Food getById(long id)
        {
            return db.Foods.Where(x => x.id == id).FirstOrDefault();

        }

        // GET: admin/Foods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // POST: admin/Foods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Food food = db.Foods.Find(id);

            // Lấy đường dẫn tới folder chứa file hình ảnh
            string imagePath = Server.MapPath("~/Uploads/images/foods");

            // Xóa file hình ảnh có tên tương ứng với id của banner
            string imageName = food.img;
            // Console.Error.WriteLine(imageName);
            string fullPath = Path.Combine(imagePath, imageName);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            db.Foods.Remove(food);
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
