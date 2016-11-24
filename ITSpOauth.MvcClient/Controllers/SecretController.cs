using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ITSpOauth.MvcClient.Controllers
{
    public class SecretController : Controller
    {
        // GET: Secret
        public async Task<ActionResult> Index()
        {
            HttpClient hc=Client.HttpCLientHelper.GetClient();
            var data = await hc.GetStringAsync("/api/profile");
            ViewBag.data = data;
            return View();
        }
    }
}