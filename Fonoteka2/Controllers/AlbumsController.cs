using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
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
            var czas = db.ZliczCzas(id).ToList();
            var album = db.Album.FirstOrDefault(p => p.IdAlbumu == id);
            ViewBag.Album = album.Nazwa;

            var data = db.Album.FirstOrDefault(p => p.IdAlbumu == id);
            ViewBag.Data = data.DataWydania;

            var tempzespol = db.Album.FirstOrDefault(p => p.IdAlbumu == id);
            var idzespolu = tempzespol.IdZespolu;
            var zespol = db.Zespol.FirstOrDefault(p => p.IdZespolu == idzespolu);
            ViewBag.Zespol = zespol.Nazwa;

            if (czas.Count() > 0)
            {
                ViewBag.Czas = czas[0].Godziny + " : " + czas[0].Minuty + " : " + czas[0].Sekundy;
            }
            
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
            return RedirectToAction("SciezkaDzwiekowaView", new { id = idAlbumu });

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

        public ActionResult testowe() {
            FonotekaDBEntities3 ent = new FonotekaDBEntities3();

            ObjectParameter returnId = new ObjectParameter("wyjscie", typeof(int));
            ObjectParameter returnId2 = new ObjectParameter("wyjscie2", typeof(int));
            var value = ent.testowe(returnId, returnId2).ToList();
            var valu2 = Convert.ToInt32(returnId2.Value);//calling our entity imported function "Bangalore" is our input parameter, returnId is a output parameter, it will receive the output value   
            ViewBag.Staty = Convert.ToInt32(returnId.Value); //set the out put value to StudentsCount ViewBag  
            return View();
        }

        public ActionResult Staty()
        {
            FonotekaDBEntities3 ent = new FonotekaDBEntities3();

            ObjectParameter returnId = new ObjectParameter("out", typeof(int));
            ObjectParameter returnId2 = new ObjectParameter("out2", typeof(int));
            ObjectParameter returnId3= new ObjectParameter("out3", typeof(int));
            ObjectParameter returnId4 = new ObjectParameter("out4", typeof(int));
            var value = ent.Staty(returnId, returnId2, returnId3, returnId4).ToList();
            var valu1 = Convert.ToInt32(returnId.Value);//calling our entity imported function "Bangalore" is our input parameter, returnId is a output parameter, it will receive the output value   
            var valu2 = Convert.ToInt32(returnId2.Value);//calling our entity imported function "Bangalore" is our input parameter, returnId is a output parameter, it will receive the output value   
            var valu3 = Convert.ToInt32(returnId3.Value);//calling our entity imported function "Bangalore" is our input parameter, returnId is a output parameter, it will receive the output value   

            var valu4 = Convert.ToInt32(returnId4.Value);//calling our entity imported function "Bangalore" is our input parameter, returnId is a output parameter, it will receive the output value   
            ViewBag.sum = Convert.ToInt32(returnId.Value); //set the out put value to StudentsCount ViewBag  
            ViewBag.average = Convert.ToInt32(returnId2.Value);
            ViewBag.max = Convert.ToInt32(returnId3.Value);
            ViewBag.min = Convert.ToInt32(returnId4.Value);
            return View();
        }


        public ActionResult CZM()
        {
            var czas = db.CzasZMiesiaca2().ToList();
            int czas2 = (int)czas[0];
            int godz = czas2 / 3600;
            int min = czas2 / 60;
            int sek = czas2 % 60;
            ViewBag.czas = czas;
            return View();
        }


        //public ActionResult Getstats() {
        //    FonotekaDBEntities3 ent = new FonotekaDBEntities3();

        //        ObjectParameter returnId = new ObjectParameter("sum", typeof(int));
        //        ObjectParameter returnId2 = new ObjectParameter("srednia", typeof(int));
        //        var value = ent.Stats(returnId,returnId2).ToList(); //calling our entity imported function "Bangalore" is our input parameter, returnId is a output parameter, it will receive the output value   
        //        ViewBag.Staty = Convert.ToInt32(returnId.Value); //set the out put value to StudentsCount ViewBag  
        //    return View();
        //}



    }
}
