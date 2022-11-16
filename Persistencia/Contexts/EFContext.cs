﻿using Modelo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Contexts
{
    public class EFContext : DbContext
    {
        public EFContext() : base("Asp_Net_MVC_CS")
        {
            Database.SetInitializer<EFContext>(
            new DropCreateDatabaseIfModelChanges<EFContext>());
        }
        public DbSet<Exame> Exames { get; set; }
        public DbSet<Consulta> Consultas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Especie> Especies { get; set; }
    }
}
