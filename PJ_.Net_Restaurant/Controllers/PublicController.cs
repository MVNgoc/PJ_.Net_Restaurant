using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using PJ_.Net_Restaurant.Models;


namespace PJ_.Net_Restaurant.Controllers
{
    public class PublicController : Controller
    {
        RestaurantEntities db = new RestaurantEntities();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        //POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var f_password = GetMD5(password);
                var data = db.Users.Where(s => s.email.Equals(email) && s.password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add Session
                    Session["email"] = data.FirstOrDefault().email;
                    Session["name"] = data.FirstOrDefault().name;
                    Session["address"] = data.FirstOrDefault().address;
                    Session["password"] = f_password;
                    Session["idUser"] = data.FirstOrDefault().id;
                    return RedirectToRoute(new { Controller = "Default", action = "Index" });

                }
                else
                {
                    ViewData["error"] = "Your email or password not correct";
                    return View();
                }
            }
            return View();
        }

        //GET: Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User _user)
        {
            if (ModelState.IsValid)
            {
                var check = db.Users.FirstOrDefault(s => s.email == _user.email);
                if (check == null)
                {
                    _user.password = GetMD5(_user.password);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.Users.Add(_user);
                    db.SaveChanges();
                    _user = null;
                    return RedirectToAction("index");
                }
                else
                {
                    ViewData["error"] = "Email already exists";
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

        //Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }

        //GET: Forgotpass
        public ActionResult ForgotPass()
        {
            return View();
        }

        //POST: Forgotpass
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Forgotpass(string email)
        {
            if (ModelState.IsValid)
            {
                var check = db.Users.FirstOrDefault(s => s.email == email);
                if(check == null)
                {
                    ViewData["error"] = "Email is not exists";
                    return View();
                }else
                {
                    ViewData["error"] = "We have sent an email on your registered email";
                    return View();
                }
            }

            return View();
        }

        public ActionResult ResetPass()
        {
            return View();
        }
    }
}