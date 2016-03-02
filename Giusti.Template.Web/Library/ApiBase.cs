﻿using Giusti.Template.Business;
using Giusti.Template.Business.Library;
using Giusti.Template.Model;
using Giusti.Template.Model.Dominio;
using Giusti.Template.Model.Results;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Security;

namespace Giusti.Template.Web.Library
{
    public abstract partial class ApiBase : ApiController
    {
        #region Return Ok for DELETE

        protected HttpResponseMessage RetornaMensagemOk(ServiceResult resultado)
        {
            var json = JsonConvert.SerializeObject(resultado);
            HttpResponseMessage response = this.Request.CreateResponse(HttpStatusCode.OK, json);
            return response;
        }

        #endregion

        #region Return Error

        #region Return Error for PUT, POST, DELETE

        protected HttpResponseMessage RetornaMensagemErro(HttpStatusCode statusCode, ServiceResult resultado)
        {
            var json = JsonConvert.SerializeObject(resultado);
            HttpResponseMessage response = this.Request.CreateResponse(statusCode, json);
            return response;
        }

        protected HttpResponseMessage RetornaMensagemErro(HttpStatusCode statusCode, Exception ex)
        {
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.Success = false;
            serviceResult.Messages.Add(new ServiceResultMessage() { Message = GetExceptionMessages(ex) });

            var json = JsonConvert.SerializeObject(serviceResult);
            HttpResponseMessage response = this.Request.CreateResponse(statusCode, json);
            return response;
        }

        #endregion

        #region Return Error for GET

        protected void GeraErro(HttpStatusCode statusCode, Exception ex)
        {
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.Success = false;
            serviceResult.Messages.Add(new ServiceResultMessage() { Message = GetExceptionMessages(ex) });

            var json = JsonConvert.SerializeObject(serviceResult);
            HttpResponseMessage response = this.Request.CreateErrorResponse(statusCode, json);
            throw new HttpResponseException(response);
        }

        protected void GeraErro(HttpStatusCode statusCode, ServiceResult resultado)
        {
            var json = JsonConvert.SerializeObject(resultado);
            HttpResponseMessage response = this.Request.CreateErrorResponse(statusCode, json);
            throw new HttpResponseException(response);
        }

        #endregion

        private string GetExceptionMessages(Exception e, string msgs = "")
        {
            if (e == null) return string.Empty;
            if (msgs == "") msgs = e.Message;
            if (e.InnerException != null)
                msgs += "\r\nInnerException: " + GetExceptionMessages(e.InnerException);
            return msgs;
        }

        #endregion

        #region Authentication

        protected void VerificaAutenticacao(string codigoFuncionalidade, string funcionalidade, BusinessBase biz)
        {
            biz.VerificaAutenticacao(RetornaToken(), codigoFuncionalidade, funcionalidade);
            if (!biz.IsValid())
                throw new UnauthorizedAccessException();
        }
        protected string RetornaToken()
        {
            string token = string.Empty;
            if (Request.Headers.Authorization != null)
                token = Request.Headers.Authorization.Parameter;

            return token;
        }
        private FormsAuthenticationTicket RetornaTokenDescriptografado(string token)
        {
            FormsAuthenticationTicket cookie = FormsAuthentication.Decrypt(token);
            return cookie;
        }
        public string RetornaEmailAutenticado()
        {
            string token = RetornaToken();
            if (string.IsNullOrEmpty(token))
                return string.Empty;

            return RetornaTokenDescriptografado(token).Name;
        }
        protected string[] RetornaFuncionalidadesUsuario()
        {
            string token = RetornaToken();
            if (string.IsNullOrEmpty(token))
                return null;

            FormsAuthenticationTicket cookie = FormsAuthentication.Decrypt(token);

            string userData = cookie.UserData;
            string[] roles = userData.Split(',');

            return roles;
        }

        #endregion

        #region Log

        /// <summary>
        /// GravaLog
        /// </summary>
        /// <param name="tipoAcao"></param>
        /// <param name="emailAutenticado"></param>
        protected void GravaLog(EnumTipoAcao tipoAcao, string emailAutenticado)
        {
            GravaLog(tipoAcao, emailAutenticado, null, null);
        }

        /// <summary>
        /// GravaLog
        /// </summary>
        /// <param name="tipoAcao"></param>
        /// <param name="emailAutenticado"></param>
        /// <param name="funcionalidadeId"></param>
        protected void GravaLog(EnumTipoAcao tipoAcao, string emailAutenticado, int? funcionalidadeId)
        {
            GravaLog(tipoAcao, emailAutenticado, funcionalidadeId, null);
        }

        /// <summary>
        /// GravaLog
        /// </summary>
        /// <param name="tipoAcao"></param>
        /// <param name="emailAutenticado"></param>
        /// <param name="funcionalidadeId"></param>
        /// <param name="registroId"></param>
        protected void GravaLog(EnumTipoAcao tipoAcao, string emailAutenticado, int? funcionalidadeId, int? registroId)
        {
            try
            {
                LogBusiness biz = new LogBusiness();
                string ipMaquina = string.Empty;
                string nomeMaquina = Dns.GetHostName();
                IPAddress[] ip = Dns.GetHostAddresses(nomeMaquina);
                ipMaquina = ip[1].ToString();

                UsuarioBusiness bizUsuario = new UsuarioBusiness();
                Usuario usuario = bizUsuario.RetornaUsuario_Email(emailAutenticado);

                biz.SalvaLog(new Log() { Acao = tipoAcao.ToString(), FuncionalidadeId = funcionalidadeId, DataInclusao = DateTime.Now, OrigemAcesso = nomeMaquina, RegistroId = registroId, IpMaquina = ipMaquina, UsuarioId = usuario.Id });
            }
            catch
            {
                //vazio, pois o erro de gravação de log não pode interromper o processamento.
            }
        }

        #endregion
    }
}