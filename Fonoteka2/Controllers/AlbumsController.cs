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
    public class AlbumsController : Controller
    {
        private FonotekaDBEntities3 db = new FonotekaDBEntities3();

        // GET: Albums
        public ActionResult Index()
        {
            var album = db.Album.Include(a => a.Zespol);
            return View(album.ToList());
        }

        public ActionResult SciezkaDzwiekowa(int? id)
        {
            return View(db.UtworyAlbumu1(id).ToList());
        }

        public ActionResult SciezkaDzwiekowaView(int? id)
        {
            return View(db.UtworyZView1(id).ToList());
        }

        public ActionResult EditZView(int? id)
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
            ViewBag.IdZespolu = new SelectList(db.Zespol, "IdZespolu", "Nazwa", utwor.IdZespolu);
            ViewBag.IdGatunku = new SelectList(db.Gatunek, "IdGatunku", "Nazwa", utwor.IdGatunku);
            ViewBag.IdAlbumu = new SelectList(db.Album, "IdAlbumu", "Nazwa", utwor.IdAlbumu);
            return View(utwor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditZView([Bind(Include = "IdUtworu,IdZespolu,IdAlbumu,IdGatunku,Tytul,Minuty,Sekundy")] Utwor utwor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(utwor).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("SciezkaDzwiekowaView" , new { id = utwor.IdAlbumu});
                }
                catch (Exception e)
                {
                    if (e.InnerException == null)
                        ViewBag.Exception = "Niepoprawne dane utworu";
                    else
                    {
                        String msg = e.InnerException.InnerException.Message;
                        ViewBag.Exception = msg;
                    }
                    ViewBag.Exception2 = "Baza danych zwrocila wyjatek!";
                    ViewBag.IdAlbumu = new SelectList(db.Album, "IdAlbumu", "Nazwa", utwor.IdAlbumu);
                    ViewBag.IdGatunku = new SelectList(db.Gatunek, "IdGatunku", "Nazwa", utwor.IdGatunku);
                    ViewBag.IdZespolu = new SelectList(db.Zespol, "IdZespolu", "Nazwa", utwor.IdZespolu);
                    return View(utwor);
                }
            }
            ViewBag.IdZespolu = new SelectList(db.Zespol, "IdZespolu", "Nazwa", utwor.IdZespolu);
            ViewBag.IdGatunku = new SelectList(db.Gatunek, "IdGatunku", "Nazwa", utwor.IdGatunku);
            ViewBag.IdAlbumu = new SelectList(db.Album, "IdAlbumu", "Nazwa", utwor.IdAlbumu);
            return View(utwor);
        }

            public ActionResult DeleteZView(int? id, int? idAlbumu)
        {
            db.DeleteUtworyZView(id);
            return View(db.UtworyZView1(idAlbumu).ToList());
            
        }

        // GET: Albums/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Album.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // GET: Albums/Create
        public ActionResult Create()
        {
            ViewBag.IdZespolu = new SelectList(db.Zespol, "IdZespolu", "Nazwa");
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAlbumu,IdZespolu,Nazwa,DataWydania,Godziny,Minuty,Sekundy")] Album album)
        {
            if (ModelState.IsValid)
            {
                db.Album.Add(album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdZespolu = new SelectList(db.Zespol, "IdZespolu", "Nazwa", album.IdZespolu);
            return View(album);
        }

        // GET: Albums/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Album.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdZespolu = new SelectList(db.Zespol, "IdZespolu", "Nazwa", album.IdZespolu);
            return View(album);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAlbumu,IdZespolu,Nazwa,DataWydania")] Album album)
        {
            if (ModelState.IsValid)
            {
                db.Entry(album).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdZespolu = new SelectList(db.Zespol, "IdZespolu", "Nazwa", album.IdZespolu);
            return View(album);
        }

        // GET: Albums/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Album.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = db.Album.Find(id);
            db.Album.Remove(album);
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
