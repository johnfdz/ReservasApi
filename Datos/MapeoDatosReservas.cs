using Utils;
using GDifare.Utilitario.BaseDatos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using Reservas.API.Entidades;

namespace Reservas.API.Datos
{
    public interface IMapeoDatosReservas : IMapeoDatos
    {
        #region Metodos de la interfaz
        List<UsuarioResponse> ObtenerUsuarios();
        List<ReservaResponse> ObtenerReservas();
        List<TipoEventoResponse> ObtenerTipoEvento();
        List<TipoServicioResponse> ObtenerTipoServicio();

        UsuarioResponse GrabarUsuario(UsuarioRequest request);
        TipoServicioResponse GrabarTipoServicio(TipoServicioRequest request);
        TipoEventoResponse GrabarTipoEvento(TipoEventoRequest request);
        ReservaResponse GrabarReserva(ReservaRequest request);

        UsuarioResponse EditarUsuario(UsuarioRequest request);
        TipoServicioResponse EditarTipoServicio(TipoServicioRequest request);
        TipoEventoResponse EditarTipoEvento(TipoEventoRequest request);
        ReservaResponse EditarReserva(ReservaRequest request);
        #endregion
    }

    public class MapeoDatosReservas :MapeoDatosBase, IMapeoDatosReservas
    {
        #region Constructores de la clase
        public MapeoDatosReservas(ISqlServer _sqlServer)
        {
            SqlServer = _sqlServer;
        }
        #endregion

        #region Implementacion de las Interfaces
        #region Metodos Get
        List<UsuarioResponse> IMapeoDatosReservas.ObtenerUsuarios()
        {
            var usuario = new List<UsuarioResponse>();
            var tiempoInicial = DateTime.Now;

            SqlServer.AgregarParametro("@i_tipo_dato", SqlDbType.Char, TipoDato.U);
            SqlServer.AgregarParametro("@i_accion", SqlDbType.Char, TipoAccion.G);

            var dataSet = SqlServer.EjecutarProcedimiento(Environment.GetEnvironmentVariable(StrHandler.EnvDatabaseReservas),
                StrHandler.ProcedureReservas);

            DbResponseTime = DateTime.Now - tiempoInicial;
            DbResponseJson = JsonConvert.SerializeObject(dataSet);

            if (dataSet.Tables.Count > 0)
            {
                usuario = SqlTransformador.ConvertTo<UsuarioResponse>(dataSet.Tables[0]);

                // Establecimiento de mensaje serializado
                MensajeJson = JsonConvert.SerializeObject(usuario);
            }

            return usuario;
        }

        List<ReservaResponse> IMapeoDatosReservas.ObtenerReservas()
        {
            var reserva = new List<ReservaResponse>();
            var tiempoInicial = DateTime.Now;

            SqlServer.AgregarParametro("@i_tipo_dato", SqlDbType.Char, TipoDato.R);
            SqlServer.AgregarParametro("@i_accion", SqlDbType.Char, TipoAccion.G);

            var dataSet = SqlServer.EjecutarProcedimiento(Environment.GetEnvironmentVariable(StrHandler.EnvDatabaseReservas),
                StrHandler.ProcedureReservas);

            DbResponseTime = DateTime.Now - tiempoInicial;
            DbResponseJson = JsonConvert.SerializeObject(dataSet);

            if (dataSet.Tables.Count > 0)
            {
                reserva = SqlTransformador.ConvertTo<ReservaResponse>(dataSet.Tables[0]);

                // Establecimiento de mensaje serializado
                MensajeJson = JsonConvert.SerializeObject(reserva);
            }

            return reserva;
        }

        List<TipoEventoResponse> IMapeoDatosReservas.ObtenerTipoEvento()
        {
            var evento = new List<TipoEventoResponse>();
            var tiempoInicial = DateTime.Now;

            SqlServer.AgregarParametro("@i_tipo_dato", SqlDbType.Char, TipoDato.E);
            SqlServer.AgregarParametro("@i_accion", SqlDbType.Char, TipoAccion.G);

            var dataSet = SqlServer.EjecutarProcedimiento(Environment.GetEnvironmentVariable(StrHandler.EnvDatabaseReservas),
                StrHandler.ProcedureReservas);

            DbResponseTime = DateTime.Now - tiempoInicial;
            DbResponseJson = JsonConvert.SerializeObject(dataSet);

            if (dataSet.Tables.Count > 0)
            {
                evento = SqlTransformador.ConvertTo<TipoEventoResponse>(dataSet.Tables[0]);

                // Establecimiento de mensaje serializado
                MensajeJson = JsonConvert.SerializeObject(evento);
            }

            return evento;
        }

        List<TipoServicioResponse> IMapeoDatosReservas.ObtenerTipoServicio()
        {
            var servicio = new List<TipoServicioResponse>();
            var tiempoInicial = DateTime.Now;

            SqlServer.AgregarParametro("@i_tipo_dato", SqlDbType.Char, TipoDato.S);
            SqlServer.AgregarParametro("@i_accion", SqlDbType.Char, TipoAccion.G);

            var dataSet = SqlServer.EjecutarProcedimiento(Environment.GetEnvironmentVariable(StrHandler.EnvDatabaseReservas),
                StrHandler.ProcedureReservas);

            DbResponseTime = DateTime.Now - tiempoInicial;
            DbResponseJson = JsonConvert.SerializeObject(dataSet);

            if (dataSet.Tables.Count > 0)
            {
                servicio = SqlTransformador.ConvertTo<TipoServicioResponse>(dataSet.Tables[0]);

                // Establecimiento de mensaje serializado
                MensajeJson = JsonConvert.SerializeObject(servicio);
            }

            return servicio;
        }

        #endregion

        #region Metodos Post
        UsuarioResponse IMapeoDatosReservas.GrabarUsuario(UsuarioRequest request)
        {
            var usuario = new List<UsuarioResponse>();
            var tiempoInicial = DateTime.Now;

            SqlServer.AgregarParametro("@i_nombre_usuario", SqlDbType.VarChar, request.Nombre);
            SqlServer.AgregarParametro("@i_apellido_usuario", SqlDbType.VarChar, request.Apellido);
            SqlServer.AgregarParametro("@i_correo_usuario", SqlDbType.VarChar, request.Correo);
            SqlServer.AgregarParametro("@i_telefono_usuario", SqlDbType.VarChar, request.Telefono);
            SqlServer.AgregarParametro("@i_estado", SqlDbType.Char, request.Estado);

            SqlServer.AgregarParametro("@i_accion", SqlDbType.Char, TipoAccion.I);
            SqlServer.AgregarParametro("@i_tipo_dato", SqlDbType.Char, TipoDato.U);

            var dataSet = SqlServer.EjecutarProcedimiento(Environment.GetEnvironmentVariable(StrHandler.EnvDatabaseReservas),
                StrHandler.ProcedureReservas);

            DbResponseTime = DateTime.Now - tiempoInicial;
            DbResponseJson = JsonConvert.SerializeObject(dataSet);

            if (dataSet.Tables.Count > 0)
            {
                usuario = SqlTransformador.ConvertTo<UsuarioResponse>(dataSet.Tables[0]);

                // Establecimiento de mensaje serializado
                MensajeJson = JsonConvert.SerializeObject(usuario);
            }

            return usuario[0];
        }
        TipoServicioResponse IMapeoDatosReservas.GrabarTipoServicio(TipoServicioRequest request)
        {
            var tipoServicio = new List<TipoServicioResponse>();
            var tiempoInicial = DateTime.Now;

            SqlServer.AgregarParametro("@i_nombre_servicio", SqlDbType.VarChar, request.Nombre);
            SqlServer.AgregarParametro("@i_costo_servicio", SqlDbType.Decimal, request.Costo);
            SqlServer.AgregarParametro("@i_estado", SqlDbType.Char, request.Estado);

            SqlServer.AgregarParametro("@i_accion", SqlDbType.Char, TipoAccion.I);
            SqlServer.AgregarParametro("@i_tipo_dato", SqlDbType.Char, TipoDato.S);

            var dataSet = SqlServer.EjecutarProcedimiento(Environment.GetEnvironmentVariable(StrHandler.EnvDatabaseReservas),
                StrHandler.ProcedureReservas);

            DbResponseTime = DateTime.Now - tiempoInicial;
            DbResponseJson = JsonConvert.SerializeObject(dataSet);

            if (dataSet.Tables.Count > 0)
            {
                tipoServicio = SqlTransformador.ConvertTo<TipoServicioResponse>(dataSet.Tables[0]);

                // Establecimiento de mensaje serializado
                MensajeJson = JsonConvert.SerializeObject(tipoServicio);
            }

            return tipoServicio[0];
        }
        TipoEventoResponse IMapeoDatosReservas.GrabarTipoEvento(TipoEventoRequest request)
        {
            var tipoServicio = new List<TipoEventoResponse>();
            var tiempoInicial = DateTime.Now;

            SqlServer.AgregarParametro("@i_nombre_evento", SqlDbType.VarChar, request.Nombre);
            SqlServer.AgregarParametro("@i_costo_evento", SqlDbType.Decimal, request.Costo);
            SqlServer.AgregarParametro("@i_estado", SqlDbType.Char, request.Estado);

            SqlServer.AgregarParametro("@i_accion", SqlDbType.Char, TipoAccion.I);
            SqlServer.AgregarParametro("@i_tipo_dato", SqlDbType.Char, TipoDato.E);

            var dataSet = SqlServer.EjecutarProcedimiento(Environment.GetEnvironmentVariable(StrHandler.EnvDatabaseReservas),
                StrHandler.ProcedureReservas);

            DbResponseTime = DateTime.Now - tiempoInicial;
            DbResponseJson = JsonConvert.SerializeObject(dataSet);

            if (dataSet.Tables.Count > 0)
            {
                tipoServicio = SqlTransformador.ConvertTo<TipoEventoResponse>(dataSet.Tables[0]);

                // Establecimiento de mensaje serializado
                MensajeJson = JsonConvert.SerializeObject(tipoServicio);
            }

            return tipoServicio[0];
        }
        ReservaResponse IMapeoDatosReservas.GrabarReserva(ReservaRequest request)
        {
            var reserva = new List<ReservaResponse>();
            var tiempoInicial = DateTime.Now;

            SqlServer.AgregarParametro("@i_id_usuario", SqlDbType.BigInt, request.Id_Usuario);
            SqlServer.AgregarParametro("@i_id_tipo_servicio", SqlDbType.BigInt, request.Id_Tipo_Servicio);
            SqlServer.AgregarParametro("@i_id_tipo_evento", SqlDbType.BigInt, request.Id_Tipo_Evento);
            SqlServer.AgregarParametro("@i_cantidad_camarografos", SqlDbType.Int, request.Cantidad_Camarografos);
            SqlServer.AgregarParametro("@i_duracion_evento", SqlDbType.Int, request.Duracion_Evento_H);
            SqlServer.AgregarParametro("@i_fecha_hora_evento", SqlDbType.DateTime, request.Fecha_Hora_Evento);
            SqlServer.AgregarParametro("@i_direccion_evento", SqlDbType.VarChar, request.Direccion_Evento);

            SqlServer.AgregarParametro("@i_estado", SqlDbType.Char, request.Estado);
            SqlServer.AgregarParametro("@i_accion", SqlDbType.Char, TipoAccion.I);
            SqlServer.AgregarParametro("@i_tipo_dato", SqlDbType.Char, TipoDato.R);

            var dataSet = SqlServer.EjecutarProcedimiento(Environment.GetEnvironmentVariable(StrHandler.EnvDatabaseReservas),
                StrHandler.ProcedureReservas);

            DbResponseTime = DateTime.Now - tiempoInicial;
            DbResponseJson = JsonConvert.SerializeObject(dataSet);

            if (dataSet.Tables.Count > 0)
            {
                reserva = SqlTransformador.ConvertTo<ReservaResponse>(dataSet.Tables[0]);

                // Establecimiento de mensaje serializado
                MensajeJson = JsonConvert.SerializeObject(reserva);
            }

            return reserva[0];
        }

        #endregion

        #region Metodos Put
        UsuarioResponse IMapeoDatosReservas.EditarUsuario(UsuarioRequest request)
        {
            var usuario = new List<UsuarioResponse>();
            var tiempoInicial = DateTime.Now;

            SqlServer.AgregarParametro("@i_nombre_usuario", SqlDbType.VarChar, request.Nombre is null ? DBNull.Value : request.Nombre);
            SqlServer.AgregarParametro("@i_apellido_usuario", SqlDbType.VarChar, request.Apellido is null ? DBNull.Value : request.Apellido);
            SqlServer.AgregarParametro("@i_correo_usuario", SqlDbType.VarChar, request.Correo is null ? DBNull.Value : request.Correo);
            SqlServer.AgregarParametro("@i_telefono_usuario", SqlDbType.VarChar,string.IsNullOrEmpty(request.Telefono)  ? DBNull.Value : request.Telefono);
            SqlServer.AgregarParametro("@i_estado", SqlDbType.VarChar, string.IsNullOrEmpty(request.Estado)  ? DBNull.Value : request.Estado);
            SqlServer.AgregarParametro("@i_id_usuario", SqlDbType.BigInt, request.Id);

            SqlServer.AgregarParametro("@i_accion", SqlDbType.Char, TipoAccion.U);
            SqlServer.AgregarParametro("@i_tipo_dato", SqlDbType.Char, TipoDato.U);

            var dataSet = SqlServer.EjecutarProcedimiento(Environment.GetEnvironmentVariable(StrHandler.EnvDatabaseReservas),
                StrHandler.ProcedureReservas);

            DbResponseTime = DateTime.Now - tiempoInicial;
            DbResponseJson = JsonConvert.SerializeObject(dataSet);

            if (dataSet.Tables.Count > 0)
            {
                usuario = SqlTransformador.ConvertTo<UsuarioResponse>(dataSet.Tables[0]);

                // Establecimiento de mensaje serializado
                MensajeJson = JsonConvert.SerializeObject(usuario);
            }

            return usuario[0];
        }

        TipoServicioResponse IMapeoDatosReservas.EditarTipoServicio(TipoServicioRequest request)
        {
            var tipoServicio = new List<TipoServicioResponse>();
            var tiempoInicial = DateTime.Now;

            SqlServer.AgregarParametro("@i_nombre_servicio", SqlDbType.VarChar, string.IsNullOrEmpty(request.Nombre) ? DBNull.Value : request.Nombre);
            SqlServer.AgregarParametro("@i_costo_servicio", SqlDbType.Decimal, string.IsNullOrEmpty(request.Costo.ToString()) ? DBNull.Value : request.Costo);
            SqlServer.AgregarParametro("@i_estado", SqlDbType.VarChar, string.IsNullOrEmpty(request.Estado) ? DBNull.Value : request.Estado);
            SqlServer.AgregarParametro("@i_id_tipo_servicio", SqlDbType.BigInt, request.Id);

            SqlServer.AgregarParametro("@i_accion", SqlDbType.Char, TipoAccion.U);
            SqlServer.AgregarParametro("@i_tipo_dato", SqlDbType.Char, TipoDato.S);

            var dataSet = SqlServer.EjecutarProcedimiento(Environment.GetEnvironmentVariable(StrHandler.EnvDatabaseReservas),
                StrHandler.ProcedureReservas);

            DbResponseTime = DateTime.Now - tiempoInicial;
            DbResponseJson = JsonConvert.SerializeObject(dataSet);

            if (dataSet.Tables.Count > 0)
            {
                tipoServicio = SqlTransformador.ConvertTo<TipoServicioResponse>(dataSet.Tables[0]);

                // Establecimiento de mensaje serializado
                MensajeJson = JsonConvert.SerializeObject(tipoServicio);
            }

            return tipoServicio[0];
        }

        TipoEventoResponse IMapeoDatosReservas.EditarTipoEvento(TipoEventoRequest request)
        {
            var tipoServicio = new List<TipoEventoResponse>();
            var tiempoInicial = DateTime.Now;

            SqlServer.AgregarParametro("@i_nombre_evento", SqlDbType.VarChar, string.IsNullOrEmpty(request.Nombre) ? DBNull.Value: request.Nombre);
            SqlServer.AgregarParametro("@i_costo_evento", SqlDbType.Decimal, string.IsNullOrEmpty(request.Costo.ToString())? DBNull.Value: request.Costo);
            SqlServer.AgregarParametro("@i_estado", SqlDbType.VarChar, string.IsNullOrEmpty(request.Estado) ? DBNull.Value : request.Estado);
            SqlServer.AgregarParametro("@i_id_tipo_evento", SqlDbType.BigInt, request.Id);

            SqlServer.AgregarParametro("@i_accion", SqlDbType.Char, TipoAccion.U);
            SqlServer.AgregarParametro("@i_tipo_dato", SqlDbType.Char, TipoDato.E);

            var dataSet = SqlServer.EjecutarProcedimiento(Environment.GetEnvironmentVariable(StrHandler.EnvDatabaseReservas),
                StrHandler.ProcedureReservas);

            DbResponseTime = DateTime.Now - tiempoInicial;
            DbResponseJson = JsonConvert.SerializeObject(dataSet);

            if (dataSet.Tables.Count > 0)
            {
                tipoServicio = SqlTransformador.ConvertTo<TipoEventoResponse>(dataSet.Tables[0]);

                // Establecimiento de mensaje serializado
                MensajeJson = JsonConvert.SerializeObject(tipoServicio);
            }

            return tipoServicio[0];
        }

        ReservaResponse IMapeoDatosReservas.EditarReserva(ReservaRequest request)
        {
            var reserva = new List<ReservaResponse>();
            var tiempoInicial = DateTime.Now;

            SqlServer.AgregarParametro("@i_id_reserva", SqlDbType.BigInt, request.Id_Reserva);
            SqlServer.AgregarParametro("@i_id_usuario", SqlDbType.BigInt, string.IsNullOrEmpty(request.Id_Usuario.ToString()) ? DBNull.Value : request.Id_Usuario);
            SqlServer.AgregarParametro("@i_id_tipo_servicio", SqlDbType.BigInt, string.IsNullOrEmpty(request.Id_Tipo_Servicio.ToString()) ? DBNull.Value : request.Id_Tipo_Servicio);
            SqlServer.AgregarParametro("@i_id_tipo_evento", SqlDbType.BigInt, string.IsNullOrEmpty(request.Id_Tipo_Evento.ToString()) ? DBNull.Value : request.Id_Tipo_Evento);
            SqlServer.AgregarParametro("@i_cantidad_camarografos", SqlDbType.Int, string.IsNullOrEmpty(request.Cantidad_Camarografos.ToString()) ? DBNull.Value : request.Cantidad_Camarografos);
            SqlServer.AgregarParametro("@i_duracion_evento", SqlDbType.Int, string.IsNullOrEmpty(request.Duracion_Evento_H.ToString()) ? DBNull.Value : request.Duracion_Evento_H);
            SqlServer.AgregarParametro("@i_fecha_hora_evento", SqlDbType.DateTime, string.IsNullOrEmpty(request.Fecha_Hora_Evento.ToString()) ? DBNull.Value : request.Fecha_Hora_Evento);
            SqlServer.AgregarParametro("@i_direccion_evento", SqlDbType.VarChar, string.IsNullOrEmpty(request.Direccion_Evento) ? DBNull.Value : request.Direccion_Evento);
            SqlServer.AgregarParametro("@i_estado", SqlDbType.Char,string.IsNullOrEmpty(request.Estado) ? DBNull.Value : request.Estado);

            SqlServer.AgregarParametro("@i_accion", SqlDbType.Char, TipoAccion.U);
            SqlServer.AgregarParametro("@i_tipo_dato", SqlDbType.Char, TipoDato.R);

            var dataSet = SqlServer.EjecutarProcedimiento(Environment.GetEnvironmentVariable(StrHandler.EnvDatabaseReservas),
                StrHandler.ProcedureReservas);

            DbResponseTime = DateTime.Now - tiempoInicial;
            DbResponseJson = JsonConvert.SerializeObject(dataSet);

            if (dataSet.Tables.Count > 0)
            {
                reserva = SqlTransformador.ConvertTo<ReservaResponse>(dataSet.Tables[0]);

                // Establecimiento de mensaje serializado
                MensajeJson = JsonConvert.SerializeObject(reserva);
            }

            return reserva[0];
        }
        #endregion
        #endregion

    }

    public enum TipoDato
    {
        U,
        R,
        E,
        S
    }

    public enum TipoAccion
    {
        I,
        U,
        G,
        C
    }

    public enum Estado
    {
        A,
        I,
        C
    }
}
