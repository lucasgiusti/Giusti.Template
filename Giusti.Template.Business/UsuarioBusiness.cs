using System.Collections.Generic;
using System.Linq;
using Giusti.Template.Model.Results;
using Giusti.Template.Business.Library;
using Giusti.Template.Data;
using Giusti.Template.Model;
using System;
using Giusti.Template.Model.Dominio;

namespace Giusti.Template.Business
{
    public class UsuarioBusiness : BusinessBase
    {
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
            if (IsValid() && string.IsNullOrWhiteSpace(itemGravar.Nome))
            {
                serviceResult.Success = false;
                serviceResult.Messages.Add(new ServiceResultMessage() { Message = MensagemBusiness.RetornaMensagens("Usuario_Nome") });
            }
            if (IsValid() && string.IsNullOrWhiteSpace(itemGravar.Email))
            {
                serviceResult.Success = false;
                serviceResult.Messages.Add(new ServiceResultMessage() { Message = MensagemBusiness.RetornaMensagens("Usuario_Email") });
            }
            if (IsValid())
            {
                Usuario itemBase = RetornaUsuario_Email(itemGravar.Email);
                if (itemBase != null && itemGravar.Id != itemBase.Id)
                {
                    serviceResult.Success = false;
                    serviceResult.Messages.Add(new ServiceResultMessage() { Message = MensagemBusiness.RetornaMensagens("Usuario_CadastroDuplicado") });
                }
            }
            if (IsValid())
            {
                if (itemGravar.Id.HasValue)
                {
                    Usuario itemBase = RetornaUsuario_Id((int)itemGravar.Id);
                    ValidaExistencia(itemBase);
                    if (IsValid())
                    {
                        itemGravar.DataInclusao = itemBase.DataInclusao;
                        itemGravar.DataAlteracao = DateTime.Now;

                        if (string.IsNullOrWhiteSpace(itemGravar.Senha) && string.IsNullOrWhiteSpace(itemGravar.SenhaConfirmacao))
                            itemGravar.Senha = itemBase.Senha;
                        else
                        {
                            if (string.IsNullOrWhiteSpace(itemGravar.Senha))
                            {
                                serviceResult.Success = false;
                                serviceResult.Messages.Add(new ServiceResultMessage() { Message = MensagemBusiness.RetornaMensagens("Usuario_Senha") });
                            }
                            if (IsValid() && string.IsNullOrWhiteSpace(itemGravar.SenhaConfirmacao))
                            {
                                serviceResult.Success = false;
                                serviceResult.Messages.Add(new ServiceResultMessage() { Message = MensagemBusiness.RetornaMensagens("Usuario_SenhaConfirmacao") });
                            }
                            if (IsValid() && !itemGravar.Senha.Equals(itemGravar.SenhaConfirmacao))
                            {
                                serviceResult.Success = false;
                                serviceResult.Messages.Add(new ServiceResultMessage() { Message = MensagemBusiness.RetornaMensagens("Usuario_SenhaConfirmacao_Incorreta") });
                            }
                            if (IsValid())
                            {
                                itemGravar.Senha = PasswordHash.HashPassword(itemGravar.Senha);
                            }
                        }
                    }
                }
                else
                {
                    itemGravar.DataInclusao = DateTime.Now;
                    itemGravar.Ativo = true;

                    if (string.IsNullOrWhiteSpace(itemGravar.Senha))
                    {
                        serviceResult.Success = false;
                        serviceResult.Messages.Add(new ServiceResultMessage() { Message = MensagemBusiness.RetornaMensagens("Usuario_Senha") });
                    }
                    if (IsValid() && string.IsNullOrWhiteSpace(itemGravar.SenhaConfirmacao))
                    {
                        serviceResult.Success = false;
                        serviceResult.Messages.Add(new ServiceResultMessage() { Message = MensagemBusiness.RetornaMensagens("Usuario_SenhaConfirmacao") });
                    }
                    if (IsValid() && !itemGravar.Senha.Equals(itemGravar.SenhaConfirmacao))
                    {
                        serviceResult.Success = false;
                        serviceResult.Messages.Add(new ServiceResultMessage() { Message = MensagemBusiness.RetornaMensagens("Usuario_SenhaConfirmacao_Incorreta") });
                    }
                    if (IsValid())
                    {
                        itemGravar.Senha = PasswordHash.HashPassword(itemGravar.Senha);
                    }
                }
            }


        }
        public void ValidaRegrasExcluir(Usuario itemGravar)
        {
            if (IsValid())
                ValidaExistencia(itemGravar);

            if (IsValid())
            {
                PerfilUsuarioBusiness biz = new PerfilUsuarioBusiness();
                var PerfisAssociados = biz.RetornaPerfilUsuarios_PerfilId_UsuarioId(null, itemGravar.Id);

                if (PerfisAssociados.Count > 0)
                {
                    serviceResult.Success = false;
                    serviceResult.Messages.Add(new ServiceResultMessage() { Message = MensagemBusiness.RetornaMensagens("Usuario_CadastroUtilizado") });
                }
            }

            if (IsValid() && ExisteLog_UsuarioId((int)itemGravar.Id))
            {
                serviceResult.Success = false;
                serviceResult.Messages.Add(new ServiceResultMessage() { Message = MensagemBusiness.RetornaMensagens("Usuario_CadastroUtilizado") });
            }

        }
        public void ValidaExistencia(Usuario itemGravar)
        {
            if (itemGravar == null)
            {
                serviceResult.Success = false;
                serviceResult.Messages.Add(new ServiceResultMessage() { Message = MensagemBusiness.RetornaMensagens("Usuario_NaoEncontrado") });
            }
        }
    }
}
