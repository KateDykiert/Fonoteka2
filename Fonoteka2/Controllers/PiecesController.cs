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
    public static class globalVariables
    {
        public static int? a;
        public static byte[] b;
     
    }

    public class PiecesController : Controller
    {
       private FonotekaDBEntities3 db = new FonotekaDBEntities3();
        

        // GET: Pieces
        public ActionResult Index()
        {
            var utwor = db.Utwor.Include(u => u.Album).Include(u => u.Gatunek).Include(u => u.Zespol);
            return View(utwor.ToList());
        }

        

        // GET: Pieces/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utwor utwor = db.Utwor.Find(id);
            if (utwor == null)
            {
                return HttpNotFound();
            }
            return View(utwor);
        }

        // GET: Pieces/Create
        public ActionResult Create()
        {
            ViewBag.IdAlbumu = new SelectList(db.Album, "IdAlbumu", "Nazwa");
            ViewBag.IdGatunku = new SelectList(db.Gatunek, "IdGatunku", "Nazwa");
            ViewBag.IdZespolu = new SelectList(db.Zespol, "IdZespolu", "Nazwa");
            return View();
        }

        // POST: Pieces/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdUtworu,IdZespolu,IdAlbumu,IdGatunku,Tytul,Minuty,Sekundy")] Utwor utwor)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Exception = null;
                try
                {
                    db.updateAlbumTime(utwor.IdUtworu, utwor.IdZespolu, utwor.IdAlbumu, utwor.IdGatunku, utwor.Tytul, utwor.Minuty, utwor.Sekundy);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    if (e.InnerException == null)
                        ViewBag.Exception = "Niepoprawne dane utworu";
                    else
                        ViewBag.Exception = e.InnerException.InnerException.Message;
                    ViewBag.Exception2 = "Baza danych zwrocila wyjatek!";
                    ViewBag.IdZespolu = new SelectList(db.Zespol, "IdZespolu", "Nazwa");
                    ViewBag.IdAlbumu = new SelectList(db.Album, "IdAlbumu", "Nazwa");
                    ViewBag.IdGatunku = new SelectList(db.Gatunek, "IdGatunku", "Nazwa");
                    return View("Create");
                }
                return RedirectToAction("Index");
            }

            ViewBag.IdAlbumu = new SelectList(db.Album, "IdAlbumu", "Nazwa", utwor.IdAlbumu);
            ViewBag.IdGatunku = new SelectList(db.Gatunek, "IdGatunku", "Nazwa", utwor.IdGatunku);
            ViewBag.IdZespolu = new SelectList(db.Zespol, "IdZespolu", "Nazwa", utwor.IdZespolu);
            return View(utwor);
        }
        
        // GET: Pieces/Edit/5
        public ActionResult Edit(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utwor utwor = db.Utwor.Find(id);
            globalVariables.a = id;
            globalVariables.b = utwor.Currency;
            if (utwor == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdAlbumu = new SelectList(db.Album, "IdAlbumu", "Nazwa", utwor.IdAlbumu);
            ViewBag.IdGatunku = new SelectList(db.Gatunek, "IdGatunku", "Nazwa", utwor.IdGatunku);
            ViewBag.IdZespolu = new SelectList(db.Zespol, "IdZespolu", "Nazwa", utwor.IdZespolu);
            
            return View(utwor);
        }

        // POST: Pieces/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdUtworu,IdZespolu,IdAlbumu,IdGatunku,Tytul,Minuty,Sekundy")] Utwor utwor)
        {
            
            if (ModelState.IsValid)
            {
                db.Entry(utwor).State = EntityState.Modified;
                try
                {
                    db.BezpiecznikEdit(globalVariables.b, globalVariables.a);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    if (e.InnerException == null)
                        ViewBag.Exception = "Niepoprawne dane utworu";
                    else
                    {
                        globalVariables.b = utwor.Currency;
                        String msg = e.InnerException.Message;
                        ViewBag.Exception = msg;
                    }

                    // uaktualnienie modelu
                    ModelState.Clear(); 
                    db = new FonotekaDBEntities3();
                    Utwor utwor2 = db.Utwor.Find(utwor.IdUtworu);

                    ViewBag.Exception2 = "Baza danych zwrocila wyjatek!";
                    ViewBag.IdAlbumu = new SelectList(db.Album, "IdAlbumu", "Nazwa", utwor.IdAlbumu);
                    ViewBag.IdGatunku = new SelectList(db.Gatunek, "IdGatunku", "Nazwa", utwor.IdGatunku);
                    ViewBag.IdZespolu = new SelectList(db.Zespol, "IdZespolu", "Nazwa", utwor.IdZespolu);

                    return View(utwor2);
                }
            }
            ViewBag.IdAlbumu = new SelectList(db.Album, "IdAlbumu", "Nazwa", utwor.IdAlbumu);
            ViewBag.IdGatunku = new SelectList(db.Gatunek, "IdGatunku", "Nazwa", utwor.IdGatunku);
            ViewBag.IdZespolu = new SelectList(db.Zespol, "IdZespolu", "Nazwa", utwor.IdZespolu);
            return View(utwor);
        }

        // GET: Pieces/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utwor utwor = db.Utwor.Find(id);
            if (utwor == null)
            {
                return HttpNotFound();
            }
            return View(utwor);
        }

        // POST: Pieces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Utwor utwor = db.Utwor.Find(id);
            db.Utwor.Remove(utwor);
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
