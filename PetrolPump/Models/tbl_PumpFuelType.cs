//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PetrolPump.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_PumpFuelType
    {
        public int PumpFuelTypeID { get; set; }
        public int PumpID { get; set; }
        public Nullable<int> FuelTypeID { get; set; }
    
        public virtual tbl_FuelType tbl_FuelType { get; set; }
        public virtual tbl_Pump tbl_Pump { get; set; }
    }
}