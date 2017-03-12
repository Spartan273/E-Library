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
    public class LivresController : Controller
    {
        private dbBiblioEntities db = new dbBiblioEntities();

        // GET: /Livres/
        // Prend en paramètre sortOrder et searchString dans l'url, envoyés par la vue (actionLink new{ sortOrder =...) et le textbox searchString}
        public ActionResult Index(string sortOrder, string searchString)
        {
            //Si le Viewbag Title est null ou vide (sortOrder) il lui attribue title_desc (desc = descending), sinon il attribue une chaine vide
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            //Viewbag Author = paramètre sortOrder. Si sortOrder == Author, viewbag.Author = author_desc, sinon, il prend Author (lorsqu'on clique une deuxième fois)
            ViewBag.AuthorSortParm = sortOrder == "Author" ? "author_desc" : "Author";
            var livres = from model in db.Livres
                         select model;


            //barre de recherche : si paramètre searchString envoyé du textbox n'est PAS null/vide
            if(!String.IsNullOrEmpty(searchString))
            {
                //Select les livres avec l'attribut titre OU auteur égal à searchString)
                livres = livres.Where(model => model.titre.Contains(searchString) || model.auteur.Contains(searchString));
            }

            //selon sortOrder, classe la liste select en Décroissant
            switch (sortOrder)
            {
                case "title_desc":
                    livres = livres.OrderByDescending(model => model.titre);
                    break;
                case "Author":
                    livres = livres.OrderByDescending(model => model.auteur);
                    break;
                default:
                    livres = livres.OrderBy(model => model.titre);
                    break;
            }

            return View(livres.ToList());
            /*return View(db.Livres.ToList());*/
        }

        // GET: /Livres/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livres livres = db.Livres.Find(id);
            if (livres == null)
            {
                return HttpNotFound();
            }
            return View(livres);
        }

        // GET: /Livres/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Livres/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id_Livre,titre,auteur,editeur,exemplaires,categorie")] Livres livres)
        {
            if (ModelState.IsValid)
            {
                db.Livres.Add(livres);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(livres);
        }

        // GET: /Livres/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livres livres = db.Livres.Find(id);
            if (livres == null)
            {
                return HttpNotFound();
            }
            return View(livres);
        }

        // POST: /Livres/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id_Livre,titre,auteur,editeur,exemplaires,categorie")] Livres livres)
        {
            if (ModelState.IsValid)
            {
                db.Entry(livres).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(livres);
        }

        // GET: /Livres/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livres livres = db.Livres.Find(id);
            if (livres == null)
            {
                return HttpNotFound();
            }
            return View(livres);
        }

        // POST: /Livres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Livres livres = db.Livres.Find(id);
            db.Livres.Remove(livres);
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
