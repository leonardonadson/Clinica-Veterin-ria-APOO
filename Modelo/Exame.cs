using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Exame
    {
        public long ExameId { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<Exame> Exames { get; set; }
    }
}
