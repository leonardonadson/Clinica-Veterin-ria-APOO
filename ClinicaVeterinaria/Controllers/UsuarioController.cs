using Modelo;
using Persistencia.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ClinicaVeterinaria.Controllers
{
    public class UsuarioController : Controller
    {
        private EFContext context = new EFContext();

        // GET: Usuario
        public ActionResult Index()
        {
            return View(context.Usuarios.OrderBy(c => c.Nome));
        }

        // GET: Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuario usuario)
        {
            context.Usuarios.Add(usuario);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Usuarios/Edit/5
        [HttpGet]
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = context.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                context.Entry(usuario).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = context.Usuarios.Where(f => f.UsuarioId == id).First();
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }


        // GET: Usuarios/Delete/5
        [HttpGet]
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = context.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            Usuario usuario = context.Usuarios.Find(id);
            context.Usuarios.Remove(usuario);
            context.SaveChanges();
            TempData["Message"] = "Usuario " + usuario.Nome.ToUpper() + " foi removido";
            return RedirectToAction("Index");
        }
    }
}