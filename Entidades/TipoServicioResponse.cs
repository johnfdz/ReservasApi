using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Reservas.API.Entidades
{
    public class TipoServicioResponse
    {
        [JsonProperty("id_tipo_servicio")]
        [Column("id_tipo_servicio")]
        public long Id { get; set; }

        [JsonProperty("nombre_servicio")]
        [Column("nombre_servicio")]
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
