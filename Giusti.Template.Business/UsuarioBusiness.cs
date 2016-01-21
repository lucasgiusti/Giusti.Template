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
    public class UsuarioBusiness : BusinessBase
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
    }
}
