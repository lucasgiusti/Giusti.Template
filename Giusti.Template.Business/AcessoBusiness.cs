using System.Collections.Generic;
using System.Linq;
using Giusti.Template.Model.Results;
using Giusti.Template.Business.Library;
using Giusti.Template.Data;
using Giusti.Template.Model;

namespace Giusti.Template.Business
{
    public class AcessoBusiness : BusinessBase
    {
        public Acesso RetornaAcesso_Id(int id)
        {
            LimpaValidacao();
            Acesso RetornoAcao = null;
            if (IsValid())
            {
                using (AcessoData data = new AcessoData())
                {
                    RetornoAcao = data.RetornaAcesso_Id(id);
                }
            }

            return RetornoAcao;
        }
        public IList<Acesso> RetornaAcessos()
        {
            LimpaValidacao();
            IList<Acesso> RetornoAcao = new List<Acesso>();
            if (IsValid())
            {
                using (AcessoData data = new AcessoData())
                {
                    RetornoAcao = data.RetornaAcessos();
                }
            }

            return RetornoAcao;
        }
        public IList<Acesso> RetornaAcessos_PerfilId_FuncionalidadeId(int? perfilId, int? funcionalidadeId)
        {
            LimpaValidacao();
            IList<Acesso> RetornoAcao = new List<Acesso>();
            if (IsValid())
            {
                using (AcessoData data = new AcessoData())
                {
                    RetornoAcao = data.RetornaAcessos_PerfilId_FuncionalidadeId(perfilId, funcionalidadeId);
                }
            }

            return RetornoAcao;
        }
        public void SalvaAcesso(Acesso itemGravar)
        {
            LimpaValidacao();
            ValidateService(itemGravar);
            ValidaRegrasSalvar(itemGravar);
            if (IsValid())
            {
                using (AcessoData data = new AcessoData())
                {
                    data.SalvaAcesso(itemGravar);
                    serviceResult = new ServiceResult();
                    serviceResult.Success = true;
                    serviceResult.Messages.Add(new ServiceResultMessage() { Message = MensagemBusiness.RetornaMensagens("Acesso_SalvaAcessoOK") });
                }
            }
        }
        public void ExcluiAcesso(Acesso itemGravar)
        {
            LimpaValidacao();
            ValidateService(itemGravar);
            ValidaRegrasExcluir(itemGravar);
            if (IsValid())
            {
                using (AcessoData data = new AcessoData())
                {
                    data.ExcluiAcesso(itemGravar);
                    serviceResult = new ServiceResult();
                    serviceResult.Success = true;
                    serviceResult.Messages.Add(new ServiceResultMessage() { Message = MensagemBusiness.RetornaMensagens("Acesso_ExcluiAcessoOK") });
                }
            }
        }

        public void ValidaRegrasSalvar(Acesso itemGravar)
        {

        }
        public void ValidaRegrasExcluir(Acesso itemGravar)
        {
            ValidaExistencia(itemGravar);
        }
        public void ValidaExistencia(Acesso itemGravar)
        {
            if (itemGravar == null)
            {
                serviceResult = new ServiceResult();
                serviceResult.Success = false;
                serviceResult.Messages.Add(new ServiceResultMessage() { Message = MensagemBusiness.RetornaMensagens("Acesso_NaoEncontrado") });
            }
        }
    }
}
