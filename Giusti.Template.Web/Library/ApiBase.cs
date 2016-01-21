using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Net.Http;
using Newtonsoft.Json;
using System.Web.Security;
using Giusti.Template.Model;
using Giusti.Template.Model.Dominio;
using Giusti.Template.Business;
using Giusti.Template.Business.Library;
using System;
using Giusti.Template.Model.Results;

namespace Giusti.Template.Web.Library
{
    /// <summary>
    /// ApiBase
    /// </summary>
    public abstract partial class ApiBase : ApiController
    {
        protected ServiceBusiness biz = new ServiceBusiness();

        #region Return Ok for DELETE

        /// <summary>
        /// RetornaMensagemOk
        /// </summary>
        /// <param name="resultado"></param>
        /// <returns></returns>
        protected HttpResponseMessage RetornaMensagemOk(ServiceResult resultado)
        {
            var json = JsonConvert.SerializeObject(resultado);
            HttpResponseMessage response = this.Request.CreateResponse(HttpStatusCode.OK, json);
            return response;
        }

        #endregion
        
        #region Return Error for PUT, POST, DELETE

        /// <summary>
        /// RetornaMensagemErro
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="resultado"></param>
        /// <returns></returns>
        protected HttpResponseMessage RetornaMensagemErro(HttpStatusCode statusCode, ServiceResult resultado)
        {
            var json = JsonConvert.SerializeObject(resultado);
            HttpResponseMessage response = this.Request.CreateResponse(statusCode, json);
            return response;
        }

        /// <summary>
        /// RetornaMensagemErro
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        protected HttpResponseMessage RetornaMensagemErro(HttpStatusCode statusCode, Exception ex)
        {
            ServiceResult serviceResult = new ServiceResult();

            serviceResult.Success = false;
            serviceResult.Messages.Add(UtilitarioBusiness.RetornaExceptionMessages(ex));

            var json = JsonConvert.SerializeObject(serviceResult);
            HttpResponseMessage response = this.Request.CreateResponse(statusCode, json);
            return response;
        }

        #endregion
        
        #region Return Error for GET
        
        /// <summary>
        /// GeraErro
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="ex"></param>
        protected void GeraErro(HttpStatusCode statusCode, Exception ex)
        {
            ServiceResult serviceResult = new ServiceResult();

            serviceResult.Success = false;
            serviceResult.Messages.Add(UtilitarioBusiness.RetornaExceptionMessages(ex));

            var json = JsonConvert.SerializeObject(serviceResult);
            HttpResponseMessage response = this.Request.CreateErrorResponse(statusCode, json);
            throw new HttpResponseException(response);
        }

        /// <summary>
        /// GeraErro
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="resultado"></param>
        protected void GeraErro(HttpStatusCode statusCode, ServiceResult resultado)
        {
            var json = JsonConvert.SerializeObject(resultado);
            HttpResponseMessage response = this.Request.CreateErrorResponse(statusCode, json);
            throw new HttpResponseException(response);
        }

        #endregion
        
        #region Autenticacao

        /// <summary>
        /// RetornaToken
        /// </summary>
        /// <returns></returns>
        protected string RetornaToken()
        {
            string token = string.Empty;
            if (Request.Headers.Authorization != null)
                token = Request.Headers.Authorization.Parameter;

            if (token == string.Empty)
                throw new UnauthorizedAccessException(MensagemBusiness.RetornaMensagens("Autenticacao_TokenVazio"));

            return token;
        }

        /// <summary>
        /// RetornaUserAgent
        /// </summary>
        /// <returns></returns>
        protected string RetornaUserAgent()
        {
            string userAgent = string.Empty;
            if (Request.Headers.UserAgent != null)
                userAgent = Request.Headers.UserAgent.ToString();

            return userAgent;
        }

        /// <summary>
        /// RetornaTokenDescriptografado
        /// </summary>
        /// <returns></returns>
        protected FormsAuthenticationTicket RetornaTokenDescriptografado()
        {
            string token = RetornaToken();
            FormsAuthenticationTicket tokenDescriptografado = FormsAuthentication.Decrypt(token);
            return tokenDescriptografado;
        }

        /// <summary>
        /// RetornaEmailAutenticado
        /// </summary>
        /// <returns></returns>
        public string RetornaEmailAutenticado()
        {
            return RetornaTokenDescriptografado().Name;
        }

        /// <summary>
        /// RetornaRotinasUsuario
        /// </summary>
        /// <returns></returns>
        protected string[] RetornaRotinasUsuario()
        {
            string userData = RetornaTokenDescriptografado().UserData;
            string[] roles = userData.Split(',');
            return roles;
        }

        /// <summary>
        /// VerificaAutenticacao
        /// </summary>
        /// <param name="codigoFuncionalidade"></param>
        /// <param name="funcionalidade"></param>
        protected void VerificaAutenticacao(string codigoFuncionalidade, string funcionalidade)
        {
            VerificaAutenticacao(RetornaToken(), codigoFuncionalidade, funcionalidade);
        }

        /// <summary>
        /// VerificaAutenticacao
        /// </summary>
        /// <param name="token"></param>
        /// <param name="codigoFuncionalidade"></param>
        /// <param name="funcionalidade"></param>
        protected void VerificaAutenticacao(string token, string codigoFuncionalidade, string funcionalidade)
        {
            biz.VerificaAutenticacao(token, codigoFuncionalidade, funcionalidade);
            if (!biz.IsValid())
                throw new UnauthorizedAccessException();
        }

        #endregion
        
        #region Log

        /// <summary>
        /// GravaLog
        /// </summary>
        /// <param name="tipoAcao"></param>
        /// <param name="nomeFuncionalidade"></param>
        /// <param name="registroId"></param>
        protected void GravaLog(enumTipoAcao tipoAcao, string nomeFuncionalidade, int? registroId)
        {
            if (!Convert.ToBoolean(UtilitarioBusiness.RetornaChaveConfig("SalvaLogAcoes")))
                return;

            try
            {
                string ipMaquina = string.Empty;
                string nomeMaquina = Dns.GetHostName();
                IPAddress[] ip = Dns.GetHostAddresses(nomeMaquina);
                ipMaquina = ip[1].ToString();
                string emailAutenticado = RetornaEmailAutenticado();
                Usuario usuario = biz.RetornaUsuario_Email(emailAutenticado);

                biz.SalvaLog(new Log() { Acao = tipoAcao.ToString(), Funcionalidade = nomeFuncionalidade, DataInclusao = DateTime.Now, OrigemAcesso = RetornaUserAgent(), RegistroId = registroId, IpMaquina = ipMaquina, UsuarioId = usuario.Id });
            }
            catch
            {
                //vazio, pois o erro de gravação de log não pode interromper o processamento.
            }
        }

        #endregion
    }
}