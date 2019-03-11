using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Practica.Models;
using EntityState = System.Data.Entity.EntityState;

namespace Practica.Controllers
{
    public class UsuariosController : Controller
    {
        private PracticaContext db = new PracticaContext();

        // GET: Usuarios
        public ActionResult Index()
        {
            return View(db.Usuarios.Where(x => !x.Eliminado).ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Identificador,Nombre_usuario,Contraseña")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Identificador,Nombre_usuario,Contraseña")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult InicioSesion()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InicioSesion([Bind(Include = "Nombre_usuario,Contraseña")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                List<Usuario> _usuario = db.Usuarios.Where(x => x.Nombre_usuario == usuario.Nombre_usuario && x.Contraseña == usuario.Contraseña).ToList();
                if(_usuario.Count == 1)
                {
                    IdentitySignin(new AppUserState { UserId = _usuario[0].Identificador.ToString(), Name = _usuario[0].Nombre_usuario, Email = "@", IsAdmin = false, Theme = "empty" });
                    //HttpCookie cookie = FormsAuthentication.GetAuthCookie(usuario.Nombre_usuario, false);

                    //Response.Cookies.Add(cookie);
                    return RedirectToAction("Home", "Home");
                }
                ModelState.AddModelError("USUARIO_INC", "No existe un usuario con esa combinación de usuario y/o contraseña.");
            }
            return View();
        }


        [HttpGet]
        public void CerrarSesion()
        {
            IdentitySignout();           
            Response.Redirect("~/");
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        public void IdentitySignin(AppUserState appUserState, string providerKey = null, bool isPersistent = false)
        {
            var claims = new List<Claim>();

            // create required claims
            claims.Add(new Claim(ClaimTypes.NameIdentifier, appUserState.UserId));
            claims.Add(new Claim(ClaimTypes.Name, appUserState.Name));

            // custom – my serialized AppUserState object
            claims.Add(new Claim("userState", appUserState.ToString()));

            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            AuthenticationManager.SignIn(new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = isPersistent,
                ExpiresUtc = DateTime.UtcNow.AddDays(7)
            }, identity);
        }

        public void IdentitySignout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie,
                                            DefaultAuthenticationTypes.ExternalCookie);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }
    }
}
