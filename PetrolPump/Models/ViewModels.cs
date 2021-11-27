using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetrolPump.Models
{
    public class ViewModels
    {
        public class KPI
        {
            public string Pumps { get; set; }
            public string ActiveSensor { get; set; }
            public string TotalSales { get; set; }
            public string TotalPurchases { get; set; }
        }

        public class FuelTypes
        {
            public List<string> Title { get; set; }
            public List<string> TotalAmount { get; set; }
        }

        public class CylinderCharts
        {
            public string category { get; set; }
            public decimal? value1 { get; set; }
            public decimal? value2 { get; set; }
        }

        public class ApiAcces
        {
            public string UserId { get; set; }
            public string Password { get; set; }
            public string SensonMac { get; set; }
            public int FuelPresent { get; set; }
        }


        public class FuelType
        {
            public string Title { get; set; }
            public decimal? TotalAmount { get; set; }
            public int? TankCapacity { get; set; }
        }

        public class SalesReport
        {
            public DateTime? CreatedDate { get; set; }
            public string OrderType { get; set; }
            public string FuelType { get; set; }
            public string Title { get; set; }
            public decimal? SalePrice { get; set; }
            public int? Quantity { get; set; }
        }

        public class PurchaseReport
        {
            public DateTime? CreatedDate { get; set; }
            public string OrderType { get; set; }
            public string FuelType { get; set; }
            public string Title { get; set; }
            public decimal? PurchasePrice { get; set; }
            public int? Quantity { get; set; }
        }
    }
}