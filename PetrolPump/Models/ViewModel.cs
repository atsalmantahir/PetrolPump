using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PetrolPump.Models
{
    public static class ViewModel
    {
        public class User : tbl_User
        {
            public int UserID { get; set; }
            [Required]
            public int RoleID { get; set; }

            
        }
    }
}