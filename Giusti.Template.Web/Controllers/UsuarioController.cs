using Giusti.Template.Business;
using Giusti.Template.Model;
using Giusti.Template.Model.Dominio;
using Giusti.Template.Web.Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

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
                GeraErro(HttpStatusCode.Unauthorized, bizCommon.serviceResult);
            }
            catch (Exception ex)
            {
                GeraErro(HttpStatusCode.BadRequest, ex);
            }

            return ResultadoBusca;
        }
    }
}