using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PetrolPump.Models
{
    [MetadataType(typeof(UserMetadata))]
    public partial class tbl_User
    { }

    public class UserMetadata
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 7)]
        [RegularExpression(@"^[0-9-]*$", ErrorMessage = "Not a valid phone number")]
        public string Phone { get; set; }

        public Nullable<System.DateTime> CreatedAt { get; set; }

        [Required]
        public string Password { get; set; }
    }


    [MetadataType(typeof(PumpMetadata))]
    public partial class tbl_Pump
    {
        public int FuelTypeID { get; set; }
        public List<int> List_FuelTypeID { get; set; }
    }

    public class PumpMetadata
    {
        [Required]
        public string Location { get; set; }
        
    }

    [MetadataType(typeof(ExpenseMetaData))]
    public partial class tbl_Expense
    { }

    public class ExpenseMetaData
    {
        [Required]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Currency,ErrorMessage = "Invalid Input")]
            
        public decimal Amount { get; set; }
        
    }

    [MetadataType(typeof(EmployeeMetaData))]
    public partial class tbl_Employee
    { }

    public class EmployeeMetaData
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.Currency, ErrorMessage = "Invalid Input")]

        public decimal Salary { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 7)]
        [RegularExpression(@"^[0-9-]*$", ErrorMessage = "Not a valid phone number")]
        public string Phone { get; set; }

    }
}