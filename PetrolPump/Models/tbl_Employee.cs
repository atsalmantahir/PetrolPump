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
    
    public partial class tbl_Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Employee()
        {
            this.tbl_EmployeeSalary = new HashSet<tbl_EmployeeSalary>();
        }
    
        public int EmployeeID { get; set; }
        public Nullable<int> PumpID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Designation { get; set; }
        public Nullable<decimal> Salary { get; set; }
    
        public virtual tbl_Pump tbl_Pump { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_EmployeeSalary> tbl_EmployeeSalary { get; set; }
    }
}
