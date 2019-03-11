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
    public class FacturasController : Controller
    {
        private PracticaContext db = new PracticaContext();

        // GET: Facturas
        public ActionResult Index()
        {
            var facturas = db.Facturas.Where(x => !x.Eliminado).Include(f => f.Pedido).Include(m=>m.Pedido.Cliente);
            return View(facturas.ToList());
        }

        // GET: Facturas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Facturas.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        // GET: Facturas/Create
        public ActionResult Create()
        {
            ViewBag.PedidoID = new SelectList(db.Pedidos.Where(x => !x.Eliminado), "Identificador", "Concepto");
            return View();
        }

        // POST: Facturas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Identificador,PedidoID")] Factura factura)
        {
            factura.Fecha = DateTime.Now;
            if (ModelState.IsValid)
            {
                if(factura.PedidoID > 0)
                {
                    Pedido pedido = db.Pedidos.Find(factura.PedidoID);
                   
                    if(pedido != null)
                    {
                        if (PagoRestante(db.Pagos.Where(x => x.PedidoID == factura.PedidoID).ToList(), pedido.TotalAPagar) == 0)
                        {
                            db.Facturas.Add(factura);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("PedidoID", "El pedido presenta adeudo");
                        }
                    }
                    ModelState.AddModelError("PedidoID", "Pedido no existe, ingrese un pedido correcto");
                }
                ModelState.AddModelError("PedidoID", "Pedido incorrecto, ingrese un pedido válido");
            }
            ViewBag.PedidoID = new SelectList(db.Pedidos.Where(x => !x.Eliminado), "Identificador", "Concepto", factura.PedidoID);
            return View(factura);
        }

        public double PagoRestante(IList<Pagos> Pagos, double TotalAPagar)
        {
            double _TotalRestante = TotalAPagar;
            if (Pagos != null && Pagos.Count() > 0)
            {
                foreach (Pagos pago in Pagos)
                {
                    _TotalRestante -= pago.TotalPago;
                }
                return _TotalRestante;
            }
            return _TotalRestante;
        }
        // GET: Facturas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Facturas.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            ViewBag.PedidoID = new SelectList(db.Pedidos.Where(x => !x.Eliminado), "Identificador", "Concepto", factura.PedidoID);
            return View(factura);
        }

        // POST: Facturas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Identificador,PedidoID,Fecha")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                if (factura.PedidoID > 0)
                {
                    Pedido pedido = db.Pedidos.Find(factura.PedidoID);
                    if (pedido != null)
                    {
                        db.Entry(factura).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("PedidoID", "Pedido no existe, ingrese un pedido correcto");
                }
                ModelState.AddModelError("PedidoID", "Pedido incorrecto, ingrese un pedido válido");                
            }
            ViewBag.PedidoID = new SelectList(db.Pedidos.Where(x => !x.Eliminado), "Identificador", "Concepto", factura.PedidoID);
            return View(factura);
        }

        // GET: Facturas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Facturas.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        // POST: Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Factura factura = db.Facturas.Find(id);
            factura.Eliminado = true;
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
