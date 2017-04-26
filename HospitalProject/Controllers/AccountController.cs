using HospitalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HospitalProject.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(AdminUser adminuser)
        {
            HospitalContext db = new HospitalContext();
            var count = db.AdminUsers.Where(u => u.Username == adminuser.Username && u.Password == adminuser.Password).Count();

            if (count == 0)
            {
                ViewBag.Message = "Invalid User";
                return View();
            }
            else
            {
                FormsAuthentication.SetAuthCookie(adminuser.Username, false);
                return RedirectToAction("Index", "News");

                //hey josh. You just have to create a view for this now. Slide 25/36. You're doing great man!
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}