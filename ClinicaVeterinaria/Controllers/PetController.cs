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
    public class PetController : Controller
    {
        private EFContext context = new EFContext();

        // GET: Pet
        public ActionResult Index()
        {
            return View(context.Pets.OrderBy(c => c.Nome));
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
        public ActionResult Create(Pet pet)
        {
            context.Pets.Add(pet);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Pets/Edit/5
        [HttpGet]
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pet pet = context.Pets.Find(id);
            if (pet == null)
            {
                return HttpNotFound();
            }
            return View(pet);
        }

        // POST: Pets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Pet pet)
        {
            if (ModelState.IsValid)
            {
                context.Entry(pet).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pet);
        }

        // GET: Pets/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pet pet = context.Pets.Where(f => f.PetId == id).First();
            if (pet == null)
            {
                return HttpNotFound();
            }
            return View(pet);
        }


        // GET: Pets/Delete/5
        [HttpGet]
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pet pet = context.Pets.Find(id);
            if (pet == null)
            {
                return HttpNotFound();
            }
            return View(pet);
        }

        // POST: Pets/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            Pet pet = context.Pets.Find(id);
            context.Pets.Remove(pet);
            context.SaveChanges();
            TempData["Message"] = "Pet " + pet.Nome.ToUpper() + " foi removido";
            return RedirectToAction("Index");
        }
    }
}