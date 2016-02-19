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
    /// Perfil
    /// </summary>
    public class FuncionalidadeController : ApiBase
    {
        FuncionalidadeBusiness biz = new FuncionalidadeBusiness();

        /// <summary>
        /// Retorna todas as funcionalidades
        /// </summary>
        /// <returns></returns>
        public List<Funcionalidade> Get()
        {
            List<Funcionalidade> ResultadoBusca = new List<Funcionalidade>();
            try
            {
                //API
                ResultadoBusca = new List<Funcionalidade>(biz.RetornaFuncionalidades());

                if (!biz.IsValid())
                    throw new InvalidDataException();

                ResultadoBusca.RemoveAll(a =>
                    a.FuncionalidadeIdPai != null
                );
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

    }
}