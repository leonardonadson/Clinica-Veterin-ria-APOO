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
    public class EspecieController : Controller
    {
        private EFContext context = new EFContext();

        // GET: Especie
        public ActionResult Index()
        {
            return View(context.Especies.OrderBy(c => c.Nome));
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
        public ActionResult Create(Especie especie)
        {
            context.Especies.Add(especie);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Especies/Edit/5
        [HttpGet]
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Especie especie = context.Especies.Find(id);
            if (especie == null)
            {
                return HttpNotFound();
            }
            return View(especie);
        }

        // POST: Especies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Especie especie)
        {
            if (ModelState.IsValid)
            {
                context.Entry(especie).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(especie);
        }

        // GET: Especies/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Especie especie = context.Especies.Where(f => f.EspecieId == id).First();
            if (especie == null)
            {
                return HttpNotFound();
            }
            return View(especie);
        }


        // GET: Especies/Delete/5
        [HttpGet]
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Especie especie = context.Especies.Find(id);
            if (especie == null)
            {
                return HttpNotFound();
            }
            return View(especie);
        }

        // POST: Especies/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            Especie especie = context.Especies.Find(id);
            context.Especies.Remove(especie);
            context.SaveChanges();
            TempData["Message"] = "Especie " + especie.Nome.ToUpper() + " foi removido";
            return RedirectToAction("Index");
        }
    }
}