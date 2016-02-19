using System;
using System.Collections.Generic;

namespace Giusti.Template.Model
{
    public class Usuario
    {
        public Usuario()
        {
        }

        public int? Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int? Situacao { get; set; }
        public DateTime? DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public IList<PerfilUsuario> Perfis { get; set; }
        public Usuario Clone()
        {
            return (Usuario)this.MemberwiseClone();
        }
    }
}
