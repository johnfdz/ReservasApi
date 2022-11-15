using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reservas.API.Entidades
{
    public class TipoRequest
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public double? Costo { get; set; }
        public string Estado { get; set; }
    }
}
