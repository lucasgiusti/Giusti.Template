﻿using System.Collections.Generic;
using System.Linq;
using Giusti.Template.Model.Results;
using Giusti.Template.Business.Library;
using Giusti.Template.Data;
using Giusti.Template.Model;

namespace Giusti.Template.Business
{
    public class PerfilFuncionalidadeBusiness : BusinessBase
    {
        public PerfilFuncionalidade RetornaPerfilFuncionalidade_Id(int id)
        {
            LimpaValidacao();
            PerfilFuncionalidade RetornoAcao = null;
            if (IsValid())
            {
                using (PerfilFuncionalidadeData data = new PerfilFuncionalidadeData())
                {
                    RetornoAcao = data.RetornaPerfilFuncionalidade_Id(id);
                }
            }

            return RetornoAcao;
        }
        public IList<PerfilFuncionalidade> RetornaPerfilFuncionalidades()
        {
            LimpaValidacao();
            IList<PerfilFuncionalidade> RetornoAcao = new List<PerfilFuncionalidade>();
            if (IsValid())
            {
                using (PerfilFuncionalidadeData data = new PerfilFuncionalidadeData())
                {
                    RetornoAcao = data.RetornaPerfilFuncionalidades();
                }
            }

            return RetornoAcao;
        }
        public IList<PerfilFuncionalidade> RetornaPerfilFuncionalidades_PerfilId_FuncionalidadeId(int? perfilId, int? funcionalidadeId)
        {
            LimpaValidacao();
            IList<PerfilFuncionalidade> RetornoAcao = new List<PerfilFuncionalidade>();
            if (IsValid())
            {
                using (PerfilFuncionalidadeData data = new PerfilFuncionalidadeData())
                {
                    RetornoAcao = data.RetornaPerfilFuncionalidades_PerfilId_FuncionalidadeId(perfilId, funcionalidadeId);
                }
            }

            return RetornoAcao;
        }
        public void SalvaPerfilFuncionalidade(PerfilFuncionalidade itemGravar)
        {
            LimpaValidacao();
            ValidateService(itemGravar);
            ValidaRegrasSalvar(itemGravar);
            if (IsValid())
            {
                using (PerfilFuncionalidadeData data = new PerfilFuncionalidadeData())
                {
                    data.SalvaPerfilFuncionalidade(itemGravar);
                    serviceResult = new ServiceResult();
                    serviceResult.Success = true;
                    serviceResult.Messages.Add(new ServiceResultMessage() { Message = MensagemBusiness.RetornaMensagens("PerfilFuncionalidade_SalvaAcessoOK") });
                }
            }
        }
        public void ExcluiPerfilFuncionalidade(PerfilFuncionalidade itemGravar)
        {
            LimpaValidacao();
            ValidateService(itemGravar);
            ValidaRegrasExcluir(itemGravar);
            if (IsValid())
            {
                using (PerfilFuncionalidadeData data = new PerfilFuncionalidadeData())
                {
                    data.ExcluiPerfilFuncionalidade(itemGravar);
                    serviceResult = new ServiceResult();
                    serviceResult.Success = true;
                    serviceResult.Messages.Add(new ServiceResultMessage() { Message = MensagemBusiness.RetornaMensagens("PerfilFuncionalidade_ExcluiAcessoOK") });
                }
            }
        }

        public void ValidaRegrasSalvar(PerfilFuncionalidade itemGravar)
        {

        }
        public void ValidaRegrasExcluir(PerfilFuncionalidade itemGravar)
        {
            ValidaExistencia(itemGravar);
        }
        public void ValidaExistencia(PerfilFuncionalidade itemGravar)
        {
            if (itemGravar == null)
            {
                serviceResult = new ServiceResult();
                serviceResult.Success = false;
                serviceResult.Messages.Add(new ServiceResultMessage() { Message = MensagemBusiness.RetornaMensagens("PerfilFuncionalidade_NaoEncontrado") });
            }
        }
    }
}
