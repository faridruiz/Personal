using Practica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Practica.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private PracticaContext db = new PracticaContext();

        // GET: Home
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult Home()
        {
            DateTime now = DateTime.Now.Date;
            ViewBag.PedidosAvencer = db.Pedidos.Where(x => x.FechaPago >= now && x.FechaPago.Month == now.Month).ToList();
            ViewBag.PedidosVencidos = db.Pedidos.Where(x => x.FechaPago < now).ToList();
            return View();
        }
    }
}