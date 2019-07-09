using POSWeb.AccesoDatos;
using POSWeb.Entidades;
using POSWeb.Entidades.ResourceFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Net.Mail;
using System.Net;
using System.IO;

namespace POSWeb.LogicaNegocios
{
    /// <summary>
    /// Requerimiento:       MobileToken
    /// Empresa:             BMYASOCIADOS S.A
    /// Autor:               Jorge Bastos - BMYASOCIADOS S.A
    /// Propósito:           Logica de Negocio clase   AlertaLogicaNegocio
    /// Ultima modificacion: 01/12/2017
    /// </summary>
    public class AlertaLogicaNegocio
    {

        AlertaAccesoDatos dal = new AlertaAccesoDatos();
		SesionLogicaNegocio sesion = new SesionLogicaNegocio();
        private readonly IDownloadService _downloadService;
        private readonly ITokenizer _tokenizer;

        /// <summary>
        /// Metodo que sirve para Obtener la lista de tipo Alerta
        /// </summary>
        /// <param name="pAlerta">Filtro de tipo Alerta</param>
        /// <returns></returns>
		public  RespuestaListaAlerta ObtenerListaAlerta(AlertaBase pAlerta)
        {
			          
			var respuesta = new RespuestaListaAlerta();
            try
            { 
				RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pAlerta.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    pAlerta.IdEntidad = respS.Sesion.IdEntidad;
                    respuesta = dal.ObtenerAlerta(pAlerta);
					return respuesta;
                }
                else
					return new RespuestaListaAlerta { Respuesta = respS.Respuesta, ListaAlerta = new List<AlertaBase>() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pAlerta);
				
				BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                            TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);
							
                return new RespuestaListaAlerta { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }

		/// <summary>
        /// Metodo que sirve para Obtener la lista de tipo Alerta
        /// </summary>
        /// <param name="pAlerta">Filtro de tipo Alerta</param>
        /// <returns></returns>
		public  RespuestaAlerta ObtenerAlerta(AlertaBase pAlerta)
        {					
            var respuesta = new RespuestaAlerta();
            try
            { 
				RespuestaSesion respS = sesion.ObtenerSesion(new Sesion() { Token = pAlerta.UsrtokensAuthenticate });
                if (respS.Respuesta.CodMensaje == Respuesta.CodExitoso)
                {
                    pAlerta.IdEntidad = respS.Sesion.IdEntidad;
                    respuesta.Alerta = dal.ObtenerAlerta(pAlerta).ListaAlerta?[0];
                    return respuesta.Alerta != null ?
                      new RespuestaAlerta { Respuesta = new Respuesta(string.Empty, Respuesta.CodExitoso), Alerta = respuesta.Alerta } :
                      new RespuestaAlerta { Respuesta = new Respuesta(Respuestas.GI03, Respuesta.CodExitoso), Alerta = new AlertaBase() };					
                }
                else
					return new RespuestaAlerta { Respuesta = respS.Respuesta, Alerta = new AlertaBase() };
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, pAlerta);
				
				BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                            TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);
							
                return new RespuestaAlerta { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }

        /// <summary>
        /// Metodo que sirve para Enviar una Alerta
        /// </summary>
        /// <param name="pAlerta"></param>
        /// <returns></returns>
        public RespuestaAlertaEntidad EnviarAlerta <T>(ALERTAS alerta, T Object)
        {
            AlertaEntidadLogicaNegocio bl = new AlertaEntidadLogicaNegocio();
            MessageTokenProviderLogicaNegocio tkn = new MessageTokenProviderLogicaNegocio();
            CuentaEmailLogicaNegocio ctabl = new CuentaEmailLogicaNegocio();
            string CorreoDestino = "";
            string NombreDestino = "";
            var tokens = new List<Token>();
            var respuesta = new RespuestaAlertaEntidad();
            try
            {
                respuesta = bl.ObtenerAlertaEntidad(new AlertaEntidad() { CodigoAlerta = alerta.ToString() });
                CuentaEmail cta = ctabl.ObtenerCuentaEmail(new CuentaEmail() { Id = respuesta.AlertaEntidad.IdCuenta }).CuentaEmail;
                switch (alerta)
                {
                    /***********************************************************************************************/
                    /********************            Alertas de Usuario                *****************************/
                    /***********************************************************************************************/
                    case ALERTAS.NewPassUser:
                    case ALERTAS.ForgoutPass:
                    case ALERTAS.LockedUser:
                    case ALERTAS.WrongPass:
                        Usuario usuario = (Usuario)Convert.ChangeType(Object, typeof(Usuario));
                        UsuarioLogicaNegocio ubl = new UsuarioLogicaNegocio();
                        usuario = ubl.ObtenerUsuario(usuario).Usuario;                        
                        tokens = tkn.AddUsuarioTokens(tokens, usuario);
                        CorreoDestino = usuario.CorreoElectronico;
                        NombreDestino = usuario.Nombre;
                        break;
                }
                string subject = Tokenizador(respuesta.AlertaEntidad.Titulo, tokens, false);
                string body = Tokenizador(respuesta.AlertaEntidad.HtmlContent, tokens, true);

                SendEmail(cta, subject, body, cta.CorreoElectronico, cta.Alias,
                    CorreoDestino, NombreDestino, null, null, null, null, null, null, 0, null);

                return respuesta;
            }
            catch (Exception ex)
            {
                ControlExcepciones.ControlExcepciones.ManejoExcepciones(ex, alerta);

                BitacoraLogicaNegocios.RegistrarBitacora(MENSAJES_SISTEMA.ErrorExcepcion,
                            TraceData.Utilities.Enums.TypeTrace.Exception, ex, MethodBase.GetCurrentMethod().Name);

                return new RespuestaAlertaEntidad { Respuesta = new Respuesta(TipoRespuesta.Excepcion, ex.Message, Respuesta.CodNoValido) };
            }
        }
		
		/// <summary>
        /// Notificacion Accion
        /// </summary>
        /// <param name="pAlerta"></param>
        /// <param name="pMensajes"></param>
        /// <returns></returns>
        private void Notificacion(AlertaBase pAlerta, string CodigoAlerta)
        {
			AlertaLogicaNegocio al = new AlertaLogicaNegocio();
			RespuestaAlerta alerta = al.ObtenerAlerta( new AlertaBase() { CodigoAlerta = CodigoAlerta, Activo = true });
            if (alerta != null && alerta.Respuesta.CodMensaje == Respuesta.CodExitoso)
            {
				//al.EnviarAlerta(alerta.Alerta);
            }
        }

        /// <summary>
        /// Metodo que sirve para cambiar los token
        /// </summary>
        /// <param name="template"></param>
        /// <param name="tokens"></param>
        /// <param name="htmlEncode"></param>
        /// <returns></returns>
        public string Tokenizador(string template, IEnumerable<Token> tokens, bool htmlEncode)
        {
            //string result = "";
            foreach (Token t in tokens)
            {
                template = template.Replace(t.Key, t.Value);
            }
            //template = String(template, tokens, htmlEncode);
            return template;
        }


        /// <summary>
        /// Sends an email
        /// </summary>
        /// <param name="emailAccount">Email account to use</param>
        /// <param name="subject">Subject</param>
        /// <param name="body">Body</param>
        /// <param name="fromAddress">From address</param>
        /// <param name="fromName">From display name</param>
        /// <param name="toAddress">To address</param>
        /// <param name="toName">To display name</param>
        /// <param name="replyTo">ReplyTo address</param>
        /// <param name="replyToName">ReplyTo display name</param>
        /// <param name="bcc">BCC addresses list</param>
        /// <param name="cc">CC addresses list</param>
        /// <param name="attachmentFilePath">Attachment file path</param>
        /// <param name="attachmentFileName">Attachment file name. If specified, then this file name will be sent to a recipient. Otherwise, "AttachmentFilePath" name will be used.</param>
        /// <param name="attachedDownloadId">Attachment download ID (another attachedment)</param>
        /// <param name="headers">Headers</param>
        public virtual void SendEmail(CuentaEmail emailAccount, string subject, string body,
            string fromAddress, string fromName, string toAddress, string toName,
             string replyTo = null, string replyToName = null,
            IEnumerable<string> bcc = null, IEnumerable<string> cc = null,
            string attachmentFilePath = null, string attachmentFileName = null,
            int attachedDownloadId = 0, IDictionary<string, string> headers = null)
        {
            var message = new MailMessage();
            //from, to, reply to
            message.From = new MailAddress(fromAddress, fromName);
            message.To.Add(new MailAddress(toAddress, toName ?? ""));
            if (!String.IsNullOrEmpty(replyTo))
            {
                message.ReplyToList.Add(new MailAddress(replyTo, replyToName));
            }

            //BCC
            if (bcc != null)
            {
                foreach (var address in bcc.Where(bccValue => !String.IsNullOrWhiteSpace(bccValue)))
                {
                    message.Bcc.Add(address.Trim());
                }
            }

            //CC
            if (cc != null)
            {
                foreach (var address in cc.Where(ccValue => !String.IsNullOrWhiteSpace(ccValue)))
                {
                    message.CC.Add(address.Trim());
                }
            }

            //content
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            //headers
            if (headers != null)
                foreach (var header in headers)
                {
                    message.Headers.Add(header.Key, header.Value);
                }

            //create the file attachment for this e-mail message
            if (!String.IsNullOrEmpty(attachmentFilePath) &&
                File.Exists(attachmentFilePath))
            {
                var attachment = new Attachment(attachmentFilePath);
                attachment.ContentDisposition.CreationDate = File.GetCreationTime(attachmentFilePath);
                attachment.ContentDisposition.ModificationDate = File.GetLastWriteTime(attachmentFilePath);
                attachment.ContentDisposition.ReadDate = File.GetLastAccessTime(attachmentFilePath);
                if (!String.IsNullOrEmpty(attachmentFileName))
                {
                    attachment.Name = attachmentFileName;
                }
                message.Attachments.Add(attachment);
            }
            //another attachment?
            if (attachedDownloadId > 0)
            {
                var download = _downloadService.GetDownloadById(attachedDownloadId);
                if (download != null)
                {
                    //we do not support URLs as attachments
                    if (!download.UseDownloadUrl)
                    {
                        string fileName = !String.IsNullOrWhiteSpace(download.Filename) ? download.Filename : download.Id.ToString();
                        fileName += download.Extension;


                        var ms = new MemoryStream(download.DownloadBinary);
                        var attachment = new Attachment(ms, fileName);
                        //string contentType = !String.IsNullOrWhiteSpace(download.ContentType) ? download.ContentType : "application/octet-stream";
                        //var attachment = new Attachment(ms, fileName, contentType);
                        attachment.ContentDisposition.CreationDate = DateTime.UtcNow;
                        attachment.ContentDisposition.ModificationDate = DateTime.UtcNow;
                        attachment.ContentDisposition.ReadDate = DateTime.UtcNow;
                        message.Attachments.Add(attachment);
                    }
                }
            }

            //send email
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.UseDefaultCredentials = emailAccount.CredencialesDefecto;
                smtpClient.Host = emailAccount.Servidor;
                smtpClient.Port = emailAccount.Puerto.Value;
                smtpClient.EnableSsl = emailAccount.Ssl;
                smtpClient.Credentials = emailAccount.CredencialesDefecto ?
                    CredentialCache.DefaultNetworkCredentials :
                    new NetworkCredential(emailAccount.Usuario, emailAccount.Contrasena);
                smtpClient.Send(message);
            }
        }
    }//fin de clase
}		