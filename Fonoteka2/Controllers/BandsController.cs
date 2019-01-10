using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Fonoteka2.Models;

namespace Fonoteka2.Controllers
{
    public class BandsController : Controller
    {
        private FonotekaDBEntities3 db = new FonotekaDBEntities3();

        // GET: Bands
        public ActionResult Index()
        {
            return View(db.Zespol.ToList());
        }

        // GET: Bands/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zespol zespol = db.Zespol.Find(id);
            if (zespol == null)
            {
                return HttpNotFound();
            }
            return View(zespol);
        }

        // GET: Bands/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdZespolu,Nazwa")] Zespol zespol)
        {
            if (ModelState.IsValid)
            {
                db.Zespol.Add(zespol);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(zespol);
        }

        // GET: Bands/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zespol zespol = db.Zespol.Find(id);
            if (zespol == null)
            {
                return HttpNotFound();
            }
            return View(zespol);
        }

        // POST: Bands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdZespolu,Nazwa")] Zespol zespol)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zespol).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(zespol);
        }

        // GET: Bands/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zespol zespol = db.Zespol.Find(id);
            if (zespol == null)
            {
                return HttpNotFound();
            }
            return View(zespol);
        }

        // POST: Bands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Zespol zespol = db.Zespol.Find(id);
            db.Zespol.Remove(zespol);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult UA()
        {
            return View(db.Zespol.ToList());
        }

        public ActionResult LastAlbums(int? id)
        {
            var currentDate = DateTime.Now.Year;
            var currentMonth = DateTime.Now.Month;
            var currentDay = DateTime.Now.Day;
            var lastalbums = from album in db.Album
                             where album.IdZespolu == id && (album.DataWydania.Year == currentDate || (album.DataWydania.Year == currentDate-1 && currentMonth < album.DataWydania.Month) || (album.DataWydania.Year == currentDate - 1 && currentMonth == album.DataWydania.Month && currentDay > album.DataWydania.Day))
                             select album;

            return View(lastalbums.ToList());
        }
    }
}
