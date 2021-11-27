using PetrolPump.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetrolPump.Controllers
{
    public class CustomerController : BaseController
    {
        PetrolPumpDbEntities db;
        AppSession session = (AppSession)System.Web.HttpContext.Current.Session["AppSession"];
        public CustomerController()
        {
            db = new PetrolPumpDbEntities();
        }

        // GET: Customer
        public ActionResult Index()
        {
            var data = db.tbl_Customer.Where(x => x.PumpID == session.User.PumpID).OrderByDescending(x => x.CustomerID).ToList();
            return View(data);
        }

        public ActionResult Add()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Edit(int ID)
        {
            var data = db.tbl_Customer.Where(x => x.CustomerID == ID).FirstOrDefault();
            return View(data);
        }

        [HttpPost]
        public ActionResult Add(tbl_Customer customer)
        {
            if (ModelState.IsValid)
            {
                tbl_Customer data = new tbl_Customer();
                data.PumpID = session.User.PumpID;
                data.Title = customer.Title;
                data.Address = customer.Address;
                data.Phone = customer.Phone;
                data.Email = customer.Email;
                data.CreatedDate = DateTime.Now;
                data.IsActive = true;

                db.tbl_Customer.Add(data);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(customer);
        }

        [HttpPost]
        public ActionResult Edit(tbl_Customer customer)
        {
            if (ModelState.IsValid)
            {
                tbl_Customer data = db.tbl_Customer.Where(x => x.CustomerID == customer.CustomerID).FirstOrDefault();
                data.Title = customer.Title;
                data.Address = customer.Address;
                data.Phone = customer.Phone;
                data.Email = customer.Email;

                db.tbl_Customer.Attach(data);
                db.Entry(data).State = EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            


            return View(customer);
        }
    }
}