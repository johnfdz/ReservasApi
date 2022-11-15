using Utils;
using GDifare.Utilitario.Log;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Reservas.API.Datos;
using Reservas.API.Entidades;

namespace Reservas.API.Controllers
{
    public class ReservasController : Controller
    {
        #region Miembros privados de la clase
        private readonly ILogHandler logHandler;
        private readonly IMapeoDatosReservas mapeoDatosReservas;
        #endregion

        #region Constructores del controlador

        public ReservasController(
            IMapeoDatosReservas _mapeoDatosReservas,
            ILogHandler _logHandler)
        {
            mapeoDatosReservas = _mapeoDatosReservas;
            logHandler = _logHandler;
        }

        #endregion

        #region Metodos Get
        [HttpGet("consultarusuario/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IList<UsuarioResponse>> ConsultarUsuario(
            [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER)
        {
            try
            {
                // Inicialización de registro en ElasticSearch
                var message = string.Format("get global");
                logHandler.Init(
                    StrHandler.ControllerNameClientes, Request, CONSUMER, REFERENCE_ID,
                    Environment.GetEnvironmentVariable(StrHandler.Environment), message);


                // Ejecución de la operación de datos
                var listaUsuarios = mapeoDatosReservas.ObtenerUsuarios();

                // Envía mensaje de registro en ElasticSearch
                logHandler.SendOkLog(mapeoDatosReservas, string.Empty, StatusCodes.Status200OK, listaUsuarios);

                return Ok(listaUsuarios);
            }
            catch (Exception e)
            {
                return ResponseFault(e);
            }
            finally
            {
                DisposeService();
            }
        }

        [HttpGet("consultartiposervicio/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IList<TipoServicioResponse>> ConsultarTipoServicio(
            [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER)
        {
            try
            {
                // Inicialización de registro en ElasticSearch
                var message = string.Format("get global");
                logHandler.Init(
                    StrHandler.ControllerNameClientes, Request, CONSUMER, REFERENCE_ID,
                    Environment.GetEnvironmentVariable(StrHandler.Environment), message);


                // Ejecución de la operación de datos
                var listaTipoServicio = mapeoDatosReservas.ObtenerTipoServicio();

                // Envía mensaje de registro en ElasticSearch
                logHandler.SendOkLog(mapeoDatosReservas, string.Empty, StatusCodes.Status200OK, listaTipoServicio);

                return Ok(listaTipoServicio);
            }
            catch (Exception e)
            {
                return ResponseFault(e);
            }
            finally
            {
                DisposeService();
            }
        }

        [HttpGet("consultartipoevento/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IList<TipoEventoResponse>> ConsultarTipoEvento(
            [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER)
        {
            try
            {
                // Inicialización de registro en ElasticSearch
                var message = string.Format("get global");
                logHandler.Init(
                    StrHandler.ControllerNameClientes, Request, CONSUMER, REFERENCE_ID,
                    Environment.GetEnvironmentVariable(StrHandler.Environment), message);


                // Ejecución de la operación de datos
                var listaTipoEvento = mapeoDatosReservas.ObtenerTipoEvento();

                // Envía mensaje de registro en ElasticSearch
                logHandler.SendOkLog(mapeoDatosReservas, string.Empty, StatusCodes.Status200OK, listaTipoEvento);

                return Ok(listaTipoEvento);
            }
            catch (Exception e)
            {
                return ResponseFault(e);
            }
            finally
            {
                DisposeService();
            }
        }

        [HttpGet("consultarreservas/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IList<ReservaResponse>> ConsultarReservas(
            [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER)
        {
            try
            {
                // Inicialización de registro en ElasticSearch
                var message = string.Format("get global");
                logHandler.Init(
                    StrHandler.ControllerNameClientes, Request, CONSUMER, REFERENCE_ID,
                    Environment.GetEnvironmentVariable(StrHandler.Environment), message);


                // Ejecución de la operación de datos
                var listaReservas = mapeoDatosReservas.ObtenerReservas();

                // Envía mensaje de registro en ElasticSearch
                logHandler.SendOkLog(mapeoDatosReservas, string.Empty, StatusCodes.Status200OK, listaReservas);

                return Ok(listaReservas);
            }
            catch (Exception e)
            {
                return ResponseFault(e);
            }
            finally
            {
                DisposeService();
            }
        }
        #endregion

        #region Metodos Post
        [HttpPost("registrarusuario/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UsuarioResponse> GuardarUsuario(
            [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER,
            [FromBody] UsuarioRequest usuario)
        {
            try
            {
                // Inicialización de registro en ElasticSearch
                var message = string.Format("{0}, {1}", usuario.Nombre, usuario.Id);
                logHandler.Init(
                    StrHandler.ControllerNameClientes, Request, CONSUMER, REFERENCE_ID,
                    Environment.GetEnvironmentVariable(StrHandler.Environment), message);

                // Validaciones de parámetros de entrada
                string codigo, descripcion;
                if (!ValidarUsuario(usuario,out codigo,out descripcion))
                {
                    return ResponseBadRequest(codigo, descripcion);
                }

                // Ejecución de la operación de datos
                var listaUsuarios = mapeoDatosReservas.GrabarUsuario(usuario);

                // Envía mensaje de registro en ElasticSearch
                logHandler.SendOkLog(mapeoDatosReservas, string.Empty, StatusCodes.Status200OK, listaUsuarios);

                return Ok(listaUsuarios);
            }
            catch (Exception e)
            {
                return ResponseFault(e);
            }
            finally
            {
                DisposeService();
            }
        }

        [HttpPost("registratipoevento/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TipoEventoResponse> GuardarTipoEvento(
            [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER,
            [FromBody] TipoEventoRequest evento)
        {
            try
            {
                // Inicialización de registro en ElasticSearch
                var message = string.Format("{0}, {1}", evento.Nombre, evento.Id);
                logHandler.Init(
                    StrHandler.ControllerNameClientes, Request, CONSUMER, REFERENCE_ID,
                    Environment.GetEnvironmentVariable(StrHandler.Environment), message);

                // Validaciones de parámetros de entrada

                string codError, descError;
                var respuesta = ValidarTipo(evento, out codError, out descError);

                if (!respuesta)
                {
                    return ResponseBadRequest(codError, descError);
                }

                // Ejecución de la operación de datos
                var listaTipoEvento = mapeoDatosReservas.GrabarTipoEvento(evento);

                // Envía mensaje de registro en ElasticSearch
                logHandler.SendOkLog(mapeoDatosReservas, string.Empty, StatusCodes.Status200OK, listaTipoEvento);

                return Ok(listaTipoEvento);
            }
            catch (Exception e)
            {
                return ResponseFault(e);
            }
            finally
            {
                DisposeService();
            }
        }

        [HttpPost("registratiposervicio/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TipoServicioResponse> GuardarTipoServicio(
            [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER,
            [FromBody] TipoServicioRequest servicio)
        {
            try
            {
                // Inicialización de registro en ElasticSearch
                var message = string.Format("{0}, {1}", servicio.Nombre, servicio.Id);
                logHandler.Init(
                    StrHandler.ControllerNameClientes, Request, CONSUMER, REFERENCE_ID,
                    Environment.GetEnvironmentVariable(StrHandler.Environment), message);

                // Validaciones de parámetros de entrada

                string codError, descError;
                var respuesta = ValidarTipo(servicio, out codError, out descError);

                if (!respuesta)
                {
                    return ResponseBadRequest(codError, descError);
                }

                // Ejecución de la operación de datos
                var listaTipoServicio = mapeoDatosReservas.GrabarTipoServicio(servicio);

                // Envía mensaje de registro en ElasticSearch
                logHandler.SendOkLog(mapeoDatosReservas, string.Empty, StatusCodes.Status200OK, listaTipoServicio);

                return Ok(listaTipoServicio);
            }
            catch (Exception e)
            {
                return ResponseFault(e);
            }
            finally
            {
                DisposeService();
            }
        }

        [HttpPost("registrarreserva/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ReservaResponse> GuardarReserva(
            [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER,
            [FromBody] ReservaRequest reserva)
        {
            try
            {
                // Inicialización de registro en ElasticSearch
                var message = string.Format("{0}", reserva.Id_Reserva);
                logHandler.Init(
                    StrHandler.ControllerNameClientes, Request, CONSUMER, REFERENCE_ID,
                    Environment.GetEnvironmentVariable(StrHandler.Environment), message);

                // Validaciones de parámetros de entrada

                string codError, descError;
                var respuesta = ValidarReserva(reserva, out codError, out descError);

                if (!respuesta)
                {
                    return ResponseBadRequest(codError, descError);
                }

                // Ejecución de la operación de datos
                var listaReserva = mapeoDatosReservas.GrabarReserva(reserva);

                // Envía mensaje de registro en ElasticSearch
                logHandler.SendOkLog(mapeoDatosReservas, string.Empty, StatusCodes.Status200OK, listaReserva);

                return Ok(listaReserva);
            }
            catch (Exception e)
            {
                return ResponseFault(e);
            }
            finally
            {
                DisposeService();
            }
        }

        #endregion

        #region Metodos Put
        [HttpPut("editarusuario/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UsuarioResponse> EditarUsuario(
            [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER,
            [FromBody] UsuarioRequest usuario)
        {
            try
            {
                // Inicialización de registro en ElasticSearch
                var message = string.Format("{0}, {1}", usuario.Nombre, usuario.Id);
                logHandler.Init(
                    StrHandler.ControllerNameClientes, Request, CONSUMER, REFERENCE_ID,
                    Environment.GetEnvironmentVariable(StrHandler.Environment), message);

                // Validaciones de parámetros de entrada

                string codError, descError;
                var respuesta = ValidarUsuarioEditar(usuario, out codError, out descError);

                if (!respuesta)
                {
                    return ResponseBadRequest(codError, descError);
                }

                // Ejecución de la operación de datos
                var listaUsuarios = mapeoDatosReservas.EditarUsuario(usuario);

                // Envía mensaje de registro en ElasticSearch
                logHandler.SendOkLog(mapeoDatosReservas, string.Empty, StatusCodes.Status200OK, listaUsuarios);

                return Ok(listaUsuarios);
            }
            catch (Exception e)
            {
                return ResponseFault(e);
            }
            finally
            {
                DisposeService();
            }
        }

        [HttpPut("editartiposervicio/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TipoServicioResponse> EditarTipoServicio(
            [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER,
            [FromBody] TipoServicioRequest servicio)
        {
            try
            {
                // Inicialización de registro en ElasticSearch
                var message = string.Format("{0}, {1}", servicio.Nombre, servicio.Id);
                logHandler.Init(
                    StrHandler.ControllerNameClientes, Request, CONSUMER, REFERENCE_ID,
                    Environment.GetEnvironmentVariable(StrHandler.Environment), message);

                // Validaciones de parámetros de entrada

                string codError, descError;
                var respuesta = ValidarTipoEditar(servicio, out codError, out descError);

                if (!respuesta)
                {
                    return ResponseBadRequest(codError, descError);
                }

                // Ejecución de la operación de datos
                var listaTipoServicio = mapeoDatosReservas.EditarTipoServicio(servicio);

                // Envía mensaje de registro en ElasticSearch
                logHandler.SendOkLog(mapeoDatosReservas, string.Empty, StatusCodes.Status200OK, listaTipoServicio);

                return Ok(listaTipoServicio);
            }
            catch (Exception e)
            {
                return ResponseFault(e);
            }
            finally
            {
                DisposeService();
            }
        }

        [HttpPut("editartipoevento/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TipoEventoResponse> EditarTipoEvento(
            [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER,
            [FromBody] TipoEventoRequest evento)
        {
            try
            {
                // Inicialización de registro en ElasticSearch
                var message = string.Format("{0}, {1}", evento.Nombre, evento.Id);
                logHandler.Init(
                    StrHandler.ControllerNameClientes, Request, CONSUMER, REFERENCE_ID,
                    Environment.GetEnvironmentVariable(StrHandler.Environment), message);

                // Validaciones de parámetros de entrada

                string codError, descError;
                var respuesta = ValidarTipoEditar(evento, out codError, out descError);

                if (!respuesta)
                {
                    return ResponseBadRequest(codError, descError);
                }

                // Ejecución de la operación de datos
                var listaTipoEvento = mapeoDatosReservas.EditarTipoEvento(evento);

                // Envía mensaje de registro en ElasticSearch
                logHandler.SendOkLog(mapeoDatosReservas, string.Empty, StatusCodes.Status200OK, listaTipoEvento);

                return Ok(listaTipoEvento);
            }
            catch (Exception e)
            {
                return ResponseFault(e);
            }
            finally
            {
                DisposeService();
            }
        }

        [HttpPut("editarreserva/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ReservaResponse> EditarReserva(
            [FromHeader] string REFERENCE_ID, [FromHeader] string CONSUMER,
            [FromBody] ReservaRequest reserva)
        {
            try
            {
                // Inicialización de registro en ElasticSearch
                var message = string.Format("{0}", reserva.Id_Reserva);
                logHandler.Init(
                    StrHandler.ControllerNameClientes, Request, CONSUMER, REFERENCE_ID,
                    Environment.GetEnvironmentVariable(StrHandler.Environment), message);

                // Validaciones de parámetros de entrada

                string codError, descError;
                var respuesta = ValidarReservaEditar(reserva, out codError, out descError);

                if (!respuesta)
                {
                    return ResponseBadRequest(codError, descError);
                }

                // Ejecución de la operación de datos
                var listaReserva = mapeoDatosReservas.EditarReserva(reserva);

                // Envía mensaje de registro en ElasticSearch
                logHandler.SendOkLog(mapeoDatosReservas, string.Empty, StatusCodes.Status200OK, listaReserva);

                return Ok(listaReserva);
            }
            catch (Exception e)
            {
                return ResponseFault(e);
            }
            finally
            {
                DisposeService();
            }
        }
        #endregion

        #region Metodos de devolucion de Errores

        private bool ValidarUsuario(UsuarioRequest usuario, out string codigo, out string descripcion)
        {
            if (!EsCampoNuloVacio(usuario.Nombre, StrHandler.CODE_ERROR_VAL_06, StrHandler.ERROR_VAL_06, out codigo, out descripcion)){ return false; }
            if (!EsCampoNuloVacio(usuario.Apellido, StrHandler.CODE_ERROR_VAL_07, StrHandler.ERROR_VAL_07, out codigo, out descripcion)){ return false; }
            if(!EsCampoNuloVacio(usuario.Correo, StrHandler.CODE_ERROR_VAL_08, StrHandler.ERROR_VAL_08, out codigo, out descripcion)){ return false;}
            if (!EsCampoNuloVacio(usuario.Telefono, StrHandler.CODE_ERROR_VAL_09, StrHandler.ERROR_VAL_09, out codigo, out descripcion)){return false;}
            if(!ValidarEstado(usuario.Estado, out codigo, out descripcion)) { return false; }
            if (!IsValidEmail(usuario.Correo, out codigo, out descripcion)){ return false; }
            if(!ValidarCelular(usuario.Telefono, out codigo, out descripcion)) { return false; }
            return true;
        }

        private bool ValidarUsuarioEditar(UsuarioRequest usuario, out string codigo, out string descripcion)
        {
            if (!EsNumInvalido(usuario.Id, StrHandler.CODE_ERROR_VAL_10, StrHandler.ERROR_VAL_10, out codigo, out descripcion)) { return false; }
            if (!EsCampoVacio(usuario.Nombre, StrHandler.CODE_ERROR_VAL_06, StrHandler.ERROR_VAL_06, out codigo, out descripcion)) { return false; }
            if (!EsCampoVacio(usuario.Apellido, StrHandler.CODE_ERROR_VAL_07, StrHandler.ERROR_VAL_07, out codigo, out descripcion)) { return false; }
            if (!EsCampoVacio(usuario.Correo, StrHandler.CODE_ERROR_VAL_08, StrHandler.ERROR_VAL_08, out codigo, out descripcion)) { return false; }
            if (!EsCampoVacio(usuario.Telefono, StrHandler.CODE_ERROR_VAL_09, StrHandler.ERROR_VAL_09, out codigo, out descripcion)) { return false; }
            if (usuario.Estado != null)
            {
                if (!ValidarEstado(usuario.Estado, out codigo, out descripcion)) { return false; }
            }
            if (usuario.Correo != null)
            {
                if (!IsValidEmail(usuario.Correo, out codigo, out descripcion)) { return false; }
            }
            if (usuario.Telefono != null)
            {
                if (!ValidarCelular(usuario.Telefono, out codigo, out descripcion)) { return false; }
            }
            return true;
        }

        private bool ValidarTipo(TipoRequest tipo, out string codigo, out string descripcion)
        {
            if (!EsCampoNuloVacio(tipo.Nombre, StrHandler.CODE_ERROR_VAL_16, StrHandler.ERROR_VAL_16, out codigo, out descripcion)) { return false; }
            if (!ValidarCosto(tipo.Costo,out codigo, out descripcion)) { return false; }
            if (!ValidarEstado(tipo.Estado, out codigo, out descripcion)) { return false; }
            return true;
        }

        private bool ValidarTipoEditar(TipoRequest tipo, out string codigo, out string descripcion)
        {
            if (!EsNumInvalido(tipo.Id, StrHandler.CODE_ERROR_VAL_17, StrHandler.ERROR_VAL_17, out codigo, out descripcion)) { return false; }
            if (!EsCampoVacio(tipo.Nombre, StrHandler.CODE_ERROR_VAL_16, StrHandler.ERROR_VAL_16, out codigo, out descripcion)) { return false; }
            if (tipo.Costo.ToString() !=null)
            {
                if (!ValidarCosto(tipo.Costo, out codigo, out descripcion)) { return false; }
            }
            if (tipo.Estado != null)
            {
                if (!ValidarEstado(tipo.Estado, out codigo, out descripcion)) { return false; }
            }
            return true;
        }

        private bool ValidarReserva(ReservaRequest reserva, out string codigo, out string descripcion)
        {
            if (!EsNumInvalido(reserva.Id_Usuario, StrHandler.CODE_ERROR_VAL_10, StrHandler.ERROR_VAL_10, out codigo, out descripcion)) { return false; }
            if (!EsNumInvalido(reserva.Id_Tipo_Evento, StrHandler.CODE_ERROR_VAL_13, StrHandler.ERROR_VAL_13, out codigo, out descripcion)) { return false; }
            if (!EsNumInvalido(reserva.Id_Tipo_Servicio, StrHandler.CODE_ERROR_VAL_14, StrHandler.ERROR_VAL_14, out codigo, out descripcion)) { return false; }
            if (!EsNumInvalido(reserva.Cantidad_Camarografos, StrHandler.CODE_ERROR_VAL_19, StrHandler.ERROR_VAL_19, out codigo, out descripcion)) { return false; }
            if (!EsNumInvalido(reserva.Duracion_Evento_H, StrHandler.CODE_ERROR_VAL_20, StrHandler.ERROR_VAL_20, out codigo, out descripcion)) { return false; }
            if (!ValidarFecha(reserva.Fecha_Hora_Evento, out codigo, out descripcion)) { return false; }
            if (!EsCampoNuloVacio(reserva.Direccion_Evento, StrHandler.CODE_ERROR_VAL_21, StrHandler.ERROR_VAL_21, out codigo, out descripcion)) { return false; }
            if (!ValidarEstado(reserva.Estado, out codigo, out descripcion)) { return false; }
            return true;
        }

        private bool ValidarReservaEditar(ReservaRequest reserva, out string codigo, out string descripcion)
        {
            if (!EsNumInvalido(reserva.Id_Reserva, StrHandler.CODE_ERROR_VAL_10, StrHandler.ERROR_VAL_10, out codigo, out descripcion)) { return false; }

            if (reserva.Id_Usuario.ToString() != null)
            {
                if (!EsNumInvalido(reserva.Id_Usuario, StrHandler.CODE_ERROR_VAL_10, StrHandler.ERROR_VAL_10, out codigo, out descripcion)) { return false; }
            }
            if (reserva.Id_Tipo_Evento.ToString() != null)
            {
                if (!EsNumInvalido(reserva.Id_Tipo_Evento, StrHandler.CODE_ERROR_VAL_13, StrHandler.ERROR_VAL_13, out codigo, out descripcion)) { return false; }
            }
            if (reserva.Id_Tipo_Servicio.ToString() != null)
            {
                if (!EsNumInvalido(reserva.Id_Tipo_Servicio, StrHandler.CODE_ERROR_VAL_14, StrHandler.ERROR_VAL_14, out codigo, out descripcion)) { return false; }
            }
            if (reserva.Cantidad_Camarografos.ToString() != null)
            {
                if (!EsNumInvalido(reserva.Cantidad_Camarografos, StrHandler.CODE_ERROR_VAL_19, StrHandler.ERROR_VAL_19, out codigo, out descripcion)) { return false; }
            }
            if (reserva.Duracion_Evento_H.ToString() != null)
            {
                if (!EsNumInvalido(reserva.Duracion_Evento_H, StrHandler.CODE_ERROR_VAL_20, StrHandler.ERROR_VAL_20, out codigo, out descripcion)) { return false; }
            }
            if (reserva.Fecha_Hora_Evento.ToString() != null)
            {
                if (!ValidarFecha(reserva.Fecha_Hora_Evento, out codigo, out descripcion)) { return false; }
            }
            if (reserva.Direccion_Evento != null)
            {
                if (!EsCampoVacio(reserva.Direccion_Evento, StrHandler.CODE_ERROR_VAL_21, StrHandler.ERROR_VAL_21, out codigo, out descripcion)) { return false; }
            }
            if (reserva.Estado != null)
            {
                if (!ValidarEstado(reserva.Estado, out codigo, out descripcion)) { return false; }
            }
            return true;
        }

        private bool ValidarFecha(DateTime? fecha, out string codigo, out string descripcion)
        {           
            codigo = descripcion = string.Empty;
            if (fecha < DateTime.Today)
            {
                codigo = StrHandler.CODE_ERROR_VAL_18;
                descripcion = StrHandler.ERROR_VAL_18;
                return false;
            }
            return true;
        }

        private bool EsNumInvalido(long? id, string codeError, string descError, out string codigo, out string descripcion)
        {
            codigo = descripcion = string.Empty;
            if (id <= 0)
            {
                codigo = codeError;
                descripcion = descError;
                return false;
            }
            return true;
        }

        private bool ValidarCosto(double? costo, out string codigo, out string descripcion)
        {
            string a = costo.ToString();
            double b;
            codigo = descripcion = string.Empty;
            if (!double.TryParse(a, out b))
            {
                codigo = StrHandler.CODE_ERROR_VAL_15;
                descripcion = StrHandler.ERROR_VAL_15;
                return false;
            }
            return true;
        }

        private bool EsCampoNuloVacio(string texto, string codeError, string descError, out string codigo, out string descripcion)
        {
            codigo = descripcion = string.Empty;
            if (string.IsNullOrEmpty(texto))
            {
                codigo = codeError;
                descripcion = descError;
                return false;
            }
            return true;
        }
       
        private bool EsCampoVacio(string texto, string codeError, string descError, out string codigo, out string descripcion)
        {
            codigo = descripcion = string.Empty;
            if (texto == string.Empty)
            {
                codigo = codeError;
                descripcion = descError;
                return false;
            }
            return true;
        }

        private bool ValidarEstado(string estado, out string codigo, out string descripcion)
        {
            codigo = descripcion = string.Empty;
            if (estado != Estado.A.ToString() && estado != Estado.I.ToString() && estado != Estado.C.ToString())
            {
                codigo = StrHandler.CODE_ERROR_VAL_05;
                descripcion = StrHandler.ERROR_VAL_05;
                return false;
            }
            return true;
        }

        private bool IsValidEmail(string email, out string codigo, out string descripcion)
        {
            codigo = descripcion = string.Empty;
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                codigo = StrHandler.CODE_ERROR_VAL_08;
                descripcion = StrHandler.ERROR_VAL_08;
                return false;
            }
        }

        private bool ValidarCelular(string celular, out string codigo, out string descripcion)
        {
            codigo = descripcion = string.Empty;

            if (celular.Length != 10)
            {
                codigo = StrHandler.CODE_ERROR_VAL_09;
                descripcion = StrHandler.ERROR_VAL_09;
                return false;
            }
            return true;
        }

        private ObjectResult ResponseBadRequest(string code, string description)
        {
            var statusCode = StatusCodes.Status400BadRequest;
            var fault = logHandler.SendErrorLog(mapeoDatosReservas, string.Empty, statusCode, code, description);

            return StatusCode(statusCode, fault);
        }

        private ObjectResult ResponseFault(Exception e)
        {
            var statusCode = StatusCodes.Status500InternalServerError;
            var fault = logHandler.SendErrorLog(mapeoDatosReservas, string.Empty, statusCode, e);

            return StatusCode(statusCode, fault);
        }
        #endregion

        #region Métodos de liberación de recursos

        private void DisposeService()
        {
            GC.RemoveMemoryPressure(1000);
            GC.Collect();
        }

        #endregion
    }
}
