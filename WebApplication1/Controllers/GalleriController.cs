using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Papizz.Controllers
{
    public class GalleriController : Controller
    {
        ImagesDatabaseEntities imagedb = new ImagesDatabaseEntities();
        // GET: Galleri
        public ActionResult Index()
        {
            List<Image> imageList = imagedb.Images.ToList();
            return View(imageList);
        }
    }
}