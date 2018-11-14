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
    public class SkillsController : Controller
    {
        private FonotekaDBEntities3 db = new FonotekaDBEntities3();

        // GET: Skills
        public ActionResult Index()
        {
            var umiejetnosc = db.Umiejetnosc.Include(u => u.Instrument).Include(u => u.Wykonawca);
            return View(umiejetnosc.ToList());
        }

        // GET: Skills/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Umiejetnosc umiejetnosc = db.Umiejetnosc.Find(id);
            if (umiejetnosc == null)
            {
                return HttpNotFound();
            }
            return View(umiejetnosc);
        }

        // GET: Skills/Create
        public ActionResult Create()
        {
            ViewBag.IdInstrumentu = new SelectList(db.Instrument, "IdInstrumentu", "Nazwa");
            ViewBag.IdWykonawcy = new SelectList(db.Wykonawca, "IdWykonawcy", "Pseudonim");
            return View();
        }

        // POST: Skills/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdUmiejetnosci,IdWykonawcy,IdInstrumentu")] Umiejetnosc umiejetnosc)
        {
            if (ModelState.IsValid)
            {
                db.Umiejetnosc.Add(umiejetnosc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdInstrumentu = new SelectList(db.Instrument, "IdInstrumentu", "Nazwa", umiejetnosc.IdInstrumentu);
            ViewBag.IdWykonawcy = new SelectList(db.Wykonawca, "IdWykonawcy", "Imie", umiejetnosc.IdWykonawcy);
            return View(umiejetnosc);
        }

        // GET: Skills/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Umiejetnosc umiejetnosc = db.Umiejetnosc.Find(id);
            if (umiejetnosc == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdInstrumentu = new SelectList(db.Instrument, "IdInstrumentu", "Nazwa", umiejetnosc.IdInstrumentu);
            ViewBag.IdWykonawcy = new SelectList(db.Wykonawca, "IdWykonawcy", "Imie", umiejetnosc.IdWykonawcy);
            return View(umiejetnosc);
        }

        // POST: Skills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdUmiejetnosci,IdWykonawcy,IdInstrumentu")] Umiejetnosc umiejetnosc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(umiejetnosc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdInstrumentu = new SelectList(db.Instrument, "IdInstrumentu", "Nazwa", umiejetnosc.IdInstrumentu);
            ViewBag.IdWykonawcy = new SelectList(db.Wykonawca, "IdWykonawcy", "Imie", umiejetnosc.IdWykonawcy);
            return View(umiejetnosc);
        }

        // GET: Skills/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Umiejetnosc umiejetnosc = db.Umiejetnosc.Find(id);
            if (umiejetnosc == null)
            {
                return HttpNotFound();
            }
            return View(umiejetnosc);
        }

        // POST: Skills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Umiejetnosc umiejetnosc = db.Umiejetnosc.Find(id);
            db.Umiejetnosc.Remove(umiejetnosc);
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
