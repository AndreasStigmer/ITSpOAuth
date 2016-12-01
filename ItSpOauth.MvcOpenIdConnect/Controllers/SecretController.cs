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

        //Kräver att aktuell Identity har en "role" claim med värdet "admin"
        [Authorize(Roles ="admin")]
        public ActionResult Index()
        {
            
            return View();
        }
    }
}