using PetrolPump.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetrolPump.Controllers
{
    public class OnlineMonitoringController : BaseController
    {
        PetrolPumpDbEntities db;
        AppSession session = (AppSession)System.Web.HttpContext.Current.Session["AppSession"];
        public OnlineMonitoringController()
        {
            db = new PetrolPumpDbEntities();
        }
        // GET: OnlineMonitoring
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Petrol() {
            return View();
        }
        public ActionResult Diesel() {
            return View();
        }
        public ActionResult HighOctrane()
        {
            return View();
        }

        public JsonResult getTanksCapacity(string t)
        {
            List<ViewModels.CylinderCharts> model = new List<ViewModels.CylinderCharts>();
            int typeId = 1;
            if (t == "petrol") {
                typeId = 1;
            }
            else if (t == "highOctane") {
                typeId = 2;
            }
            else if (t == "diesel") {
                typeId = 3;
            }
            List<tbl_Sensor> data = new List<tbl_Sensor>();
            if (session.User.RoleID == 1)
            {
                data = db.tbl_Sensor.Where(x => x.FuelTypeID == typeId && x.IsActive == true).ToList();
            }
            else
            {
                data = db.tbl_Sensor.Where(x => x.FuelTypeID == typeId && x.IsActive == true && x.PumpID == session.User.PumpID).ToList();
            }
            
            foreach (var item in data)
            {
                decimal percentage = 0;
                percentage = (Convert.ToDecimal(item.FuelPresents) / Convert.ToDecimal(item.TankCapacity)) * 100;
                ViewModels.CylinderCharts obj = new ViewModels.CylinderCharts();
                obj.category = item.SensorMAC + ", " + item.TankCapacity + "-" + item.FuelPresents;
                obj.value1 = percentage;
                obj.value2 = 100;
                model.Add(obj);
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}