using Modelo;
using Persistencia.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.DAL
{
    class ClienteDAL
    {
        private EFContext context = new EFContext();
        public IQueryable<Cliente> ObterClassificadosPorNome()
        {
            return context.Clientes.OrderBy(d => d.Nome);
        }

        public Cliente ObterClientePorId(long id)
        {
            return context.Clientes.Where(e => e.UsuarioId == id).First();
        }

        public void GravarCliente(Cliente cliente)
        {
            if (cliente.UsuarioId == null)
            {
                context.Clientes.Add(cliente);
            }
            else
            {
                context.Entry(cliente).State = EntityState.Modified;
            }
            context.SaveChanges();
        }

        public Cliente EliminarClientePorId(long id)
        {
            Cliente cliente = ObterClientePorId(id);
            context.Clientes.Remove(cliente);
            context.SaveChanges();
            return cliente;
        }
    }
}
