﻿using System;
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

        /// <summary>
        /// Retora todas as funcionalidades para utilização no menu
        /// </summary>
        /// <returns></returns>
        public List<Funcionalidade> GetForMenu()
        {
            List<Funcionalidade> ResultadoBusca = new List<Funcionalidade>();
            try
            {
                //VerificaAutenticacao(Funcionalidade.Rotina_Pesquisa, Funcionalidade.NomeRotina_Pesquisa);

                //API
                string[] funcionalidadesUsuario = RetornaFuncionalidadesUsuario();

                if (funcionalidadesUsuario != null)
                {
                    int[] funcionalidadeId = Array.ConvertAll(funcionalidadesUsuario, int.Parse);
                    ResultadoBusca = new List<Funcionalidade>(biz.RetornaFuncionalidades_UtilizaMenu(funcionalidadeId));
                }

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

    }
}