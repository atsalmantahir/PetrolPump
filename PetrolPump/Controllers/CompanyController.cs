using PetrolPump.Helpers;
using PetrolPump.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetrolPump.Controllers
{
    public class CompanyController : BaseController
    {
        PetrolPumpDbEntities db;
        AppSession session = (AppSession)System.Web.HttpContext.Current.Session["AppSession"];
        public CompanyController()
        {
            db = new PetrolPumpDbEntities();
        }
        // GET: Company
        public ActionResult Index()
        {
            var data = db.tbl_Company.OrderByDescending(x => x.CompanyID).ToList();
            return View(data);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(tbl_Company obj)
        {

            tbl_Company data = new tbl_Company();
            data.CompanyName = obj.CompanyName;
            data.CompanyLogo = obj.CompanyLogo;


            db.tbl_Company.Add(data);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}