using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetrolPump.Models;

namespace PetrolPump.Models
{
    public class AppSession
    {
        public tbl_User User { get; set; }
        public tbl_Role Role { get; set; }
        public List<tbl_Page> Page { get; set; }
        public List<tbl_Permission> Permissions { get; set; }
    }
}