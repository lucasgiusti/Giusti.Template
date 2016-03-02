using System;
using System.Collections.Generic;
using System.Web.Http;
using System.IO;
using Giusti.Template.Web.Library;
using System.Net.Http;
using System.Net;
using Giusti.Template.Model;
using Giusti.Template.Model.Dominio;
using System.Web;
using Giusti.Template.Business;

namespace Giusti.Template.Web.Controllers.Api
{
    /// <summary>
    /// AlterarSenha
    /// </summary>
    public class AlterarSenhaController : ApiBase
    {
        UsuarioBusiness biz = new UsuarioBusiness();

        /// <summary>
        /// Altera a senha de um determinado usuário
        /// </summary>
        /// <param name="id"></param>
        /// <param name="itemSalvar"></param>
        /// <returns></returns>
        public Usuario Put(int id, [FromBody]Usuario itemSalvar)
        {
            try
            {
                VerificaAutenticacao(Constantes.FuncionalidadeAlterarSenha, Constantes.FuncionalidadeNomeAlterarSenha, biz);

                //API
                itemSalvar.Id = id;
                biz.AlteraSenha(itemSalvar);

                if (!biz.IsValid())
                    throw new InvalidDataException();

                if (itemSalvar != null)
                {
                    itemSalvar.Senha = null;
                    itemSalvar.SenhaConfirmacao = null;
                }

                GravaLog(EnumTipoAcao.Incluir, RetornaEmailAutenticado(), Convert.ToInt32(Constantes.FuncionalidadeAlterarSenha), itemSalvar.Id);
            }
            catch (InvalidDataException)
            {
                GeraErro(HttpStatusCode.InternalServerError, biz.serviceResult);
            }
            catch (UnauthorizedAccessException)
            {
                GeraErro(HttpStatusCode.Unauthorized, biz.serviceResult);
            }
            catch (Exception ex)
            {
                GeraErro(HttpStatusCode.BadRequest, ex);
            }

            return itemSalvar;
        }
    }
}