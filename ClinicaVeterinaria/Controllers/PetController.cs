﻿using Modelo;
using Modelo.Models;
using Persistencia.Contexts;
using Servico;
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
        private PetServico petServico = new PetServico();
        private EspecieServico especieServico = new EspecieServico();
        private ClienteServico clienteServico = new ClienteServico();
        private ActionResult ObterVisaoPetPorId(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                HttpStatusCode.BadRequest);
            }
            Pet pet = petServico.ObterPetPorId((long)id);
            if (pet == null)
            {
                return HttpNotFound();
            }
            return View(pet);
        }

        private ActionResult GravarPet(Pet pet)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    petServico.GravarPet(pet);
                    return RedirectToAction("Index");
                }
                return View(pet);
            }
            catch
            {
                return View(pet);
            }
        }
        public ActionResult Index()
        {
            return View(petServico.ObterPetsClassificadosPorNome());
        }

        public ActionResult Create()
        {
            ViewBag.ClienteId = new SelectList(clienteServico.ObterClientesClassificadosPorNome(),
                 "Id", "Nome");
            ViewBag.EspecieId = new SelectList(especieServico.ObterEspeciesClassificadasPorNome(),
            "Id", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pet pet)
        {
            ViewBag.ClienteId = new SelectList(clienteServico.ObterClientesClassificadosPorNome(),
                "Id", "Nome");
            ViewBag.EspecieId = new SelectList(especieServico.ObterEspeciesClassificadasPorNome(),
            "Id", "Nome");
            return GravarPet(pet);
        }

        public ActionResult Edit(long? id)
        {
            ViewBag.ClienteId = new SelectList(clienteServico.ObterClientesClassificadosPorNome(),
                "Id", "Nome");
            ViewBag.EspecieId = new SelectList(especieServico.ObterEspeciesClassificadasPorNome(),
            "Id", "Nome");
            return ObterVisaoPetPorId(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Pet pet)
        {
            return GravarPet(pet);
        }

        public ActionResult Delete(long? id)
        {
            return ObterVisaoPetPorId(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            try
            {
                Pet pet = petServico.EliminarPetPorId(id);
                TempData["Message"] = "Pet " + pet.PetId + " foi removido";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}