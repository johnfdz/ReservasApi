using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Reservas.API.Entidades
{
    public class UsuarioResponse
    {
        [JsonProperty("id_usuario")]
        [Column("id_usuario")]
        public long Id { get; set; }

        [JsonProperty("nombre")]
        [Column("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("apellido")]
        [Column("apellido")]
        public string Apellido { get; set; }

        [JsonProperty("correo")]
        [Column("correo")]
        public string Correo { get; set; }

        [JsonProperty("telefono")]
        [Column("telefono")]
        public string Telefono { get; set; }

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
