using Modelo;
using Persistencia.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia
{
    class UsuarioDAL
    {
        private EFContext context = new EFContext();
        public IQueryable<Usuario> ObterUsuariosClassificadosPorNome()
        {
            return context.Usuarios.OrderBy(d => d.Nome);
        }

        public Usuario ObterUsuarioPorId(long id)
        {
            return context.Usuarios.Where(e => e.UsuarioId == id).First();
        }

        public void GravarUsuario(Usuario usuario)
        {
            if (usuario.UsuarioId == null)
            {
                context.Usuarios.Add(usuario);
            }
            else
            {
                context.Entry(usuario).State = EntityState.Modified;
            }
            context.SaveChanges();
        }

        public Usuario EliminarUsuarioPorId(long id)
        {
            Usuario usuario = ObterUsuarioPorId(id);
            context.Usuarios.Remove(usuario);
            context.SaveChanges();
            return usuario;
        }
    }
}
