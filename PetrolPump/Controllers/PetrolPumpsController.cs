using PetrolPump.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using PetrolPump.Helpers;
using System.Web.Mvc;

namespace PetrolPump.Controllers
{
    public class PetrolPumpsController : BaseController
    {
        PetrolPumpDbEntities db;
        PetrolPump.Models.AppSession session = (PetrolPump.Models.AppSession)System.Web.HttpContext.Current.Session["AppSession"];
        public PetrolPumpsController()
        {
            db = new PetrolPumpDbEntities();
        }
        // GET: Pumps
        #region Pumps
        public ActionResult Index()
        {
            List<tbl_Pump> data = new List<tbl_Pump>();
            if (session.User.RoleID > 1)
            {
                data = db.tbl_Pump.Where(x=>x.PumpID == session.User.PumpID).OrderByDescending(x => x.PumpID).ToList();
            }
            else
            {
                data = db.tbl_Pump.OrderByDescending(x => x.PumpID).ToList();
            }
            

            var fuelTypes = db.tbl_FuelType.OrderBy(x => x.FuelType).ToList();
            ViewBag.FuelTypes = fuelTypes;

            return View(data);
        }

        public ActionResult Add()
        {
            var company = db.tbl_Company.OrderBy(x => x.CompanyName).ToList();
            var fuelTypes = db.tbl_FuelType.OrderBy(x => x.FuelType).ToList();
            ViewBag.Comapny = company;
            ViewBag.FuelTypes = fuelTypes;
            return View();
        }


        [HttpPost]
        public ActionResult Add(tbl_Pump pump)
        {
            if (ModelState.IsValid)
            {
                int id = db.tbl_Pump.Count() > 0 ? db.tbl_Pump.Max(x => x.PumpID) + 1 : 1;
               
                tbl_Pump data = new tbl_Pump();
                data.CompanyID = pump.CompanyID;
                data.Location = Common.GenerateTitle(id, pump.Location);
                data.CreatedDate = DateTime.Now;
                data.IsActive = true;

                db.tbl_Pump.Add(data);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            var company = db.tbl_Company.OrderBy(x => x.CompanyName).ToList();
            var fuelTypes = db.tbl_FuelType.OrderBy(x => x.FuelType).ToList();
            ViewBag.Comapny = company;
            ViewBag.FuelTypes = fuelTypes;

            return View(pump);
        }

        
        public ActionResult Edit(int ID)
        {
            var company = db.tbl_Company.OrderBy(x => x.CompanyName).ToList();
            var fuelTypes = db.tbl_FuelType.OrderBy(x => x.FuelType).ToList();

            ViewBag.Comapny = company;
            ViewBag.FuelTypes = fuelTypes;

            var data = db.tbl_Pump.Where(x => x.PumpID == ID).FirstOrDefault();

            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(tbl_Pump pump)
        {
            if (ModelState.IsValid)
            {
                tbl_Pump data = db.tbl_Pump.Where(x => x.PumpID == pump.PumpID).FirstOrDefault();
                data.Location = pump.Location.Trim();
                data.CompanyID = pump.CompanyID;

                db.tbl_Pump.Attach(data);
                db.Entry(data).State = EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            var company = db.tbl_Company.OrderBy(x => x.CompanyName).ToList();
            var fuelTypes = db.tbl_FuelType.OrderBy(x => x.FuelType).ToList();

            ViewBag.Comapny = company;
            ViewBag.FuelTypes = fuelTypes;


            return View(pump);
        }
        #endregion

        #region FuelTanks
        public ActionResult AddFuelTank()
        {
            var fuelTypes = db.tbl_FuelType.OrderBy(x => x.FuelType).ToList();
            ViewBag.FuelTypes = fuelTypes;

            var pumps = db.tbl_Pump.OrderBy(x => x.tbl_Company.CompanyName).ToList();
            ViewBag.Pumps = pumps;

            return View();
        }


        public ActionResult GetFuelTanks()
        {
            var sensors = db.tbl_Sensor.Where(x => x.IsActive == true).OrderByDescending(x => x.SensorID).ToList();
            return View(sensors);
        }

        [HttpPost]
        public ActionResult AddFuelTank(tbl_Sensor sensor)
        {
            var a = db.tbl_Sensor.Where(x => x.SensorMAC == sensor.SensorMAC).FirstOrDefault();
            if (a != null)
            {
                var fuelTypes = db.tbl_FuelType.OrderBy(x => x.FuelType).ToList();
                ViewBag.FuelTypes = fuelTypes;

                var pumps = db.tbl_Pump.OrderBy(x => x.tbl_Company.CompanyName).ToList();
                ViewBag.Pumps = pumps;

                TempData["Result"] = "Sensor with same MAC ID already exists";
                return View();
            }
            else
            {
                int id = db.tbl_Sensor.Count() > 0 ? db.tbl_Sensor.Max(x => x.SensorID) + 1 : 1;
                tbl_Sensor data = new tbl_Sensor();
                data.PumpID = sensor.PumpID;
                data.FuelTypeID = sensor.FuelTypeID;
                data.SensorMAC = sensor.SensorMAC;
                //data.TankTitle = sensor.TankTitle;
                data.TankTitle = "Tank" + id;
                data.TankCapacity = sensor.TankCapacity;
                data.FuelPresents = 0;
                data.IsActive = sensor.IsActive;
                data.CreatedDate = DateTime.Now;
                data.IsActive = true;

                db.tbl_Sensor.Add(data);
                db.SaveChanges();

                return RedirectToAction("GetFuelTanks");
            }

            //if (ModelState.IsValid)
            //{
            //    tbl_Sensor data = new tbl_Sensor();
            //    data.PumpID = sensor.PumpID;
            //    data.FuelTypeID = sensor.FuelTypeID;
            //    data.SensorMAC = sensor.SensorMAC;
            //    data.TankTitle = sensor.TankTitle;
            //    data.TankCapacity = sensor.TankCapacity;
            //    data.IsActive = sensor.IsActive;
            //    data.CreatedDate = DateTime.Now;
            //    data.IsActive = true;

            //    db.tbl_Sensor.Add(data);
            //    db.SaveChanges();

            //    return RedirectToAction("Index");
            //}

            //var company = db.tbl_Company.OrderBy(x => x.CompanyName).ToList();
            //var fuelTypes = db.tbl_FuelType.OrderBy(x => x.FuelType).ToList();
            //ViewBag.Comapny = company;
            //ViewBag.FuelTypes = fuelTypes;

            //return View(pump);
        }

        [HttpGet]
        public ActionResult EditFuelTank(int id)
        {
            var fuelTypes = db.tbl_FuelType.OrderBy(x => x.FuelType).ToList();
            ViewBag.FuelTypes = fuelTypes;

            var pumps = db.tbl_Pump.OrderBy(x => x.tbl_Company.CompanyName).ToList();
            ViewBag.Pumps = pumps;

            var data = db.tbl_Sensor.Where(x => x.SensorID == id).FirstOrDefault();
            return View(data);
        }

        [HttpPost]
        public ActionResult EditFuelTank(tbl_Sensor sensor)
        {
            tbl_Sensor data = db.tbl_Sensor.Where(x => x.SensorID == sensor.SensorID).FirstOrDefault();
            data.PumpID = sensor.PumpID;
            data.FuelTypeID = sensor.FuelTypeID;
            //data.SensorMAC = sensor.SensorMAC;
            //data.TankTitle = sensor.TankTitle;
            data.TankCapacity = sensor.TankCapacity;
            data.IsActive = sensor.IsActive;
            data.CreatedDate = DateTime.Now;
            data.IsActive = true;


            db.tbl_Sensor.Attach(data);
            db.Entry(data).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("GetFuelTanks");

            //if (ModelState.IsValid)
            //{
            //    tbl_Sensor data = new tbl_Sensor();
            //    data.PumpID = sensor.PumpID;
            //    data.FuelTypeID = sensor.FuelTypeID;
            //    data.SensorMAC = sensor.SensorMAC;
            //    data.TankTitle = sensor.TankTitle;
            //    data.TankCapacity = sensor.TankCapacity;
            //    data.IsActive = sensor.IsActive;
            //    data.CreatedDate = DateTime.Now;
            //    data.IsActive = true;

            //    db.tbl_Sensor.Add(data);
            //    db.SaveChanges();

            //    return RedirectToAction("Index");
            //}

            //var company = db.tbl_Company.OrderBy(x => x.CompanyName).ToList();
            //var fuelTypes = db.tbl_FuelType.OrderBy(x => x.FuelType).ToList();
            //ViewBag.Comapny = company;
            //ViewBag.FuelTypes = fuelTypes;

            //return View(pump);
        }
        #endregion
    }
}