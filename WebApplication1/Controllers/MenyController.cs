using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Papizz.Controllers
{
    public class MenyController : Controller
    {
        //Skapa koppling till databas
        private MenyDatabaseEntities db = new MenyDatabaseEntities();
        // GET: Meny
        public ActionResult Index()
        {
            //Gör lista av kontaktdatabas
            List<Meny> menyLista = db.Menies.ToList();

            //Skicka med listan till view
            return View(menyLista);
        }
    }
}