using PetrolPump.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetrolPump.Helpers;

namespace PetrolPump.Controllers
{
    public class SupplierController : BaseController
    {
        PetrolPumpDbEntities db;
        AppSession session = (AppSession)System.Web.HttpContext.Current.Session["AppSession"];
        public SupplierController()
        {
            db = new PetrolPumpDbEntities();
        }
        // GET: Supplier
        public ActionResult Index()
        {
            var data = db.tbl_Supplier.Where(x => x.PumpID == session.User.PumpID).OrderByDescending(x => x.SupplierID).ToList();
            return View(data);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int ID)
        {
            var data = db.tbl_Supplier.Where(x => x.SupplierID == ID).FirstOrDefault();
            return View(data);
        }

        [HttpPost]
        public ActionResult Add(tbl_Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                tbl_Supplier data = new tbl_Supplier();
                data.PumpID = session.User.PumpID;
                data.Title = supplier.Title;
                data.Address = supplier.Address;
                data.Phone = supplier.Phone;
                data.Email = supplier.Email;
                data.CreatedDate = DateTime.Now;
                data.IsActive = true;

                db.tbl_Supplier.Add(data);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(supplier);
        }

        [HttpPost]
        public ActionResult Edit(tbl_Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                tbl_Supplier data = db.tbl_Supplier.Where(x => x.SupplierID == supplier.SupplierID).FirstOrDefault();
                data.Title = supplier.Title;
                data.Address = supplier.Address;
                data.Phone = supplier.Phone;
                data.Email = supplier.Email;

                db.tbl_Supplier.Attach(data);
                db.Entry(data).State = EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("Index");
            }



            return View(supplier);
        }
    }
}