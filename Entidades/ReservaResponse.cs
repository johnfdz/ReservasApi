using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Reservas.API.Entidades
{
    public class ReservaResponse
    {
        [JsonProperty("id_reserva")]
        [Column("id_reserva")]
        public long Id_Reserva { get; set; }

        [JsonProperty("nombre_usuario")]
        [Column("nombre_usuario")]
        public string Nombre_Usuario { get; set; }

        [JsonProperty("nombre_evento")]
        [Column("nombre_evento")]
        public string Nombre_Evento { get; set; }

        [JsonProperty("nombre_servicio")]
        [Column("nombre_servicio")]
        public string Nombre_Servicio { get; set; }

        [JsonProperty("cantidad_camarografos")]
        [Column("cantidad_camarografos")]
        public int Cantidad_Camarografos { get; set; }

        [JsonProperty("duracion_evento")]
        [Column("duracion_evento")]
        public int Duracion_Evento_H { get; set; }

        [JsonProperty("costo_total")]
        [Column("costo_total")]
        public double Costo_Total { get; set; }

        [JsonProperty("fecha_hora_evento")]
        [Column("fecha_hora_evento")]
        public DateTime Fecha_Hora_Evento { get; set; }

        [JsonProperty("direccion_evento")]
        [Column("direccion_evento")]
        public string Direccion_Evento { get; set; }

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
