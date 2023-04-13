using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Helpers;
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
            if(Session["idUser"] != null)
            {
                return RedirectToRoute(new { Controller = "Default", action = "Index" });
            }
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
            if (Session["idUser"] != null)
            {
                return RedirectToRoute(new { Controller = "Default", action = "Index" });
            }
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
            if (Session["idUser"] != null)
            {
                return RedirectToRoute(new { Controller = "Default", action = "Index" });
            }
            return View();
        }

        //POST: Forgotpass
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Forgotpass(string email)
        {
            if (ModelState.IsValid)
            {
                User user = db.Users.FirstOrDefault(s => s.email == email);
                if(user == null)
                {
                    ViewData["error"] = "Email is not exists";
                    return View();
                }else
                {
                    ViewData["error"] = "We have sent an email on your registered email";
                    string s = GenerateRandomString(20);
                    user.token = s;
                    db.SaveChanges();

                    string subject = "Hello, reset your password.";
                    string body = "<p> Hello, </p>"
                        + "<p> Click on the following link to reset your password </p>"
                        + "<p>"+ "https://localhost:44363/resetpass?token=" + s +"</p>";
                    WebMail.Send(email, subject, body,null,null,null,true,null,null,null,null,null,null);
                    return View();
                }
            }

            return View();
        }

        public string GenerateRandomString(int length)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        //GET: ResetPass
        public ActionResult ResetPass(string token)
        {
            if (token == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.FirstOrDefault(s => s.token == token);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //POST: ResetPass
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPass(User user)
        {
            if (ModelState.IsValid)
            {
                var f_password = GetMD5(user.password);
                var data = db.Users.Where(s => s.id == user.id).ToList();
                data.FirstOrDefault().password = f_password;
                data.FirstOrDefault().token = "";
                db.SaveChanges();

                ViewData["error"] = "Change Password Success";
                return View();
            }
            return View();
        }
    }
}