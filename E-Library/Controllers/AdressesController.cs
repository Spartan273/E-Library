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
    public class AdressesController : Controller
    {
        private dbBiblioEntities db = new dbBiblioEntities();

        // GET: /Adresses/
        public ActionResult Index()
        {
            return View(db.Adresses.ToList());
        }

        // GET: /Adresses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adresses adresses = db.Adresses.Find(id);
            if (adresses == null)
            {
                return HttpNotFound();
            }
            return View(adresses);
        }

        // GET: /Adresses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Adresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id_adresse,num_civique,rue,app,ville,code_postal,province")] Adresses adresses)
        {
            if (ModelState.IsValid)
            {
                db.Adresses.Add(adresses);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(adresses);
        }

        // GET: /Adresses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adresses adresses = db.Adresses.Find(id);
            if (adresses == null)
            {
                return HttpNotFound();
            }
            return View(adresses);
        }

        // POST: /Adresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id_adresse,num_civique,rue,app,ville,code_postal,province")] Adresses adresses)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adresses).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(adresses);
        }

        // GET: /Adresses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adresses adresses = db.Adresses.Find(id);
            if (adresses == null)
            {
                return HttpNotFound();
            }
            return View(adresses);
        }

        // POST: /Adresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Adresses adresses = db.Adresses.Find(id);
            db.Adresses.Remove(adresses);
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
