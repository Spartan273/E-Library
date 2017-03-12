using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using E_Library.Models;

namespace E_Library.Controllers
{
    public class EmpruntsController : Controller
    {
        private dbBiblioEntities db = new dbBiblioEntities();

        // GET: /Emprunts/
        public ActionResult Index()
        {
            var emprunts = db.Emprunts.Include(e => e.Livres).Include(e => e.Membres);
            return View(emprunts.ToList());
        }

        // GET: /Emprunts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emprunts emprunts = db.Emprunts.Find(id);
            if (emprunts == null)
            {
                return HttpNotFound();
            }
            return View(emprunts);
        }

        // GET: /Emprunts/Create
        public ActionResult Create()
        {
            ViewBag.Id_Livre = new SelectList(db.Livres, "Id_Livre", "titre");
            ViewBag.Id_membre = new SelectList(db.Membres, "Id_membre", "IdentiteMembre");
            return View();
        }

        // POST: /Emprunts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id_emprunt,date_emprunt,statut,Id_membre,Id_Livre,date_retour")] Emprunts emprunts)
        {
            if (ModelState.IsValid)
            {
                db.Emprunts.Add(emprunts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Livre = new SelectList(db.Livres, "Id_Livre", "titre", emprunts.Id_Livre);
            ViewBag.Id_membre = new SelectList(db.Membres, "Id_membre", "IdentiteMembre", emprunts.Id_membre);
            return View(emprunts);
        }

        // GET: /Emprunts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emprunts emprunts = db.Emprunts.Find(id);
            if (emprunts == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Livre = new SelectList(db.Livres, "Id_Livre", "titre", emprunts.Id_Livre);
            ViewBag.Id_membre = new SelectList(db.Membres, "Id_membre", "nom", emprunts.Id_membre);
            return View(emprunts);
        }

        // POST: /Emprunts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id_emprunt,date_emprunt,statut,Id_membre,Id_Livre,date_retour")] Emprunts emprunts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emprunts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Livre = new SelectList(db.Livres, "Id_Livre", "titre", emprunts.Id_Livre);
            ViewBag.Id_membre = new SelectList(db.Membres, "Id_membre", "IdentiteMembre", emprunts.Id_membre);
            return View(emprunts);
        }

        // GET: /Emprunts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emprunts emprunts = db.Emprunts.Find(id);
            if (emprunts == null)
            {
                return HttpNotFound();
            }
            return View(emprunts);
        }

        // POST: /Emprunts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Emprunts emprunts = db.Emprunts.Find(id);
            db.Emprunts.Remove(emprunts);
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
