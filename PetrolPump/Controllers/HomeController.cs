using PetrolPump.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetrolPump.Controllers
{
    public class HomeController : BaseController
    {
        PetrolPumpDbEntities db;
        AppSession session = (AppSession)System.Web.HttpContext.Current.Session["AppSession"];
        public HomeController()
        {
            db = new PetrolPumpDbEntities();
        }
        public ActionResult Index()
        {
            
            return View();
        }

        public JsonResult Fill()
        {
            ViewModels.KPI model = new ViewModels.KPI();
            if (session.User.RoleID == 1)
            {
                model.Pumps = db.tbl_Pump.Where(x => x.IsActive == true).Count().ToString();
                model.ActiveSensor = db.tbl_Sensor.Where(x => x.IsActive == true).Count().ToString();
                model.TotalSales = totalSales().ToString();
                model.TotalPurchases = totalPurchases().ToString();
            }
            else
            {
                model.Pumps = db.tbl_Pump.Where(x => x.IsActive == true && x.PumpID == session.User.PumpID).Count().ToString();
                model.ActiveSensor = db.tbl_Sensor.Where(x => x.IsActive == true && x.PumpID == session.User.PumpID).Count().ToString();
                model.TotalSales = totalSales().ToString();
                model.TotalPurchases = totalPurchases().ToString();
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaledFuelTypes()
        {
            List<tbl_Stock> stocks = new List<tbl_Stock>();
            ViewModels.FuelTypes model = new ViewModels.FuelTypes();
            List<string> title = new List<string>();
            List<string> totalAmount = new List<string>();
            var fuelType = db.tbl_FuelType.ToList();
            foreach (var item in fuelType)
            {
                decimal? result = 0;
                if (session.User.RoleID == 1)
                {
                    stocks = db.tbl_Stock.Where(x => x.IsSaled == true && x.tbl_Sensor.tbl_FuelType.FuelTypeID == item.FuelTypeID).ToList();
                }
                else
                {
                    stocks = db.tbl_Stock.Where(x => x.IsSaled == true && x.tbl_Sensor.tbl_FuelType.FuelTypeID == item.FuelTypeID
                    && x.tbl_Sensor.PumpID == session.User.PumpID).ToList();
                }
                foreach (var item2 in stocks.Where(x => x.tbl_Sensor.FuelTypeID == item.FuelTypeID))
                {
                    result += item2.Quantity * item2.SalePrice;
                }
                totalAmount.Add(result.ToString());
                title.Add(item.FuelType);
            }
            model.Title = title;
            model.TotalAmount = totalAmount;
            return Json(model, JsonRequestBehavior.AllowGet);
        }


        public JsonResult PurchasedFuelTypes()
        {
            List<tbl_Stock> stocks = new List<tbl_Stock>();
            ViewModels.FuelTypes model = new ViewModels.FuelTypes();
            List<string> title = new List<string>();
            List<string> totalAmount = new List<string>();
            var fuelType = db.tbl_FuelType.ToList();
            foreach (var item in fuelType)
            {
                decimal? result = 0;
                if (session.User.RoleID == 1)
                {
                    stocks = db.tbl_Stock.Where(x => x.IsPurchased == true && x.tbl_Sensor.tbl_FuelType.FuelTypeID == item.FuelTypeID).ToList();
                }
                else
                {
                    stocks = db.tbl_Stock.Where(x => x.IsPurchased == true && x.tbl_Sensor.tbl_FuelType.FuelTypeID == item.FuelTypeID
                    && x.tbl_Sensor.PumpID == session.User.PumpID).ToList();
                }
                foreach (var item2 in stocks.Where(x => x.tbl_Sensor.FuelTypeID == item.FuelTypeID))
                {
                    result += item2.Quantity * item2.PurchasePrice;
                }
                totalAmount.Add(result.ToString());
                title.Add(item.FuelType);
            }
            model.Title = title;
            model.TotalAmount = totalAmount;
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private decimal? totalSales()
        {
            List<tbl_Stock> stocks = new List<tbl_Stock>();
            var fuelTypes = db.tbl_FuelType.ToList();
            decimal? result = 0;
            foreach (var item in fuelTypes)
            {
                if (session.User.RoleID == 1)
                {
                    stocks = db.tbl_Stock.Where(x => x.IsSaled == true && x.tbl_Sensor.tbl_FuelType.FuelTypeID == item.FuelTypeID).ToList();
                }
                else
                {
                    stocks = db.tbl_Stock.Where(x => x.IsSaled == true && x.tbl_Sensor.tbl_FuelType.FuelTypeID == item.FuelTypeID 
                    && x.tbl_Sensor.PumpID == session.User.PumpID).ToList();
                }
                foreach (var item2 in stocks)
                {
                    result += item2.SalePrice * item2.Quantity;
                }
            }
            return result;
        }

        private decimal? totalPurchases()
        {
            List<tbl_Stock> stocks = new List<tbl_Stock>();
            var fuelTypes = db.tbl_FuelType.ToList();
            decimal? result = 0;
            foreach (var item in fuelTypes)
            {
                if (session.User.RoleID == 1)
                {
                    stocks = db.tbl_Stock.Where(x => x.IsPurchased == true && x.tbl_Sensor.tbl_FuelType.FuelTypeID == item.FuelTypeID).ToList();
                }
                else
                {
                    stocks = db.tbl_Stock.Where(x => x.IsPurchased == true && x.tbl_Sensor.tbl_FuelType.FuelTypeID == item.FuelTypeID
                    && x.tbl_Sensor.PumpID == session.User.PumpID).ToList();
                }
                foreach (var item2 in stocks)
                {
                    result += item2.PurchasePrice * item2.Quantity;
                }
            }
            return result;
        }
    }
}