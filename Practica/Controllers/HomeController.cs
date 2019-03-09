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

        // GET: Home
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult Home()
        {            
            return View();
        }
    }
}