using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giusti.Template.Model
{
    [HasSelfValidation()]
    public partial class Log
    {
        public Log()
        {
        }

        public int? Id { get; set; }

        public int? UsuarioId { get; set; }

        public int? RegistroId { get; set; }

        public string Funcionalidade { get; set; }
        [SelfValidation]
        private void ValidarFuncionalidade(ValidationResults results)
        {
            if (Funcionalidade == null)
            {
                ValidationResult result =
                      new ValidationResult(Resource.MensagemModelo.Log_Funcionalidade, this, "Funcionalidade", null, null);
                results.AddResult(result);
            }
            else if (Funcionalidade.Length > 50)
            {
                ValidationResult result =
                      new ValidationResult(Resource.MensagemModelo.Log_Funcionalidade_Tamanho, this, "Funcionalidade", null, null);
                results.AddResult(result);
            }
        }

        public string Acao { get; set; }
        [SelfValidation]
        private void ValidarAcao(ValidationResults results)
        {
            if (Acao == null)
            {
                ValidationResult result =
                      new ValidationResult(Resource.MensagemModelo.Log_Acao, this, "Acao", null, null);
                results.AddResult(result);
            }
            else if (Acao.Length > 20)
            {
                ValidationResult result =
                      new ValidationResult(Resource.MensagemModelo.Log_Acao_Tamanho, this, "Acao", null, null);
                results.AddResult(result);
            }
        }

        public string OrigemAcesso { get; set; }
        [SelfValidation]
        private void ValidarOrigemAcesso(ValidationResults results)
        {
            if (OrigemAcesso == null)
            {
                ValidationResult result =
                      new ValidationResult(Resource.MensagemModelo.Log_OrigemAcesso, this, "OrigemAcesso", null, null);
                results.AddResult(result);
            }
            else if (OrigemAcesso.Length > 250)
            {
                ValidationResult result =
                      new ValidationResult(Resource.MensagemModelo.Log_OrigemAcesso_Tamanho, this, "OrigemAcesso", null, null);
                results.AddResult(result);
            }
        }

        public string IpMaquina { get; set; }
        [SelfValidation]
        private void ValidarIpMaquina(ValidationResults results)
        {
            if (IpMaquina == null)
            {
                ValidationResult result =
                      new ValidationResult(Resource.MensagemModelo.Log_IpMaquina, this, "IpMaquina", null, null);
                results.AddResult(result);
            }
            else if (IpMaquina.Length > 50)
            {
                ValidationResult result =
                      new ValidationResult(Resource.MensagemModelo.Log_IpMaquina_Tamanho, this, "IpMaquina", null, null);
                results.AddResult(result);
            }
        }

        public Usuario Usuario { get; set; }

        public DateTime? DataInclusao { get; set; }
        public Log Clone()
        {
            return (Log)this.MemberwiseClone();
        }
    }
}
