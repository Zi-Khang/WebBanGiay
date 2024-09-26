using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn.Models;
namespace DoAn.Controllers
{
    public class AccountController : Controller
    {
        GiayEntities db = new GiayEntities();
        // GET: /Account/

        public ActionResult Login()
        {
            if (HttpContext.Session["UserName"] == null)
            {
                return View();
            }
            else
                return RedirectToAction("Index", "Home");
        }
        [HttpPost]

        public ActionResult Login(User u)
        {
            if (HttpContext.Session["UserName"] == null)
            {
                User user = db.Users.Where(p => p.Username == u.Username && p.Password == u.Password).FirstOrDefault();
                if (user != null)
                {
                    HttpContext.Session["UserName"] = user.Username.ToString();
                    if (user.Role == "Customer")
                    {
                        HttpContext.Session["ID"] = user.UserID;
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
            
        }
        public ActionResult Logout()
        {
            HttpContext.Session.Abandon();
            return RedirectToAction("Login", "Account");
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User u)
        {
            db.Users.Add(u);
            db.SaveChanges();
            return RedirectToAction("Login", "Account");
        }

    }
}
