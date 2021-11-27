using PetrolPump.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetrolPump.Controllers
{
    public class UsersController : BaseController
    {
        PetrolPumpDbEntities db;
        public UsersController()
        {
            db = new PetrolPumpDbEntities();
        }
        // GET: Users
        public ActionResult Index()
        {
            var users = db.tbl_User.Where(x => x.RoleID > 1).ToList();
            return View(users);
        }

        public ActionResult Add()
        {
            var roles = db.tbl_Role.Where(x => x.RoleID > 1).ToList();
            ViewBag.Roles = roles;

            var pumps = db.tbl_Pump.OrderBy(x => x.tbl_Company.CompanyName).ToList();
            ViewBag.Pumps = pumps;


            return View();
        }

        [HttpPost]
        public ActionResult Add(tbl_User user)
        {
            if (ModelState.IsValid)
            {
                tbl_User data = new tbl_User();
                data.PumpID = user.PumpID;
                data.FirstName = user.FirstName.Trim();
                data.LastName = user.LastName.Trim();
                data.Email = user.Email.Trim();
                data.Username = user.Username.Trim();
                data.Password = user.Password.Trim();
                data.Phone = user.Phone.Trim();
                data.RoleID = user.RoleID;
                data.CreatedAt = DateTime.Now;
                data.IsActive = true;

                db.tbl_User.Add(data);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            var roles = db.tbl_Role.Where(x => x.RoleID > 1).ToList();
            ViewBag.Roles = roles;


            var pumps = db.tbl_Pump.OrderBy(x => x.tbl_Company.CompanyName).ToList();
            ViewBag.Pumps = pumps;

            return View(user);
        }

        public ActionResult Edit(int ID)
        {
            var roles = db.tbl_Role.Where(x => x.RoleID > 1).ToList();
            ViewBag.Roles = roles;


            var pumps = db.tbl_Pump.OrderBy(x => x.tbl_Company.CompanyName).ToList();
            ViewBag.Pumps = pumps;

            var data = db.tbl_User.Where(x => x.UserID == ID).FirstOrDefault();

            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(tbl_User user)
        {
            if (ModelState.IsValid)
            {
                tbl_User data = db.tbl_User.Where(x => x.UserID == user.UserID).FirstOrDefault();
                data.FirstName = user.FirstName.Trim();
                data.LastName = user.LastName.Trim();
                data.Email = user.Email.Trim();
                data.Username = user.Username.Trim();
                data.Password = user.Password.Trim();
                data.Phone = user.Phone.Trim();
                data.RoleID = user.RoleID;

                data.PumpID = user.PumpID;

                db.tbl_User.Attach(data);
                db.Entry(data).State = EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            var roles = db.tbl_Role.Where(x => x.RoleID > 1).ToList();
            ViewBag.Roles = roles;


            var pumps = db.tbl_Pump.OrderBy(x => x.tbl_Company.CompanyName).ToList();
            ViewBag.Pumps = pumps;

            return View(user);
        }

        //SOFT DELETE
        [HttpGet]
        public JsonResult UpdateStatus(int ID)
        {
            var data = db.tbl_User.Where(x => x.UserID == ID).FirstOrDefault();
            
            if (data.IsActive != true)
            {
                data.IsActive = true;
            }
            else
            {
                data.IsActive = false;
            }

            db.tbl_User.Attach(data);
            db.Entry(data).State = EntityState.Modified;
            db.SaveChanges();

            return Json("Record Status Updated");
        }
    }
}