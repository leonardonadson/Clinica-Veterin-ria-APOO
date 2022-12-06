using Modelo;
using Modelo.Models;
using Modelo.ViewModels;
using Persistencia.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servico
{
    public class ConsultaServico
    {
        private ConsultaDAL consultaDAL = new ConsultaDAL();

        public IQueryable<Consulta> ObterConsultasClassificadasPorData()
        {
            return consultaDAL.ObterConsultasClassificadasPorData();

        }
        public Consulta ConsultaOriginal(ConsultaViewModel consultaViewModel)
        {
            return consultaDAL.ConsultaOriginal(consultaViewModel);
        }
        public Consulta ObterConsultaPorId(long id)
        {
            return consultaDAL.ObterConsultaPorId(id);
        }
        public ICollection<ExameVinculado> PopularExames()
        {
            return consultaDAL.PopularExames();
        }

        public void AddOrUpdateExames(Consulta consulta, IEnumerable<ExameVinculado> examesvinculados)
        {
            consultaDAL.AddOrUpdateExames(consulta, examesvinculados);
        }
 
        public void GravarConsulta(Consulta consulta)
        {
            consultaDAL.GravarConsulta(consulta);
        }
    }
}
