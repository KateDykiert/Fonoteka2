using Fonoteka2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fonoteka2.Controllers
{
    public class HomeController : Controller
    {
        private FonotekaDBEntities3 db = new FonotekaDBEntities3();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Summary()
        {
            ViewBag.Message = "Summary";
            return View();


        }
        public ActionResult UA()
        {
            var wykonawcy = db.Zespol;
            ViewBag.Message = "Message";
            return View(wykonawcy.ToList());


        }
        public ActionResult Widok()
        {

            ViewBag.Message = "widok";
            return View();


        }

        

    }
}