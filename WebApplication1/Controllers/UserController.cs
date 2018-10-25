using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Papizz.Controllers
{
    public class UserController : Controller
    {
        //Skapa koppling till databas
        UserDatabaseEntities db = new UserDatabaseEntities();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        //Httppost för att tala om att detta hanterar postad data
        [HttpPost]
        public ActionResult Index(Models.User inloggning)
        {
            //Skapa variabel för att hålla koll på om inloggad eller ej och sätt till false från början
            bool validUser = false;
            //Sätt validuser till true om username och password stämmer med databas
            validUser = CheckUser(inloggning.Username, inloggning.Password);
            if (validUser == true)
            {
                //om lyckad inloggning omdirigera
                System.Web.Security.FormsAuthentication.RedirectFromLoginPage(inloggning.Username, false);
            }
            //Om fel lösenord eller användare visa fel
            ModelState.AddModelError("", "Inloggning misslyckad");
            return View();
            }
        private bool CheckUser(string username, string password)
        {
            //Hänvisa till var användare och lösenord kontrolleras mot
            var anvandare = from rader in db.Users
                            //Gör om användare till uppercase för att ta bort case-sensetive på användarnamn men inte på password
                            where rader.Username.ToUpper() == username.ToUpper()
                            && rader.Password == password
                            select rader;
            if (anvandare.Count() == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
