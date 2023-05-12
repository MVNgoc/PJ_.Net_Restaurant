using PJ_.Net_Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace PJ_.Net_Restaurant.Areas.admin.Controllers
{
    public class PublicController : Controller
    {

        RestaurantEntities db = new RestaurantEntities();

        // GET: admin/Pubblic
        public ActionResult Index()
        {
            if (Session["idUserAd"] != null)
            {
                return RedirectToRoute(new { Controller = "Default", action = "Index" });
            }
            return View();
        }

        // POST: admin/Pubblic
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var f_password = GetMD5(password);
                var data = db.Users.Where(s => s.email.Equals(email) && s.password.Equals(f_password) && s.Role.Equals("ADMIN")).ToList();
                if (data.Count() > 0)
                {
                    //add Session
                    Session["emailAd"] = data.FirstOrDefault().email;
                    Session["nameAd"] = data.FirstOrDefault().name;
                    Session["addressAd"] = data.FirstOrDefault().address;
                    Session["passwordAd"] = f_password;
                    Session["idUserAd"] = data.FirstOrDefault().id;
                    Session["roleAd"] = data.FirstOrDefault().Role;
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
            return RedirectToRoute(new { Controller = "Public", action = "Index" });
        }

        //GET: ForgotPass
        public ActionResult ForgotPass()
        {
            if (Session["idUserAd"] != null)
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
                User user = db.Users.FirstOrDefault(s => s.email == email && s.Role == "Admin");
                if (user == null)
                {
                    ViewData["error"] = "Email is not exists";
                    return View();
                }
                else
                {
                    ViewData["error"] = "We have sent an email on your registered email";
                    string s = GenerateRandomString(20);
                    user.token = s;
                    db.SaveChanges();

                    string subject = "Hello, reset your password.";
                    string body = "<p> Hello, </p>"
                        + "<p> Click on the following link to reset your password </p>"
                        + "<p>" + "https://localhost:44363/admin/Public/ChangePass?token=" + s + "</p>";
                    WebMail.Send(email, subject, body, null, null, null, true, null, null, null, null, null, null);
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


        //GET: ChangePass

        public ActionResult ChangePass(string token)
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

        //POST: ChangePass
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePass(User user)
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