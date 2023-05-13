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
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using PJ_.Net_Restaurant.Controllers;
using PJ_.Net_Restaurant.Help;
using PJ_.Net_Restaurant.Models;

namespace PJ_.Net_Restaurant.Areas.admin.Views.users
{
    public class UserController : BaseController
    {
        private RestaurantEntities db = new RestaurantEntities();

        // GET: admin/User
        public ActionResult Index()
        {
            return View(db.Users.OrderBy(s => s.Role).ToList());
        }

        // GET: admin/User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: admin/User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,email,password,name,phonenum,address,token,meta,hide,order,datebegin,Role")] User user)
        {
            var temp = "";
            if (ModelState.IsValid)
            {
                user.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                user.meta = "";
                user.order = null;
                user.token = "";
                user.hide = true;
                temp = user.password;
                // create an instance of the MD5 hash algorithm
                MD5 md5Hash = MD5.Create();

                // convert the input string to a byte array and compute the hash
                byte[] hashBytes = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(temp));

                // convert the byte array to a hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                string hashString = sb.ToString();
                user.password = hashString;
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: admin/User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: admin/User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,email,password,name,phonenum,address,token,meta,hide,order,datebegin,Role")] User user)
        {
            try
            {
                User temp = getById(user.id);
                if (ModelState.IsValid)
                {
                    // temp.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());                   
                    temp.name = user.name;
                    temp.email = user.email;
                    temp.phonenum = user.phonenum;
                    temp.address = user.address;
                    temp.Role = user.Role;
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
            return View(user);
        }
        public User getById(long id)
        {
            return db.Users.Where(x => x.id == id).FirstOrDefault();

        }

        // GET: admin/User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: admin/User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
