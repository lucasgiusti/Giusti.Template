using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Resources;
using System.Reflection;
using Giusti.Template.Model.Resource;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Giusti.Template.Model
{
    [HasSelfValidation()]
    public partial class Perfil
    {
        public Perfil()
        {
        }
        public int? Id { get; set; }

        public string Nome { get; set; }
        [SelfValidation]
        private void ValidarNome(Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResults results)
        {
            if (Nome == null)
            {
                Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult result =
                      new Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult(Resource.Mensagem.Perfil_Nome, this, "Nome", null, null);
                results.AddResult(result);
            }
            else if (Nome.Length > 50)
            {
                Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult result =
                      new Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult(Resource.Mensagem.Perfil_NomeTamanho, this, "Nome", null, null);
                results.AddResult(result);
            }
        }
        public IList<PerfilFuncionalidade> PerfilFuncionalidades { get; set; }
    }
}
