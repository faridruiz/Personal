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
            ViewBag.ConceptoPagoID = new SelectList(db.ConceptosPago.Where(x => !x.Eliminado), "Identificador", "Concepto");
            ViewBag.PedidoID = new SelectList(db.Pedidos.Where(x => !x.Eliminado), "Identificador", "Concepto");
            return View();
        }

        // POST: Pagos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Identificador,PedidoID,TotalPago,Descripcion, TipoPago, ConceptoPagoID")] Pagos pagos)
        {
            if (ModelState.IsValid)
            {
                if (pagos.PedidoID > 0)
                {
                    Pedido pedido = db.Pedidos.Find(pagos.PedidoID);
                    if (pedido != null)
                    {
                        double _PagoRestante = PagoRestante(db.Pagos.Where(x => x.PedidoID == pagos.PedidoID).ToList(), pedido.TotalAPagar);
                        if (_PagoRestante == 0)
                        {
                            ModelState.AddModelError("PedidoID", "Este pedido ya ha sido saldado");
                        }
                        else
                        {
                            if (pagos.TotalPago > _PagoRestante)
                            {
                                ModelState.AddModelError("TotalPago", "Se ingreso una cantidad de pago mayor de la deuda (Deuda  $" + _PagoRestante + ")");
                            }
                            else
                            {
                                if (pagos.TipoPago == TipoPago.Abono)
                                {
                                    double _DeudaRestante = _PagoRestante;
                                    if (pagos.TotalPago > _DeudaRestante)
                                    {
                                        ModelState.AddModelError("TotalPago", "El abono es mayor a la deuda restante. (Restan $" + _DeudaRestante + ")");
                                    }
                                    else
                                    {
                                        if (pagos.TotalPago == _PagoRestante)
                                            pagos.TipoPago = TipoPago.Pago;
                                        db.Pagos.Add(pagos);
                                        db.SaveChanges();
                                        return RedirectToAction("Index");
                                    }
                                }
                                else
                                {
                                    if (pagos.TotalPago == _PagoRestante)
                                    {
                                        pagos.TipoPago = TipoPago.Pago;
                                        db.Pagos.Add(pagos);
                                        db.SaveChanges();
                                        return RedirectToAction("Index");
                                    }
                                    else
                                    {
                                        ModelState.AddModelError("TotalPago", "La cantidad ingresada es menor a total a pagar.");
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("PedidoID", "Pedido no existe, ingrese un pedido correcto");
                    }
                }
                else
                {
                    ModelState.AddModelError("PedidoID", "Pedido incorrecto, ingrese un pedido válido");
                }
            }
            ViewBag.PedidoID = new SelectList(db.Pedidos.Where(x => !x.Eliminado), "Identificador", "Concepto", pagos.PedidoID);
            ViewBag.ConceptoPagoID = new SelectList(db.ConceptosPago.Where(x => !x.Eliminado), "Identificador", "Concepto", pagos.ConceptoPagoID);
            return View(pagos);
        }

        private double PagoRestante(IList<Pagos> Pagos, double TotalAPagar)
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
            ViewBag.ConceptoPagoID = new SelectList(db.ConceptosPago.Where(x => !x.Eliminado), "Identificador", "Concepto", pagos.ConceptoPagoID);
            return View(pagos);
        }

        // POST: Pagos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Identificador,PedidoID,TotalPago,Descripcion, ConceptoPagoID")] Pagos pagos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pagos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PedidoID = new SelectList(db.Pedidos.Where(x => !x.Eliminado), "Identificador", "Concepto", pagos.PedidoID);
            ViewBag.ConceptoPagoID = new SelectList(db.ConceptosPago.Where(x => !x.Eliminado), "Identificador", "Concepto", pagos.ConceptoPagoID);
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
