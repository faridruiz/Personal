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
    public class PagosController : Controller
    {
        private PracticaContext db = new PracticaContext();

        // GET: Pagos
        public ActionResult Index()
        {
            var pagos = db.Pagos.Where(x => !x.Eliminado).Include(p => p.Pedido);
            return View(pagos.ToList());
        }

        // GET: Pagos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pagos pagos = db.Pagos.Find(id);
            if (pagos == null)
            {
                return HttpNotFound();
            }
            return View(pagos);
        }

        // GET: Pagos/Create
        public ActionResult Create()
        {
            ViewBag.PedidoID = new SelectList(db.Pedidos.Where(x => !x.Eliminado), "Identificador", "Concepto");
            return View();
        }

        // POST: Pagos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Identificador,PedidoID,TotalPagado,Descripcion")] Pagos pagos)
        {
            if (ModelState.IsValid)
            {
                db.Pagos.Add(pagos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PedidoID = new SelectList(db.Pedidos.Where(x => !x.Eliminado), "Identificador", "Concepto", pagos.PedidoID);
            return View(pagos);
        }

        // GET: Pagos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pagos pagos = db.Pagos.Find(id);
            if (pagos == null)
            {
                return HttpNotFound();
            }
            ViewBag.PedidoID = new SelectList(db.Pedidos.Where(x => !x.Eliminado), "Identificador", "Concepto", pagos.PedidoID);
            return View(pagos);
        }

        // POST: Pagos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Identificador,PedidoID,TotalPagado,Descripcion")] Pagos pagos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pagos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PedidoID = new SelectList(db.Pedidos.Where(x => !x.Eliminado), "Identificador", "Concepto", pagos.PedidoID);
            return View(pagos);
        }

        // GET: Pagos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pagos pagos = db.Pagos.Find(id);
            if (pagos == null)
            {
                return HttpNotFound();
            }
            return View(pagos);
        }

        // POST: Pagos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pagos pagos = db.Pagos.Find(id);
            pagos.Eliminado = true;
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
