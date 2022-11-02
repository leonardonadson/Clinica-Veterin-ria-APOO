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
    public class ExameController : Controller
    {
        private EFContext context = new EFContext();

        // GET: Exame
        public ActionResult Index()
        {
            return View(context.Exames.OrderBy(c => c.Descricao));
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
        public ActionResult Create(Exame exame)
        {
            context.Exames.Add(exame);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Exames/Edit/5
        [HttpGet]
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exame exame = context.Exames.Find(id);
            if (exame == null)
            {
                return HttpNotFound();
            }
            return View(exame);
        }

        // POST: Exames/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Exame exame)
        {
            if (ModelState.IsValid)
            {
                context.Entry(exame).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(exame);
        }

        // GET: Exames/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exame exame = context.Exames.Where(f => f.ExameId == id).First();
            if (exame == null)
            {
                return HttpNotFound();
            }
            return View(exame);
        }


        // GET: Exames/Delete/5
        [HttpGet]
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exame exame = context.Exames.Find(id);
            if (exame == null)
            {
                return HttpNotFound();
            }
            return View(exame);
        }

        // POST: Exames/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            Exame exame = context.Exames.Find(id);
            context.Exames.Remove(exame);
            context.SaveChanges();
            TempData["Message"] = "Exame " + exame.Descricao.ToUpper() + " foi removido";
            return RedirectToAction("Index");
        }
    }
}