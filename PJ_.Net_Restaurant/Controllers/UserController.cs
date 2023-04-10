using System;
using System.Collections.Generic;
using System.Linq;
using PJ_.Net_Restaurant.Models;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace PJ_.Net_Restaurant.Controllers
{
    public class UserController : Controller
    {

        RestaurantEntities db = new RestaurantEntities();

        // GET: User
        public ActionResult Index()
        {
            var s = Session["idUser"];
            User user = db.Users.Find(s);
            
            return View(user);
        }

        // POST: User
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(User _user)
        {
            User user = getById(_user.id);

            if (ModelState.IsValid)
            {
                user.name = _user.name;
                user.address = _user.address;

                if (user.email != _user.email)
                {
                    var check = db.Users.FirstOrDefault(s => s.email == _user.email);
                    if (check == null)
                    {
                        user.email = _user.email;
                        Session["email"] = user.email;
                        Session["name"] = user.name;
                        Session["address"] = user.address;
                        db.Configuration.ValidateOnSaveEnabled = false;
                        db.SaveChanges();
                        return RedirectToAction("index");
                    }
                    else
                    {
                        ViewData["error"] = "Email already exists";
                        return View();
                    }
                }
                else
                {
                    Session["name"] = user.name;
                    Session["address"] = user.address;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();
                    return RedirectToAction("index");
                }
            }
            return View();
        }

        public User getById(int id)
        {
            return db.Users.Where(x => x.id == id).FirstOrDefault();

        }

        //GET: ChangePass
        public ActionResult ChangePass()
        {
            return View();
        }

        //POST: ChangePass
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePass(ChangePass _user)
        {
            if (ModelState.IsValid)
            {
                var f_password = GetMD5(_user.password);
                var email = Session["email"];
                var data = db.Users.Where(s => s.email == email && s.password == f_password).ToList();
                if (data.Count > 0)
                {
                    _user.newpassword = GetMD5(_user.newpassword);
                    db.Configuration.ValidateOnSaveEnabled = false;

                    data.FirstOrDefault().password = _user.newpassword;
                    db.SaveChanges();


                    ViewData["error"] = "Change Password Success";
                    return View();
                }
                else
                {
                    ViewData["error"] = "Your password not correct";
                    return View();
                }
            }

            return View();
        }

        //CREATE A STRING MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");
            }
            return byte2String;
        }
    }
}