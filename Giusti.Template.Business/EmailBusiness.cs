using System.Collections.Generic;
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
    public class EmailBusiness : BusinessBase
    {
        public void GeraEmailEsqueciSenha(Usuario itemGravar, string novaSenha)
        {
            Email email = new Email();
            email.Assunto = Constantes.AssuntoEmailEsqueciSenha;
            email.Corpo = string.Format(Constantes.CorpoEmailEsqueciSenha, itemGravar.Nome, novaSenha);
            email.DataInclusao = DateTime.Now;
            email.FuncionalidadeId = Convert.ToInt32(Constantes.FuncionalidadeAlterarSenha);
            email.Destinatario = itemGravar.Email;
            SalvaEmail(email);
        }

        public void SalvaEmail(Email itemGravar)
        {
            LimpaValidacao();
            ValidateService(itemGravar);
            ValidrRegrasNegocioSalvar(itemGravar);
            if (IsValid())
            {
                using (EmailData data = new EmailData())
                {
                    data.SalvaEmail(itemGravar);
                    IncluiSucessoBusiness("Email_SalvaEmailOK");
                }
            }
        }

        private void ValidrRegrasNegocioSalvar(Email itemGravar)
        {
        }
    }
}
