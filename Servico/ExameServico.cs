using Modelo;
using Modelo.Models;
using Persistencia;
using Persistencia.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servico
{
    class ExameServico
    {
        private ExameDAL exameDAL = new ExameDAL();
        public IQueryable<Exame> ObterExamesClassificadosPorDesc()
        {
            return exameDAL.ObterExamesClassificadosPorDesc();
        }
        public IQueryable<Exame> TodosExamesBD()
        {
            return exameDAL.TodosExamesBD();
        }
        public Exame ObterExamePorId(long id)
        {
            return exameDAL.ObterExamePorId(id);
        }
        public void GravarExame(Exame exame)
        {
            exameDAL.GravarExame(exame);
        }
        public Exame EliminarExamePorId(long id)
        {
            Exame exame = exameDAL.ObterExamePorId(id);
            exameDAL.EliminarExamePorId(id);
            return exame;
        }
    }
}
