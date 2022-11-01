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
    public class ConsultaController : Controller
    {
        // GET: Consulta
        public ActionResult Index()
        {
            var consultas = context.Consultas.Include(e => e.Exame);
            return View(consultas.ToList());
        }
        private EFContext context = new EFContext();

        // GET: Create
        public ActionResult Create()
        {
            ViewBag.ExameId = new SelectList(context.Exames, "ExameId", "Descrição");
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Consulta consulta)
        {
            context.Consultas.Add(consulta);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Fabricantes/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consulta consulta = context.Consultas.Find(id);
            if (consulta == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExameId = new SelectList(context.Exames, "ExameId", "Descrição", consulta.ExameId);
            return View(consulta);
        }

        // POST: Fabricantes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Consulta consulta)
        {
            if (ModelState.IsValid)
            {
                context.Entry(consulta).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ExameId = new SelectList(context.Exames, "ExameId", "Descrição", consulta.ExameId);
            return View(consulta);
        }

        // GET: Fabricantes/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consulta consulta = context.Consultas.Find(id);
            if (consulta == null)
            {
                return HttpNotFound();
            }
            return View(consulta);
        }

        // POST: Fabricantes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            Consulta consulta = context.Consultas.Find(id);
            context.Consultas.Remove(consulta);
            context.SaveChanges();
            TempData["Message"] = "Categoria " + consulta.sintomas.ToUpper() + " foi removida";
            return RedirectToAction("Index");
        }
    }
}
