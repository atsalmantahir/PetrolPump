using PetrolPump.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetrolPump.Controllers
{
    public class PumpsController : Controller
    {
        PetrolPumpDbEntities db;
        public PumpsController()
        {
            db = new PetrolPumpDbEntities();
        }
        // GET: Pumps
        public ActionResult Index()
        {
            var data = db.tbl_Pump.OrderByDescending(x => x.PumpID).ToList();
            return View(data);
        }
    }
}