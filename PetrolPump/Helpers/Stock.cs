using PetrolPump.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PetrolPump.Helpers
{
    public class Stock
    {
        static PetrolPumpDbEntities db = new PetrolPumpDbEntities();


        public static bool updateStocks(int? SensorID, string OrderType, int? Quantity)
        {
            var data = db.tbl_Sensor.Where(x => x.SensorID == SensorID).FirstOrDefault();
            if (OrderType == "Sale")
            {
                data.FuelPresents -= Quantity;
            }
            if (OrderType == "Purchase")
            {
                data.FuelPresents += Quantity;
            }
            if (data.TankCapacity >= data.FuelPresents) 
            {
                db.tbl_Sensor.Attach(data);
                db.Entry(data).State = EntityState.Modified;

                db.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}