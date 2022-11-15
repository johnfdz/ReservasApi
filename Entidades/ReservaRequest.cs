using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reservas.API.Entidades
{
    public class ReservaRequest
    {
        public long Id_Reserva { get; set; }
        public long? Id_Usuario { get; set; }
        public long? Id_Tipo_Evento { get; set; }
        public long? Id_Tipo_Servicio { get; set; }
        public int? Cantidad_Camarografos { get; set; }
        public int? Duracion_Evento_H { get; set; }
        public DateTime? Fecha_Hora_Evento { get; set; }
        public string Direccion_Evento { get; set; }
        public string Estado { get; set; }

    }
}
