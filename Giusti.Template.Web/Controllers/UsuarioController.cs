using Giusti.Template.Business;
using Giusti.Template.Model;
using Giusti.Template.Model.Dominio;
using Giusti.Template.Web.Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Giusti.Template.Web.Controllers
{
    /// <summary>
    /// Usuário
    /// </summary>
    public class UsuarioController: ApiBase
    {
        UsuarioBusiness biz = new UsuarioBusiness();
        
        /// <summary>
        /// Retorna todos os usuários
        /// </summary>
        /// <returns></returns>
        public List<Usuario> Get()
        {
            bizBase = biz;

            List<Usuario> ResultadoBusca = new List<Usuario>();
            try
            {
                VerificaAutenticacao(Funcionalidade.UsuarioConsulta, Funcionalidade.NomeUsuarioConsulta);

                //API
                ResultadoBusca = new List<Usuario>(biz.RetornaUsuarios());
                
                if (!biz.IsValid())
                    throw new InvalidDataException();
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

            return ResultadoBusca;
        }

        /// <summary>
        /// Retorna o usuário com id solicidado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Usuario Get(int id)
        {
            Usuario ResultadoBusca = new Usuario();
            try
            {
                VerificaAutenticacao(Funcionalidade.UsuarioConsulta, Funcionalidade.NomeUsuarioConsulta);

                //API
                ResultadoBusca = biz.RetornaUsuario_Id(id);

                if (!biz.IsValid())
                    throw new InvalidDataException();
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

            return ResultadoBusca;
        }

        /// <summary>
        /// Inclui um usuário
        /// </summary>
        /// <param name="itemSalvar"></param>
        /// <returns></returns>
        public int? Post([FromBody]Usuario itemSalvar)
        {
            try
            {
                //API
                biz.SalvaUsuario(itemSalvar);
                if (!biz.IsValid())
                    throw new InvalidDataException();

                GravaLog(enumTipoAcao.Incluir, Funcionalidade.NomeUsuarioInclusao, itemSalvar.Id);
            }
            catch (InvalidDataException)
            {
                GeraErro(HttpStatusCode.InternalServerError, biz.serviceResult);
            }
            catch (Exception ex)
            {
                GeraErro(HttpStatusCode.BadRequest, ex);
            }

            return itemSalvar.Id;
        }

        /// <summary>
        /// Altera um determinado usuário
        /// </summary>
        /// <param name="id"></param>
        /// <param name="itemSalvar"></param>
        /// <returns></returns>
        public Usuario Put(int id, [FromBody]Usuario itemSalvar)
        {
            try
            {
                VerificaAutenticacao(Funcionalidade.UsuarioEdicao, Funcionalidade.NomeUsuarioEdicao);

                //API
                itemSalvar.Id = id;
                biz.SalvaUsuario(itemSalvar);

                if (!biz.IsValid())
                    throw new InvalidDataException();

                GravaLog(enumTipoAcao.Alterar, Funcionalidade.NomeUsuarioEdicao, itemSalvar.Id);
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

        /// <summary>
        /// Exclui um determinado usuário
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HttpResponseMessage Delete(int id)
        {
            HttpResponseMessage retorno = null;
            try
            {
                VerificaAutenticacao(Funcionalidade.UsuarioExclusao, Funcionalidade.NomeUsuarioExclusao);

                //API
                Usuario itemExcluir = biz.RetornaUsuario_Id(id);
                biz.ExcluiUsuario(itemExcluir);
                if (!biz.IsValid())
                    throw new InvalidDataException();

                retorno = RetornaMensagemOk(biz.serviceResult);

                GravaLog(enumTipoAcao.Excluir, Funcionalidade.NomeUsuarioExclusao, id);
            }
            catch (InvalidDataException)
            {
                retorno = RetornaMensagemErro(HttpStatusCode.InternalServerError, biz.serviceResult);
            }
            catch (UnauthorizedAccessException)
            {
                retorno = RetornaMensagemErro(HttpStatusCode.Unauthorized, biz.serviceResult);
            }
            catch (Exception ex)
            {
                retorno = RetornaMensagemErro(HttpStatusCode.BadRequest, ex);
            }

            return retorno;
        }
    }
}