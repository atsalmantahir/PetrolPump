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
    
    public partial class tbl_EmployeeSalary
    {
        public int SalaryID { get; set; }
        public int EmployeeID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> IsPaid { get; set; }
    
        public virtual tbl_Employee tbl_Employee { get; set; }
    }
}
