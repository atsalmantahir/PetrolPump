using PetrolPump.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetrolPump.Controllers
{
    public class ReportsController : BaseController
    {
        PetrolPumpDbEntities db;
        AppSession session = (AppSession)System.Web.HttpContext.Current.Session["AppSession"];
        public ReportsController()
        {
            db = new PetrolPumpDbEntities();
        }
        // GET: Reports
        public ActionResult Index()
        {
            List<tbl_Stock> data = new List<tbl_Stock>();
            if (session.User.RoleID == 1)
            {
                data = db.tbl_Stock.OrderByDescending(x => x.StockID).ToList();
            }
            else
            {
                data = db.tbl_Stock.OrderByDescending(x => x.StockID).Where(y => y.tbl_Sensor.PumpID == session.User.PumpID).ToList();
            }
            return View(data);
        }
        
        [HttpGet]
        public JsonResult saleReport()
        {
            List<tbl_Stock> data = new List<tbl_Stock>();
            if (session.User.RoleID == 1)
            {
                data = db.tbl_Stock
                    .Where(x => x.IsSaled == true)
                    .OrderByDescending(x => x.StockID).ToList();
            }
            else
            {
                data = db.tbl_Stock
                    .Where(y => y.tbl_Sensor.PumpID == session.User.PumpID && y.IsSaled == true)
                    .OrderByDescending(x => x.StockID)
                    .ToList();
            }

            List<ViewModels.SalesReport> list = new List<ViewModels.SalesReport>();
            foreach (var item in data)
            {
                ViewModels.SalesReport obj = new ViewModels.SalesReport();
                obj.CreatedDate = item.CreatedDate;
                obj.FuelType = item.tbl_Sensor.tbl_FuelType.FuelType;
                obj.OrderType = item.OrderType;
                obj.Quantity = item.Quantity;
                obj.SalePrice = item.tbl_Sensor.tbl_FuelType.SalePrice;
                obj.Title = item.tbl_Customer.Title;
                list.Add(obj);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult saleReportFilter(string date)
        {

            string startDate = date.Substring(0, date.LastIndexOf(" - ") + 1);
            string endDate = date.Substring(date.LastIndexOf(" - ") + 1);
            endDate = endDate.Replace("-", "");
            DateTime start = Convert.ToDateTime(startDate);
            DateTime end = Convert.ToDateTime(endDate);
            start = start.Date;
            end = end.Date;
            List<tbl_Stock> data = new List<tbl_Stock>();
            if (session.User.RoleID == 1)
            {
                data = db.tbl_Stock
                    .Where(y => y.CreatedDate >= start && y.CreatedDate <= end && y.IsSaled == true)
                    .OrderByDescending(x => x.StockID).ToList();
            }
            else
            {
                data = db.tbl_Stock
                    .Where(y => y.tbl_Sensor.PumpID == session.User.PumpID && y.IsSaled == true 
                    && y.CreatedDate >= start && y.CreatedDate <= end)
                    .OrderByDescending(x => x.StockID)
                    .ToList();
            }
            List<ViewModels.SalesReport> list = new List<ViewModels.SalesReport>();
            foreach (var item in data)
            {
                ViewModels.SalesReport obj = new ViewModels.SalesReport();
                obj.CreatedDate = item.CreatedDate;
                obj.FuelType = item.tbl_Sensor.tbl_FuelType.FuelType;
                obj.OrderType = item.OrderType;
                obj.Quantity = item.Quantity;
                obj.SalePrice = item.tbl_Sensor.tbl_FuelType.SalePrice;
                obj.Title = item.tbl_Customer.Title;
                list.Add(obj);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult purchaseReport()
        {
            List<tbl_Stock> data = new List<tbl_Stock>();
            if (session.User.RoleID == 1)
            {
                data = db.tbl_Stock
                    .Where(x => x.IsPurchased == true)
                    .OrderByDescending(x => x.StockID).ToList();
            }
            else
            {
                data = db.tbl_Stock
                    .Where(y => y.tbl_Sensor.PumpID == session.User.PumpID && y.IsPurchased == true)
                    .OrderByDescending(x => x.StockID)
                    .ToList();
            }

            List<ViewModels.PurchaseReport> list = new List<ViewModels.PurchaseReport>();
            foreach (var item in data)
            {
                ViewModels.PurchaseReport obj = new ViewModels.PurchaseReport();
                obj.CreatedDate = item.CreatedDate;
                obj.FuelType = item.tbl_Sensor.tbl_FuelType.FuelType;
                obj.OrderType = item.OrderType;
                obj.Quantity = item.Quantity;
                obj.PurchasePrice = item.tbl_Sensor.tbl_FuelType.PurchasePrice;
                obj.Title = item.tbl_Supplier.Title;
                list.Add(obj);

            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult purchaseReportFilter(string date)
        {
            string startDate = date.Substring(0, date.LastIndexOf(" - ") + 1);
            string endDate = date.Substring(date.LastIndexOf(" - ") + 1);
            endDate = endDate.Replace("-", "");
            DateTime start = Convert.ToDateTime(startDate);
            DateTime end = Convert.ToDateTime(endDate);
            List<tbl_Stock> data = new List<tbl_Stock>();
            if (session.User.RoleID == 1)
            {
                data = db.tbl_Stock
                    .Where(y => y.CreatedDate >= start && y.CreatedDate <= end && y.IsPurchased == true)
                    .OrderByDescending(x => x.StockID).ToList();
            }
            else
            {
                data = db.tbl_Stock
                    .Where(y => y.tbl_Sensor.PumpID == session.User.PumpID && y.IsPurchased == true && y.CreatedDate >= start && y.CreatedDate <= end)
                    .OrderByDescending(x => x.StockID)
                    .ToList();
            }
            List<ViewModels.PurchaseReport> list = new List<ViewModels.PurchaseReport>();
            foreach (var item in data)
            {
                ViewModels.PurchaseReport obj = new ViewModels.PurchaseReport();
                obj.CreatedDate = item.CreatedDate;
                obj.FuelType = item.tbl_Sensor.tbl_FuelType.FuelType;
                obj.OrderType = item.OrderType;
                obj.Quantity = item.Quantity;
                obj.PurchasePrice = item.tbl_Sensor.tbl_FuelType.PurchasePrice;
                obj.Title = item.tbl_Supplier.Title;
                list.Add(obj);

            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExpenseReport()
        {
            var data = db.tbl_Expense.OrderByDescending(x => x.ExpenseID).ToList();
            return View(data);
        }


        public ActionResult SalaryReport()
        {
            var data = db.tbl_EmployeeSalary.OrderByDescending(x => x.EmployeeID).ToList();
            return View(data);
        }
    }
}