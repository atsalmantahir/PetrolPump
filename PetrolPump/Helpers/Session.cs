using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetrolPump.Models;

namespace PetrolPump.Helpers
{
    public class Session
    {
        public static AppSession CreateSession(tbl_User user)
        {
            PetrolPumpDbEntities db = new PetrolPumpDbEntities();
            AppSession session = new AppSession();

            var role = db.tbl_Role.Where(x => x.RoleID == user.RoleID).FirstOrDefault();
            var permissions = db.tbl_Permission.Where(x => x.RoleID == role.RoleID).ToList();
            List<int> pageId = new List<int>();
            foreach (var item in permissions)
            {
                pageId.Add(item.PageID);
            }

            var pages = db.tbl_Page.Where(x => pageId.Contains(x.PageID) && x.IsActive == true).OrderBy(x => x.PageTitle).ToList();


            session.User = user;
            session.Role = role;
            session.Permissions = permissions;
            session.Page = pages;

            return session;
        }
    }
}