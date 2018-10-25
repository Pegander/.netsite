using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Papizz.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        //Visa index-sida
        public ActionResult Index()
        {
            return View();
        }
    }
}