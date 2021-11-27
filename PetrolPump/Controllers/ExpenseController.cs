using PetrolPump.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetrolPump.Controllers
{
    public class ExpenseController : BaseController
    {
        PetrolPumpDbEntities db;
        AppSession session = (AppSession)System.Web.HttpContext.Current.Session["AppSession"];
        public ExpenseController()
        {
            db = new PetrolPumpDbEntities();
        }
        // GET: Expense
        public ActionResult Index()
        {
            var data = db.tbl_Expense.OrderByDescending(x => x.ExpenseID).ToList();
            return View(data);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(tbl_Expense obj)
        {
            if (ModelState.IsValid)
            {
                tbl_Expense data = new tbl_Expense();
                data.PumpID = Convert.ToInt32(session.User.PumpID);
                data.Title = obj.Title;
                data.Amount = obj.Amount;
                data.CreatedDate = DateTime.Now;

                db.tbl_Expense.Add(data);
                db.SaveChanges();

                return RedirectToAction("Index");
            }


            return View(obj);
        }

    }
}