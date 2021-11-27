using PetrolPump.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetrolPump.Controllers
{
    public class FuelTypeController : BaseController
    {
        PetrolPumpDbEntities db;
        public FuelTypeController()
        {
            db = new PetrolPumpDbEntities();
        }
        // GET: FuelType
        public ActionResult Index()
        {
            var data = db.tbl_FuelType.OrderByDescending(x => x.FuelTypeID).ToList();
            return View(data);
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var data = db.tbl_FuelType.Where(x => x.FuelTypeID == id).FirstOrDefault();
            return View(data);
        }


        [HttpPost]
        public ActionResult Add(tbl_FuelType obj)
        {
            if (ModelState.IsValid)
            {
                tbl_FuelType data = new tbl_FuelType();
                data.FuelType = obj.FuelType;
                data.PurchasePrice = obj.PurchasePrice;
                data.SalePrice = obj.SalePrice;

                db.tbl_FuelType.Add(data);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            

            return View(obj);
        }


        [HttpPost]
        public ActionResult Edit(tbl_FuelType obj)
        {
            if (ModelState.IsValid)
            {

                tbl_FuelType data = db.tbl_FuelType.Where(x => x.FuelTypeID == obj.FuelTypeID).FirstOrDefault();
                data.FuelType = obj.FuelType;
                data.PurchasePrice = obj.PurchasePrice;
                data.SalePrice = obj.SalePrice;

                db.tbl_FuelType.Attach(data);
                db.Entry(data).State = EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("Index");
            }


            return View(obj);
        }
    }
}