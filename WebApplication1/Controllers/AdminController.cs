using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Papizz.Models;

namespace Papizz.Controllers
{
    //Lägg till authorize för att göra hela admin-controllern säker med inlogg
    [Authorize]
    public class AdminController : Controller
    {
        //Skapa kopplingar till databaser
        private KontaktDatabaseEntities kontaktdb = new KontaktDatabaseEntities();
        private MenyDatabaseEntities db = new MenyDatabaseEntities();
        private ImagesDatabaseEntities imagedb = new ImagesDatabaseEntities();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditMeny()
        {
            string currentUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            //Gör lista av menydatabas för att kunna visa i view
            List<Meny> menyLista = db.Menies.ToList();
            //Visa view med listan medskickad
            return View(menyLista);
        }
        
        public ActionResult Kontakt()
        {
            //Gör lista av kontaktdatabas och skicka med denna till view
            List<Kontakt> kontaktLista = kontaktdb.Kontakts.ToList();
            return View(kontaktLista);
        }

        public ActionResult EditKontakt(int? id)
        {
            return View();
        }
        //Httppost för att tala om att detta hanterar postad data
        [HttpPost]
        //Skicka med andradKontakt från view
        public ActionResult EditKontakt(Kontakt andradKontakt)
        {
            kontaktdb.Entry(andradKontakt).State = System.Data.Entity.EntityState.Modified;
            //Spara ändringar i databas. Fungerade med async varje gång utan fungerade det inte som det skulle
            kontaktdb.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }
        //Httppost för att tala om att detta hanterar postad data
        [HttpPost]
        public ActionResult Create(Meny nyRatt)
        {
            //Lägg till ny rätt i databas
            db.Menies.Add(nyRatt);
            //Spara ändringar till databas
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int? id)
        {
            //Om id inte är satt återvänd till index
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            //Hitta rätten med hjälp av id
            Meny rattAttRadera = db.Menies.Find(id);
            //Visa bekräftelsesida
            return View(rattAttRadera);
        }

        //Httppost för att tala om att detta hanterar postad data
        [HttpPost]
        public ActionResult Delete(int id)
        {
            //Hitta rätten med hjälp av id
            Meny rattAttRadera = db.Menies.Find(id);
            //Ta bort rätt ur databas
            db.Menies.Remove(rattAttRadera);
            //Spara ändringar till databas
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            //Om id är null skicka tillbaka till index
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            //Hitta rätt med hjälp av id
            Meny rattAttAndra = db.Menies.Find(id);
            //Visa sida för att ändra rätt
            return View(rattAttAndra);
        }

        //Httppost för att tala om att detta hanterar postad data
        [HttpPost]
        public ActionResult Edit(Meny andradRatt)
        {
            db.Entry(andradRatt).State = System.Data.Entity.EntityState.Modified;
            //Spara ändringar till databas
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ManageImages()
        {
            return View();
        }

        //Lägg till bilder
        public ActionResult AddImage()
        {
            List<Image> imageLista = imagedb.Images.ToList();
            return View(imageLista);
        }
        //Httppost för att tala om att detta hanterar postad data
        [HttpPost]
        public ActionResult AddImage(string postedfile)
        {
            //Spara postade filer i file
            HttpPostedFileBase file = Request.Files[0];
            //skapa string som hänvisar till vart filen ska sparas
            string myPath = Server.MapPath("/Uploaded/");
            //Spara fil i myPath
            file.SaveAs(myPath +
                file.FileName);
            //Passa bildens egenskaper till de i databasen
            Image image = new Image()
            {
                Filnamn = file.FileName.Split('\\').Last(),
            };
            //Lägg till filinfo i databas
            imagedb.Images.Add(image);
            imagedb.SaveChanges();
            return View();
        }
        //Skapa en ny ny för att visa bilderna som kan tas bort, liknande den i galleriet men med möjlighet att ta bort bilder
        public ActionResult viewImagestoDelete()
        {
            List<Image> bildLista = imagedb.Images.ToList();
            return View(bildLista);
        }
        
        //Om användare kommer hit genom att skriva url utan id hamnar användaren tillbaka på manageimages
        public ActionResult ImageDelete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("ManageImages");
            }
            //Leta upp bilden med rätt id och skicka detta vidare
            Image imageAttRadera = imagedb.Images.Find(id);
            return View(imageAttRadera);
        }

        [HttpPost]
        public ActionResult ImageDelete(int id)
        {
            Image imageAttRadera = imagedb.Images.Find(id);

            //Nedan försökte jag göra så att även den faktiska filen raderas men lyckades inte

            //string filnamn = imagedb.Images.Find(id).Filnamn;
            //string filmapp = "/Uploaded/";
            //string filAttRadera = System.IO.Path.Combine(filmapp, filnamn);
            //Console.Write(filAttRadera);

            //if (filAttRadera != null)
            //{
            //if (System.IO.File.Exists(filAttRadera))
            //{
            //System.IO.Directory.CreateDirectory(targetPath);
            //System.IO.File.Delete(filAttRadera);
            //}
            //}
            
            //Ta bort bil med remove och spara databasförändringar
            imagedb.Images.Remove(imageAttRadera);
            imagedb.SaveChanges();
            return RedirectToAction("viewImagestoDelete");
        }

    }
}