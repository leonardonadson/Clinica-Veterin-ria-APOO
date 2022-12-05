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
    class VeterinarioDAL
    {
        private EFContext context = new EFContext();
        public IQueryable<Veterinario> ObterVeterinariosPorNome()
        {
            return context.Veterinarios.OrderBy(d => d.Nome);
        }

        public Veterinario ObterVeterinarioPorId(long id)
        {
            return context.Veterinarios.Where(e => e.UsuarioId == id).First();
        }

        public void GravarVeterinario(Veterinario veterinario)
        {
            if (veterinario.UsuarioId == null)
            {
                context.Veterinarios.Add(veterinario);
            }
            else
            {
                context.Entry(veterinario).State = EntityState.Modified;
            }
            context.SaveChanges();
        }

        public Veterinario EliminarVeterinarioPorId(long id)
        {
            Veterinario veterinario = ObterVeterinarioPorId(id);
            context.Veterinarios.Remove(veterinario);
            context.SaveChanges();
            return veterinario;
        }
    }
}
