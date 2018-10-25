using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Papizz.Controllers
{
    public class KontaktController : Controller
    {
        //Skapa koppling till databas
        private KontaktDatabaseEntities db = new KontaktDatabaseEntities();
        // GET: Kontakt
        public ActionResult Index()
        {
            //Gör lista av kontaktdatabas
            List<Kontakt> kontaktLista = db.Kontakts.ToList();
            //Skicka med listan till view
            return View(kontaktLista);
        }
    }
}