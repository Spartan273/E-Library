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
    public class MembresController : Controller
    {
        private dbBiblioEntities db = new dbBiblioEntities();

        // GET: /Membres/
        public ActionResult Index()
        {
            var membres = db.Membres.Include(m => m.Adresses);
            return View(membres.ToList());
        }

        // GET: /Membres/Details/5
        public ActionResult Details(int? id)
        {

            /*var empruntsList = (from l in db.Livres join e in db.Emprunts on l.Id_Livre equals e.Id_Livre select  new {  l, e} );
            ViewBag.Emprunts = empruntsList.ToList();
            ViewBag.Livres = new SelectList(db.Livres, "titre"); */
            /* ViewBag.Emprunts = db.Emprunts.Where(m => m.Id_membre.Equals(id)); */


            /*Retourne la liste des livres empruntés ainsi que la date d'emprunt: ComplexView se trouve dans le model BiblioModel.Context.cs  */
            var emprunts = (from l in db.Livres join e in db.Emprunts on l.Id_Livre equals e.Id_Livre select new dbBiblioEntities.ComplexView { titre = l.titre, date_emprunt = e.date_emprunt });
            ViewBag.Emprunts = emprunts.ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Membres membres = db.Membres.Find(id);
            
            /*Emprunts emprunts = db.Emprunts.Include(m => m.Livres); */

            if (membres == null)
            {
                return HttpNotFound();
            }
            return View(membres);
        }

        // GET: /Membres/Create
        public ActionResult Create()
        {
            ViewBag.Id_adresse = new SelectList(db.Adresses, "Id_adresse", "adresseComplete");
            return View();
        }

        // POST: /Membres/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id_membre,nom,prenom,date_naissance,sexe,tel,courriel,code,Id_adresse")] Membres membres, [Bind(Include ="Id_adresse,num_civique,rue,app,ville,code_postal,province")] Adresses adresses)
        {
            if (ModelState.IsValid)
            {
               
                db.Membres.Add(membres);
                db.Adresses.Add(adresses);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_adresse = new SelectList(db.Adresses, "Id_adresse", "adresseComplete", membres.Id_adresse);
            return View(membres);
        }

        // GET: /Membres/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Membres membres = db.Membres.Find(id);
            if (membres == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_adresse = new SelectList(db.Adresses, "Id_adresse", "adresseComplete", membres.Id_adresse);
            return View(membres);
        }

        // POST: /Membres/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id_membre,nom,prenom,date_naissance,sexe,tel,courriel,code,Id_adresse")] Membres membres, [Bind(Include = "Id_adresse,num_civique,rue,app,ville,code_postal,province")] Adresses adresses)
        {
            if (ModelState.IsValid)
            {
                db.Entry(membres).State = EntityState.Modified;
                db.Entry(adresses).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_adresse = new SelectList(db.Adresses, "Id_adresse", "adresseComplete", membres.Id_adresse);
            return View(membres);
        }

        // GET: /Membres/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Membres membres = db.Membres.Find(id);
            if (membres == null)
            {
                return HttpNotFound();
            }
            return View(membres);
        }

        // POST: /Membres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Membres membres = db.Membres.Find(id);
            db.Membres.Remove(membres);
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
