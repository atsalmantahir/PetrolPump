using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetrolPump.Helpers
{
    public class Common
    {
        public static string GenerateTitle(int id, string title)
        {
            return "#" + id + " " + title;
        }
    }
}