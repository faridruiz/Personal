using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Practica.Models;
using EntityState = System.Data.Entity.EntityState;

namespace Practica.Controllers
{
    public class ConceptosPagoController : Controller
    {
        private PracticaContext db = new PracticaContext();

        // GET: ConceptosPago
        public ActionResult Index()
        {
            return View(db.ConceptosPago.ToList());
        }

        // GET: ConceptosPago/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConceptosPago ConceptosPago = db.ConceptosPago.Find(id);
            if (ConceptosPago == null)
            {
                return HttpNotFound();
            }
            return View(ConceptosPago);
        }

        // GET: ConceptosPago/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ConceptosPago/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Identificador,Concepto,Descripcion,Tipo")] ConceptosPago ConceptosPago)
        {
            if (ModelState.IsValid)
            {
                db.ConceptosPago.Add(ConceptosPago);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ConceptosPago);
        }

        // GET: ConceptosPago/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConceptosPago ConceptosPago = db.ConceptosPago.Find(id);
            if (ConceptosPago == null)
            {
                return HttpNotFound();
            }
            return View(ConceptosPago);
        }

        // POST: ConceptosPago/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Identificador,Concepto,Descripcion,Tipo")] ConceptosPago ConceptosPago)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ConceptosPago).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ConceptosPago);
        }

        // GET: ConceptosPago/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConceptosPago ConceptosPago = db.ConceptosPago.Find(id);
            if (ConceptosPago == null)
            {
                return HttpNotFound();
            }
            return View(ConceptosPago);
        }

        // POST: ConceptosPago/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ConceptosPago ConceptosPago = db.ConceptosPago.Find(id);
            ConceptosPago.Eliminado = true;
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
