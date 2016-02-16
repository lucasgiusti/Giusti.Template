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

    public partial class Perfil
    {
        public Perfil()
        {
        }
        public int? Id { get; set; }
        public string Nome { get; set; }
        public IList<PerfilFuncionalidade> PerfilFuncionalidades { get; set; }
    }
}
