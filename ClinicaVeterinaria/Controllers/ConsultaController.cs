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
        private EFContext context = new EFContext();

        // GET: Consulta
        public ActionResult Index()
        {
            var consultas =
              context.Consultas.Include(c => c.Exame).OrderBy(n => n.ConsultaId);
            return View(consultas);
        }

        // GET: Consultas/Create
        public ActionResult Create()
        {
            ViewBag.ExameId = new SelectList(context.Exames.OrderBy(b => b.Descricao), "ExameId", "Descrição");
            return View();
        }

        // POST: Consultas/Create
        [HttpPost]
        public ActionResult Create(Consulta consulta)
        {
            try
            {
                context.Consultas.Add(consulta);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Consultas/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consulta consulta = context.Consultas.Include(c => c.Exame).Where(p => p.ConsultaId == id).First();
            if (consulta == null)
            {
                return HttpNotFound();
            }
            return View(consulta);
        }

        // GET: Consultas/Edit/5
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
            ViewBag.ExameId = new SelectList(context.Exames.OrderBy(b => b.Descricao), "ExameId",
            "Descrição", consulta.ExameId);
            return View(consulta);
        }

        // POST: Consultas/Edit/5
        [HttpPost]
        public ActionResult Edit(Consulta consulta)
        {
            try
            {
                context.Entry(consulta).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Consultas/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consulta consulta = context.Consultas.Where(p => p.ConsultaId == id).Include(c => c.ExameId).First();
            if (consulta == null)
            {
                return HttpNotFound();
            }
            return View(consulta);
        }

        // POST: Consultas/Delete/5
        [HttpPost]
        public ActionResult Delete(long id)
        {
            try
            {
                Consulta consulta = context.Consultas.Find(id);
                context.Consultas.Remove(consulta);
                context.SaveChanges();
                TempData["Message"] = "A consulta " + consulta.ConsultaId + " foi removido";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
