using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Giusti.Template.Model.Results;
using Giusti.Template.Business.Library;
using Giusti.Template.Data;
using Giusti.Template.Model;
using System.Web.Security;

namespace Giusti.Template.Business
{
    public class CommonBusiness : BusinessBase
    {
        #region Log

        private void ValidaRegrasNegocioLog(Log itemGravar)
        {
        }
        public void SalvaLog(Log itemGravar)
        {
            ValidateService(itemGravar);
            ValidaRegrasNegocioLog(itemGravar);
            if (IsValid())
            {
                using (ServiceData data = new ServiceData())
                {
                    data.SalvaLog(itemGravar);
                }
            }
        }

        #endregion

        #region Autenticacao

        private string GeraToken(string email, string acesso)
        {
            try
            {
                FormsAuthenticationTicket authTicket =
                  new FormsAuthenticationTicket(1, email, DateTime.Now, DateTime.Now.AddMinutes(60), false, acesso);

                string ticketCriptografado = FormsAuthentication.Encrypt(authTicket);
                return ticketCriptografado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public void VerificaAutenticacao(string token, string codigoFuncionalidade, string funcionalidade)
        {
            if (string.IsNullOrEmpty(token))
                IncluiErroBusiness("Autenticacao_NecessarioAutenticacao");
            else
            {
                FormsAuthenticationTicket cookie = FormsAuthentication.Decrypt(token);

                if (cookie.Expired)
                    IncluiErroBusiness("Autenticacao_LoginExpirado");

                string userData = cookie.UserData;
                string[] roles = userData.Split(',');

                if (!roles.Any(a => a == codigoFuncionalidade))
                    IncluiErroBusiness(string.Format(MensagemBusiness.RetornaMensagens("Autenticacao_AcessoNegado"), cookie.Name, funcionalidade), true);
            }
        }

        #endregion
    }
}
