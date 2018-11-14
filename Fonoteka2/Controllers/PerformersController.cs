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
    public class PerformersController : Controller
    {
        private FonotekaDBEntities3 db = new FonotekaDBEntities3();

        // GET: Performers
        public ActionResult Index()
        {
            var wykonawca = db.Wykonawca.Include(w => w.Zespol);
            return View(wykonawca.ToList());
        }

        public ActionResult Instruments(int? id)
        {
            return View(db.wyswietlinstrumentywykonawcy(id).ToList());
        }

        // GET: Performers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wykonawca wykonawca = db.Wykonawca.Find(id);
            if (wykonawca == null)
            {
                return HttpNotFound();
            }
            return View(wykonawca);
        }

        // GET: Performers/Create
        public ActionResult Create()
        {
            ViewBag.IdZespolu = new SelectList(db.Zespol, "IdZespolu", "Nazwa");
            return View();
        }

        // GET: Performers/Create2
        public ActionResult Create2()
        {
            ViewBag.IdZespolu = new SelectList(db.Zespol, "IdZespolu", "Nazwa");
            return View();
        }

        // POST: Performers/Create2
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create2(int IdWykonawcy, int IdZespolu, String Imie, String Nazwisko, String Pseudonim)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Exception = null;
                try
                {
                    db.DodajWykonawce(IdWykonawcy,IdZespolu,Imie,Nazwisko,Pseudonim);
                    db.SaveChanges();
                }
                catch(Exception e) {
                    if (e.InnerException == null)
                        ViewBag.Exception = "Niepoprawne dane wykonawcy";
                    else
                        ViewBag.Exception = e.InnerException.Message;
                    ViewBag.Exception2 = "Baza danych zwrocila wyjatek!";
                    ViewBag.IdZespolu = new SelectList(db.Zespol, "IdZespolu", "Nazwa");

                    return View("Create2");
                }
                return RedirectToAction("Index");
            }
            
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdWykonawcy,IdZespolu,Imie,Nazwisko,Pseudonim")] Wykonawca wykonawca)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Exception = null;
                try
                {
                    db.Wykonawca.Add(wykonawca);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    if (e.InnerException == null)
                        ViewBag.Exception = "Niepoprawne dane wykonawcy";
                    else
                    {
                        
                        ViewBag.Exception = e.InnerException.InnerException.Message;
                    }
                    ViewBag.Exception2 = "Baza danych zwrocila wyjatek!";
                    ViewBag.IdZespolu = new SelectList(db.Zespol, "IdZespolu", "Nazwa", wykonawca.IdZespolu);
                    return View(wykonawca);
                }
                return RedirectToAction("Index");
            }

            ViewBag.IdZespolu = new SelectList(db.Zespol, "IdZespolu", "Nazwa", wykonawca.IdZespolu);
            return View(wykonawca);
        }

        // GET: Performers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wykonawca wykonawca = db.Wykonawca.Find(id);
            if (wykonawca == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdZespolu = new SelectList(db.Zespol, "IdZespolu", "Nazwa", wykonawca.IdZespolu);
            return View(wykonawca);
        }

        // POST: Performers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdWykonawcy,IdZespolu,Imie,Nazwisko,Pseudonim")] Wykonawca wykonawca)
        {
            if (ModelState.IsValid)
            {
                
                    db.Entry(wykonawca).State = EntityState.Modified;
                try { 
                    db.SaveChanges();
                    return RedirectToAction("Index");
                } catch (Exception e)
                {
                    if (e.InnerException == null)
                        ViewBag.Exception = "Niepoprawne dane wykonawcy";
                    else
                    {
                       String msg = e.InnerException.InnerException.Message;
                       ViewBag.Exception = msg;
                    }
                    ViewBag.Exception2 = "Baza danych zwrocila wyjatek!";
                    ViewBag.IdZespolu = new SelectList(db.Zespol, "IdZespolu", "Nazwa", wykonawca.IdZespolu);
                    return View(wykonawca);
                }
            }
            ViewBag.IdZespolu = new SelectList(db.Zespol, "IdZespolu", "Nazwa", wykonawca.IdZespolu);
            return View(wykonawca);
        }

        // GET: Performers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wykonawca wykonawca = db.Wykonawca.Find(id);
            if (wykonawca == null)
            {
                return HttpNotFound();
            }
            return View(wykonawca);
        }

        // POST: Performers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Wykonawca wykonawca = db.Wykonawca.Find(id);
            db.Wykonawca.Remove(wykonawca);
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
