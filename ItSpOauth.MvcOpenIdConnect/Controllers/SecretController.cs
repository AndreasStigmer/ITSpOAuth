using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace ItSpOauth.MvcOpenIdConnect.Controllers
{
    [Authorize]
    public class SecretController : Controller
    {
        // GET: Secret
        public ActionResult Index()
        {
            ClaimsIdentity ci = User.Identity as ClaimsIdentity;

            string name = ci.Name;

            return View();
        }
    }
}