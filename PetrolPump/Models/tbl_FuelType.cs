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
    
    public partial class tbl_FuelType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_FuelType()
        {
            this.tbl_PumpFuelType = new HashSet<tbl_PumpFuelType>();
            this.tbl_Sensor = new HashSet<tbl_Sensor>();
        }
    
        public int FuelTypeID { get; set; }
        public string FuelType { get; set; }
        public Nullable<decimal> PurchasePrice { get; set; }
        public Nullable<decimal> SalePrice { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_PumpFuelType> tbl_PumpFuelType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Sensor> tbl_Sensor { get; set; }
    }
}
