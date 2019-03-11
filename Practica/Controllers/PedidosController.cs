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
    public class PedidosController : Controller
    {
        private PracticaContext db = new PracticaContext();

        // GET: Pedidos
        public ActionResult Index()
        {
            var pedidos = db.Pedidos.Where(x => !x.Eliminado).Include(p => p.Cliente);
            return View(pedidos.ToList());
        }

        // GET: Pedidos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // GET: Pedidos/Create
        public ActionResult Create()
        {
            ViewBag.ClienteID = new SelectList(db.Clientes.Where(x=>!x.Eliminado), "Identificador", "Nombre_cliente");
            ViewBag.ConceptoPagoID = new SelectList(db.ConceptosPago.Where(x => !x.Eliminado), "Identificador", "Concepto");
            return View();
        }

        // POST: Pedidos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "Identificador,Concepto,ClienteID,TipoBien, TotalAPagar, FechaPago")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                if (pedido.ClienteID > 0)
                {
                    Clientes cliente = db.Clientes.Find(pedido.ClienteID);
                    if (cliente != null)
                    {
                        db.Pedidos.Add(pedido);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("ClienteID", "Cliente no existe, ingrese un cliente correcto");
                }
                ModelState.AddModelError("ClienteID", "Cliente incorrecto, ingrese un cliente válido");
            }
            ViewBag.ClienteID = new SelectList(db.Clientes.Where(x => !x.Eliminado), "Identificador", "Nombre_cliente", pedido.ClienteID);
            return View(pedido);
        }

        // GET: Pedidos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteID = new SelectList(db.Clientes.Where(x => !x.Eliminado), "Identificador", "Nombre_cliente", pedido.ClienteID);
            return View(pedido);
        }

        // POST: Pedidos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Identificador,Concepto,ClienteID,TipoBien,TotalAPagar, FechaPago")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                if (pedido.ClienteID > 0)
                {
                    Clientes cliente = db.Clientes.Find(pedido.ClienteID);
                    if (cliente != null)
                    {
                        db.Entry(pedido).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("ClienteID", "Cliente no existe, ingrese un cliente correcto");
                }
                ModelState.AddModelError("ClienteID", "Cliente incorrecto, ingrese un cliente válido");                
            }
            ViewBag.ClienteID = new SelectList(db.Clientes.Where(x => !x.Eliminado), "Identificador", "Nombre_cliente", pedido.ClienteID);
            return View(pedido);
        }

        // GET: Pedidos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pedido pedido = db.Pedidos.Find(id);
            pedido.Eliminado = true;
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
