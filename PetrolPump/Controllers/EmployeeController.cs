using PetrolPump.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetrolPump.Controllers
{
    public class EmployeeController : BaseController
    {
        PetrolPumpDbEntities db;
        AppSession session = (AppSession)System.Web.HttpContext.Current.Session["AppSession"];
        public EmployeeController()
        {
            db = new PetrolPumpDbEntities();
        }
        // GET: Empolyee
        public ActionResult Index()
        {
            var data = db.tbl_Employee.OrderByDescending(x => x.EmployeeID).ToList();
            return View(data);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(tbl_Employee obj)
        {
            if (ModelState.IsValid)
            {
                tbl_Employee data = new tbl_Employee();
                data.PumpID = Convert.ToInt32(session.User.PumpID);
                data.FullName = obj.FullName;
                data.Phone = obj.Phone;
                data.Salary = obj.Salary;
                data.Designation = obj.Designation;

                db.tbl_Employee.Add(data);
                db.SaveChanges();

                return RedirectToAction("Index");
            }


            return View(obj);
        }


        [HttpGet]
        public ActionResult SalaryHistory(int ID)
        {
            var data = db.tbl_EmployeeSalary.Where(x => x.EmployeeID == ID).ToList();
            return View(data);
        }

        [HttpGet]
        public ActionResult TransferSalary(int ID)
        {
            ViewBag.ID = ID;
            return View();
        }

        [HttpPost]
        public ActionResult TransferSalary(tbl_EmployeeSalary obj)
        {
            if (ModelState.IsValid)
            {
                tbl_EmployeeSalary data = db.tbl_EmployeeSalary.Where(x => x.EmployeeID == obj.EmployeeID).FirstOrDefault();
                if (data != null 
                    && Convert.ToDateTime(data.CreatedDate).Month == Convert.ToDateTime(obj.CreatedDate).Month
                    && Convert.ToDateTime(data.CreatedDate).Year == Convert.ToDateTime(obj.CreatedDate).Year)
                {


                    data.EmployeeID = obj.EmployeeID;
                    data.CreatedDate = obj.CreatedDate;
                    data.IsPaid = 1;

                    db.tbl_EmployeeSalary.Attach(data);
                    db.Entry(data).State = EntityState.Modified;

                    db.SaveChanges();

                    return RedirectToAction("Index");

                }
                else
                {
                    tbl_EmployeeSalary salary = new tbl_EmployeeSalary();
                    salary.EmployeeID = obj.EmployeeID;
                    salary.CreatedDate = obj.CreatedDate;
                    salary.IsPaid = 1;

                    db.tbl_EmployeeSalary.Add(salary);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                
            }


            return View(obj);
        }
    }
}