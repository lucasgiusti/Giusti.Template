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

    public partial class PerfilFuncionalidade
    {
        public PerfilFuncionalidade()
        {
        }
        public int? Id { get; set; }
        public int? PerfilId { get; set; }
        public int? FuncionalidadeId { get; set; }
    }
}
