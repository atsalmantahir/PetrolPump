using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetrolPump.Models;
using static PetrolPump.Models.ViewModels;
using System.Data.Entity;

namespace PetrolPump.Controllers
{
    public class UpdateApiController : Controller
    {
        PetrolPumpDbEntities db;
        public UpdateApiController()
        {
            db = new PetrolPumpDbEntities();
        }
        // GET: UpdateApi
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Sensor(ApiAcces obj)
        {
            if (obj.UserId == "user123" && obj.Password == "J9JzC:LeX(H[}BSe@rO|=H=[ogr3y@:<mam?=~Ne)6*kGsPSC=mi03oIQ:.y'@z")
            {
                var data = db.tbl_Sensor.Where(x => x.SensorMAC == obj.SensonMac).FirstOrDefault();
                if (data != null)
                {
                    if (obj.FuelPresent > data.TankCapacity)
                    {
                        return Json("Tank Capacity exceed, Contact Administrator", JsonRequestBehavior.AllowGet);
                    }
                    else if (obj.FuelPresent <= 0)
                    {
                        return Json("Incorrect Data", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        data.FuelPresents = obj.FuelPresent;
                        db.tbl_Sensor.Attach(data);
                        db.Entry(data).State = EntityState.Modified;

                        db.SaveChanges();
                        return Json("Tank/Sensor Record Updated", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json("No Sensor Found for given request " + obj.SensonMac, JsonRequestBehavior.AllowGet);
                }
                
            }
            else
            {
                return Json("Invalid Request", JsonRequestBehavior.AllowGet);
            }
            
        }
    }
}