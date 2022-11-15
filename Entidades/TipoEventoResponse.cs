using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Reservas.API.Entidades
{
    public class TipoEventoResponse
    {
        [JsonProperty("id_tipo_evento")]
        [Column("id_tipo_evento")]
        public long Id { get; set; }

        [JsonProperty("nombre_evento")]
        [Column("nombre_evento")]
        public string Nombre { get; set; }

        [JsonProperty("costo")]
        [Column("costo")]
        public double Costo { get; set; }

        [JsonProperty("estado")]
        [Column("estado")]
        public string Estado { get; set; }

        [JsonProperty("fecha_reg")]
        [Column("fecha_reg")]
        public DateTime? Fecha_Reg { get; set; }

        [JsonProperty("fecha_mod")]
        [Column("fecha_mod")]
        public DateTime? Fecha_Mod { get; set; }
    }
}
