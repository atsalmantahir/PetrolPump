using PetrolPump.Helpers;
using PetrolPump.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetrolPump.Controllers
{
    public class PurchaseController : BaseController
    {
        PetrolPumpDbEntities db;
        AppSession session = (AppSession)System.Web.HttpContext.Current.Session["AppSession"];
        public PurchaseController()
        {
            db = new PetrolPumpDbEntities();
        }
        // GET: Purchase
        public ActionResult Index()
        {
            var data = db.tbl_Stock.OrderByDescending(x => x.StockID)
                .Where(x => x.IsPurchased == true && x.tbl_Sensor.PumpID == session.User.PumpID).ToList();
            return View(data);
        }


        public ActionResult Add()
        {
            var tank = db.tbl_Sensor.Where(x => x.IsActive == true && x.PumpID == session.User.PumpID).OrderByDescending(x => x.SensorID).ToList();
            ViewBag.Tank = tank;

            var supplier = db.tbl_Supplier.Where(x => x.IsActive == true && x.PumpID == session.User.PumpID).OrderByDescending(x => x.SupplierID).ToList();
            ViewBag.Supplier = supplier;

            return View();
        }

        [HttpPost]
        public ActionResult Add(tbl_Stock stock)
        {
            tbl_Stock data = new tbl_Stock();
            data.OrderType = "Purchase";
            data.SensorID = stock.SensorID;
            data.IsPurchased = true;
            data.SupplierID = stock.SupplierID;
            data.CustomerID = null;
            data.IsSaled = false;
            data.Quantity = stock.Quantity;
            data.PurchasePrice = stock.PurchasePrice;

            data.CreatedDate = DateTime.Now.Date;


            bool result = Stock.updateStocks(data.SensorID, data.OrderType, data.Quantity);
            if (result == true)
            {
                db.tbl_Stock.Add(data);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                var tank = db.tbl_Sensor.Where(x => x.IsActive == true && x.PumpID == session.User.PumpID).OrderByDescending(x => x.SensorID).ToList();
                ViewBag.Tank = tank;

                var supplier = db.tbl_Supplier.Where(x => x.IsActive == true && x.PumpID == session.User.PumpID).OrderByDescending(x => x.SupplierID).ToList();
                ViewBag.Supplier = supplier;

                TempData["Result"] = "Purchase quantity can not be greater than Tank Maximum Capacity";
                return View();
            }


        }

        [HttpGet]
        public JsonResult getTank(int ID)
        {
            var sensor = db.tbl_Sensor.Where(x => x.SensorID == ID).FirstOrDefault();
            var data = db.tbl_FuelType.Where(x => x.FuelTypeID == sensor.FuelTypeID).FirstOrDefault();
            ViewModels.FuelType obj = new ViewModels.FuelType();
            obj.Title = data.FuelType;
            obj.TotalAmount = data.PurchasePrice;
            obj.TankCapacity = sensor.TankCapacity;
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}