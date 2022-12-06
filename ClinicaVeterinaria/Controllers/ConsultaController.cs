﻿using Modelo.Models;
using Modelo.ViewModels;
using Persistencia.Contexts;
using Servico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClinicaVeterinaria.Controllers
{
    public class ConsultaController : Controller
    {
        private ConsultaServico consultaServico = new ConsultaServico();
        private PetServico petServico = new PetServico();
        private VeterinarioServico veterinarioServico = new VeterinarioServico();
        private ExameServico exameServico = new ExameServico();
        private EFContext context = new EFContext();

        public void AddOrUpdateKeepExistingExames(Consulta consulta, IEnumerable<ExameVinculado> examesVinculados)
        {
            var webExameVinculadoId = examesVinculados.Where(c => c.Vinculado).Select(webExame => webExame.Id);
            var contextExameIDs = consulta.Exames.Select(contextExame => contextExame.Id);
            var exameIDs = contextExameIDs as int[] ?? contextExameIDs.ToArray();
            var examesToDeleteIDs = exameIDs.Where(id => !webExameVinculadoId.Contains(id)).ToList();

            // Apaga exames removidos
            foreach (var id in examesToDeleteIDs)
            {
                consulta.Exames.Remove(context.Exames.Find(id));
            }

            // Adiciona exames que não tenham sido usados
            foreach (var id in webExameVinculadoId)
            {
                if (!exameIDs.Contains(id))
                {
                    consulta.Exames.Add(context.Exames.Find(id));
                }
            }
        }

        // ACTIONS ABAIXO
        // GET: Consultas
        public ActionResult Index()
        {
            var consultas = consultaServico.ObterConsultasClassificadasPorData().ToList();
            var consultaViewModels = consultas.Select(consulta => consulta.ToViewModel()).ToList();
            return View(consultaViewModels);
        }

        //GET: Create
        public ActionResult Create()
        {
            ViewBag.PetId = new SelectList(petServico.ObterPetsClassificadosPorNome(),
            "PetId", "Nome");
            ViewBag.VeterinarioId = new SelectList(veterinarioServico.ObterVeterinariosClassificadosPorNome(),
            "Id", "Nome");
            var consultaViewModel = new ConsultaViewModel { Exames = consultaServico.PopularExames() };

            return View(consultaViewModel);
        }

        //POST: Create
        [HttpPost]
        public ActionResult Create(ConsultaViewModel consultaViewModel)
        {
            if (ModelState.IsValid)
            {
                var consulta = consultaViewModel.ToDomainModel();
                consultaServico.AddOrUpdateExames(consulta, consultaViewModel.Exames);
                consultaServico.GravarConsulta(consulta);

                return RedirectToAction("Index");
            }
            ViewBag.PetId = new SelectList(petServico.ObterPetsClassificadosPorNome(),
            "PetId", "Nome");
            ViewBag.VeterinarioId = new SelectList(veterinarioServico.ObterVeterinariosClassificadosPorNome(),
            "Id", "Nome");
            return View(consultaViewModel);
        }

        //GET: Edit
        public ActionResult Edit(int id = 0)
        {
            //recupera todos os exames
            //exameServico.TodosExamesBD().ToList();

            //adiciona ou atualiza exames mantendo original
            var consulta = context.Consultas.Include("Exames").FirstOrDefault(x => x.Id == id);
            var consultaViewModel = consulta.ToViewModel(exameServico.TodosExamesBD().ToList());
            ViewBag.PetId = new SelectList(context.Pets.OrderBy(b => b.Nome), "PetId", "Nome");
            ViewBag.VeterinarioId = new SelectList(context.Veterinarios.OrderBy(b => b.Nome),
            "Id", "Nome");

            return View(consultaViewModel);
        }

        //POST: Edit
        [HttpPost]
        public ActionResult Edit(ConsultaViewModel consultaViewModel)
        {
            if (ModelState.IsValid)
            {
                var ConsultaOriginal = context.Consultas.Find(consultaViewModel.Id);

                // adiciona ou atualiza mantendo original
                AddOrUpdateKeepExistingExames(ConsultaOriginal, consultaViewModel.Exames);

                context.Entry(ConsultaOriginal).CurrentValues.SetValues(consultaViewModel);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.PetId = new SelectList(context.Pets.OrderBy(b => b.Nome), "PetId", "Nome");
            ViewBag.VeterinarioId = new SelectList(context.Veterinarios.OrderBy(b => b.Nome),
            "VeterinarioId", "Nome");
            return View(consultaViewModel);
        }

        //GET: Details
        public ActionResult Details(int id = 0)
        {
            // retorna todos os exames
            var todosExamesBD = exameServico.TodosExamesBD().ToList();

            // retorna a consulta a ser editada e inclui os exames vinculados
            var consulta = context.Consultas.Include("Exames").FirstOrDefault(x => x.Id == id);
            var consultaViewModel = consulta.ToViewModel(todosExamesBD);

            return View(consultaViewModel);
        }
        //GET: Delete
        public ActionResult Delete(int id = 0)
        {
            var consultaIQueryable = from u in context.Consultas.Include("Exames")
                                     where u.Id == id
                                     select u;


            if (!consultaIQueryable.Any())
            {
                return HttpNotFound("Consulta não encontrada.");
            }

            var consulta = consultaIQueryable.First();
            var consultaViewModel = consulta.ToViewModel();
            ViewBag.PetId = new SelectList(context.Pets.OrderBy(b => b.Nome), "PetId", "Nome");
            ViewBag.VeterinarioId = new SelectList(context.Veterinarios.OrderBy(b => b.Nome),
            "Id", "Nome");
            return View(consultaViewModel);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var consulta = context.Consultas.Include("Exames").Single(u => u.Id == id);
            DeleteConsulta(consulta);

            return RedirectToAction("Index");
        }

        private void DeleteConsulta(Consulta consulta)
        {
            if (consulta.Exames != null)
            {
                foreach (var exame in consulta.Exames.ToList())
                {
                    consulta.Exames.Remove(exame);
                }
            }

            context.Consultas.Remove(consulta);
            context.SaveChanges();
        }
    }
}