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
    public class AdherencesController : Controller
    {
        private FonotekaDBEntities3 db = new FonotekaDBEntities3();

        // GET: Adherences
        public ActionResult Index()
        {
            var przynaleznosc = db.Przynaleznosc.Include(p => p.Playlista).Include(p => p.Utwor);
            return View(przynaleznosc.ToList());
        }

        // GET: Adherences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Przynaleznosc przynaleznosc = db.Przynaleznosc.Find(id);
            if (przynaleznosc == null)
            {
                return HttpNotFound();
            }
            return View(przynaleznosc);
        }

        // GET: Adherences/Create
        public ActionResult Create()
        {
            ViewBag.IdPlaylisty = new SelectList(db.Playlista, "IdPlaylisty", "Nazwa");
            ViewBag.IdUtworu = new SelectList(db.Utwor, "IdUtworu", "Tytul");
            return View();
        }

        // POST: Adherences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPrzynaleznosci,IdPlaylisty,IdUtworu")] Przynaleznosc przynaleznosc)
        {
            if (ModelState.IsValid)
            {
                db.Przynaleznosc.Add(przynaleznosc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdPlaylisty = new SelectList(db.Playlista, "IdPlaylisty", "Nazwa", przynaleznosc.IdPlaylisty);
            ViewBag.IdUtworu = new SelectList(db.Utwor, "IdUtworu", "Tytul", przynaleznosc.IdUtworu);
            return View(przynaleznosc);
        }

        // GET: Adherences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Przynaleznosc przynaleznosc = db.Przynaleznosc.Find(id);
            if (przynaleznosc == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdPlaylisty = new SelectList(db.Playlista, "IdPlaylisty", "Nazwa", przynaleznosc.IdPlaylisty);
            ViewBag.IdUtworu = new SelectList(db.Utwor, "IdUtworu", "Tytul", przynaleznosc.IdUtworu);
            return View(przynaleznosc);
        }

        // POST: Adherences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPrzynaleznosci,IdPlaylisty,IdUtworu")] Przynaleznosc przynaleznosc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(przynaleznosc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdPlaylisty = new SelectList(db.Playlista, "IdPlaylisty", "Nazwa", przynaleznosc.IdPlaylisty);
            ViewBag.IdUtworu = new SelectList(db.Utwor, "IdUtworu", "Tytul", przynaleznosc.IdUtworu);
            return View(przynaleznosc);
        }

        // GET: Adherences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Przynaleznosc przynaleznosc = db.Przynaleznosc.Find(id);
            if (przynaleznosc == null)
            {
                return HttpNotFound();
            }
            return View(przynaleznosc);
        }

        // POST: Adherences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Przynaleznosc przynaleznosc = db.Przynaleznosc.Find(id);
            db.Przynaleznosc.Remove(przynaleznosc);
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
    }
}
