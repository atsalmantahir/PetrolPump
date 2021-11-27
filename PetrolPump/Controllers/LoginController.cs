using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetrolPump.Models;
using PetrolPump.Helpers;

namespace PetrolPump.Controllers
{
    public class LoginController : Controller
    {
        PetrolPumpDbEntities db;
        public LoginController()
        {
            db = new PetrolPumpDbEntities();
        }
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(tbl_User user)
        {
            AppSession session = new AppSession();
            string result = "";
            var data = db.tbl_User.Where(x=>x.Email == user.Email && x.Password == user.Password).FirstOrDefault();
            if (data != null)
            {
                if (data.IsActive != true)
                {
                    result = "User is InActive, Contact Administrator";
                }
                else {
                    //CREATE SESSION
                    session = Helpers.Session.CreateSession(data);
                    Session.Add("AppSession", session);
                    result = "Authenticated";
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                result = "Invalid Credentials";
            }
            TempData["LoginResult"] = result;
            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            if (System.Web.HttpContext.Current.Session["AppSession"] != null)
            {
                Session.Add("AppSession", null);
            }
            Session.Abandon();
            //Response.Redirect("Index");
            return RedirectToAction("Index", "Login");
        }

    }
}