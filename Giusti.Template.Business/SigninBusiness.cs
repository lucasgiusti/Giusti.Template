﻿using System.Collections.Generic;
using System.Linq;
using Giusti.Template.Model.Results;
using Giusti.Template.Business.Library;
using Giusti.Template.Data;
using Giusti.Template.Model;
using System;
using Giusti.Template.Model.Dominio;
using System.Web.Security;

namespace Giusti.Template.Business
{
    public class SigninBusiness : BusinessBase
    {
        public UsuarioLogado EfetuaLoginSistema(string email, string senha, string ip, string nomeMaquina)
        {
            LimpaValidacao();
            if (string.IsNullOrEmpty(email))
            {
                IncluiErro("Usuario_Email");
            }
            if (string.IsNullOrEmpty(senha))
            {
                IncluiErro("Usuario_Senha");
            }

            UsuarioLogado retorno = null;
            if (IsValid())
            {
                UsuarioBusiness bizUsuario = new UsuarioBusiness();
                Usuario usuario = bizUsuario.RetornaUsuario_Email(email);

                if (usuario == null)
                {
                    IncluiErro("Usuario_EmailInvalido");
                }

                if (IsValid() && !PasswordHash.ValidatePassword(senha, usuario.Senha))
                {
                    IncluiErro("Usuario_SenhaInvalida");
                }

                if (IsValid())
                {
                    retorno = new UsuarioLogado();
                    retorno.Id = usuario.Id.Value;
                    retorno.DataHoraAcesso = DateTime.Now;
                    retorno.Email = usuario.Email;
                    retorno.Nome = usuario.Nome;
                    retorno.WorkstationId = nomeMaquina;

                    FuncionalidadeBusiness bizFuncionalidade = new FuncionalidadeBusiness();
                    IList<string> listFuncionalidade = bizFuncionalidade.RetornaFuncionalidades_UsuarioId((int)usuario.Id);

                    retorno.Token = GeraToken(email, string.Join(",", listFuncionalidade));
                }

            }
            return retorno;
        }

        private string GeraToken(string email, string funcionalidades)
        {
            try
            {
                FormsAuthenticationTicket authTicket =
                                        new FormsAuthenticationTicket(1, email, DateTime.Now, DateTime.Now.AddMinutes(60), false, funcionalidades);

                string ticketCriptografado = FormsAuthentication.Encrypt(authTicket);
                return ticketCriptografado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}