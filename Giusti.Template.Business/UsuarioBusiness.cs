﻿using System.Collections.Generic;
using System.Linq;
using Giusti.Template.Model.Results;
using Giusti.Template.Business.Library;
using Giusti.Template.Data;
using Giusti.Template.Model;

namespace Giusti.Template.Business
{
    public class UsuarioBusiness : BusinessBase
    {
        #region Usuario

        public Usuario RetornaUsuario_Id(int id)
        {
            LimpaValidacao();
            Usuario RetornoAcao = null;
            if (IsValid())
            {
                using (UsuarioData data = new UsuarioData())
                {
                    RetornoAcao = data.RetornaUsuario_Id(id);
                }
            }
            if (RetornoAcao != null)
                RetornoAcao.Senha = null;

            return RetornoAcao;
        }
        public Usuario RetornaUsuario_Email(string email)
        {
            LimpaValidacao();
            Usuario RetornoAcao = null;
            if (IsValid())
            {
                using (UsuarioData data = new UsuarioData())
                {
                    RetornoAcao = data.RetornaUsuario_Email(email);
                }
            }
            if (RetornoAcao != null)
                RetornoAcao.Senha = null;

            return RetornoAcao;
        }
        public IList<Usuario> RetornaUsuarios()
        {
            LimpaValidacao();
            IList<Usuario> RetornoAcao = new List<Usuario>();
            if (IsValid())
            {
                using (UsuarioData data = new UsuarioData())
                {
                    RetornoAcao = data.RetornaUsuarios();
                }
            }
            RetornoAcao.ToList().ForEach(a => a.Senha = null);

            return RetornoAcao;
        }
        public void SalvaUsuario(Usuario itemGravar)
        {
            LimpaValidacao();
            ValidateService(itemGravar);
            ValidaRegrasSalvar(itemGravar);
            if (IsValid())
            {
                using (UsuarioData data = new UsuarioData())
                {
                    data.SalvaUsuario(itemGravar);
                    serviceResult = new ServiceResult();
                    serviceResult.Success = true;
                    serviceResult.Messages.Add(new ServiceResultMessage() { Message = MensagemBusiness.RetornaMensagens("Usuario_SalvaUsuarioOK") });
                }
            }
        }
        public void ExcluiUsuario(Usuario itemGravar)
        {
            LimpaValidacao();
            ValidateService(itemGravar);
            ValidaRegrasExcluir(itemGravar);
            if (IsValid())
            {
                using (UsuarioData data = new UsuarioData())
                {
                    data.ExcluiUsuario(itemGravar);
                    serviceResult = new ServiceResult();
                    serviceResult.Success = true;
                    serviceResult.Messages.Add(new ServiceResultMessage() { Message = MensagemBusiness.RetornaMensagens("Usuario_ExcluiUsuarioOK") });
                }
            }
        }

        public void ValidaRegrasSalvar(Usuario itemGravar)
        {

        }
        public void ValidaRegrasExcluir(Usuario itemGravar)
        {
            ValidaExistencia(itemGravar);
        }
        public void ValidaExistencia(Usuario itemGravar)
        {
            if (itemGravar == null)
            {
                serviceResult = new ServiceResult();
                serviceResult.Success = false;
                serviceResult.Messages.Add(new ServiceResultMessage() { Message = MensagemBusiness.RetornaMensagens("Usuario_NaoEncontrado") });
            }
        }

        #endregion
    }
}
