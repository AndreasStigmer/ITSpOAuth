using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ItSpOauth.MvcOpenIdConnect.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return View();
        }
    }
}