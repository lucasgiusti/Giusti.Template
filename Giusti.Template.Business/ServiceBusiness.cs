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
    public class ServiceBusiness : BusinessBase
    {
        #region Usuario

        public Usuario RetornaUsuario_Email(string email)
        {
            LimpaValidacao();
            Usuario RetornoAcao = null;
            if (IsValid())
            {
                using (ServiceData data = new ServiceData())
                {
                    RetornoAcao = data.RetornaUsuario_Email(email);
                }
            }
            return RetornoAcao;
        }
        public IList<Usuario> RetornaUsuarios()
        {
            LimpaValidacao();
            IList<Usuario> RetornoAcao = new List<Usuario>();
            if (IsValid())
            {
                using (ServiceData data = new ServiceData())
                {
                    RetornoAcao = data.RetornaUsuarios();
                }
            }
            RetornoAcao.ToList().ForEach(a => a.Senha = null);

            return RetornoAcao;
        }

        #endregion

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
                AdicionaErroBusiness("Autenticacao_NecessarioAutenticacao");
            else
            {
                FormsAuthenticationTicket cookie = FormsAuthentication.Decrypt(token);

                if (cookie.Expired)
                    AdicionaErroBusiness("Autenticacao_LoginExpirado");

                string userData = cookie.UserData;
                string[] roles = userData.Split(',');

                if (!roles.Any(a => a == codigoFuncionalidade))
                    AdicionaErroBusiness(string.Format(MensagemBusiness.RetornaMensagens("Autenticacao_AcessoNegado"), cookie.Name, funcionalidade), true);
            }
        }

        #endregion

        #region ServiceResult

        private void AdicionaErroBusiness(string CodigoMensagemErro)
        {
            AdicionaErroBusiness(CodigoMensagemErro, false);
        }
        private void AdicionaErroBusiness(string CodigoMensagemErro, bool mensagemPersonalizada)
        {
            if (mensagemPersonalizada)
                AdicionaMensagemErroBusiness(CodigoMensagemErro);
            else
                AdicionaMensagemErroBusiness(MensagemBusiness.RetornaMensagens(CodigoMensagemErro));
        }
        private void AdicionaMensagemErroBusiness(string mensagemErro)
        {
            ServiceResult resultado = new ServiceResult();
            resultado.Success = false;
            resultado.Messages.Add(mensagemErro);
        }

        #endregion
    }
}
