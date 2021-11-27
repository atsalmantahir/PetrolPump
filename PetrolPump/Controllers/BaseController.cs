using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetrolPump.Models;
using System;
using System.IO;

using System.Web.UI;

using System.Web.UI.WebControls;
namespace PetrolPump.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string redirectController = string.Empty;
            string redirectAction = string.Empty;
            string ErrorMessage = string.Empty;
            bool IsvalidToken = true;
            bool authorizedRequest = true;
            var urlHelper = new UrlHelper(filterContext.RequestContext);
            if (System.Web.HttpContext.Current.Session["AppSession"] != null)
            {
                AppSession sess = (AppSession)System.Web.HttpContext.Current.Session["AppSession"];
                var rd = System.Web.HttpContext.Current.Request.RequestContext.RouteData;
                string currentController = rd.GetRequiredString("controller");
                string currentAction = rd.GetRequiredString("action");
                if (sess != null)
                {

                    string Page_URL = string.Empty;
                    Page_URL = currentController;

                    //var right = sess.Permissions.Where(x => x.Menu.ToLower() == Page_URL.ToLower()).FirstOrDefault();

                    //if (right == null)
                    //{
                    //    redirectController = "Error";
                    //    redirectAction = "Permission";
                    //    ErrorMessage = "UnAuthorized";
                    //    authorizedRequest = false;

                    //}
                    //else
                    //{
                    //    redirectController = currentController;
                    //    redirectAction = currentAction;
                    //    ErrorMessage = "Authorized";
                    //    authorizedRequest = true;
                    //}

                    if (!authorizedRequest)
                    {
                        filterContext.Result = new RedirectResult("~/" + redirectController + "/" + redirectAction);
                    }


                }
            }
            else
            {

                filterContext.Result = new RedirectResult("~/Login");

            }


            base.OnActionExecuting(filterContext);

            //DirectoryInfo di = new DirectoryInfo("~/Test");
            //string path = Server.MapPath("~/Test");
            //foreach (string filename in Directory.GetFiles(path))
            //{
            //    File.Delete(filename);
            //}


            //foreach (string filename in Directory.GetFiles(path))
            //{

            //    File.Delete(filename);

            //}

        }

    }
}