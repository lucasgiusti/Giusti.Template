using System.Collections.Generic;
using System.Linq;
using Giusti.Template.Model.Results;
using Giusti.Template.Business.Library;
using Giusti.Template.Data;
using Giusti.Template.Model;
using System;
using Giusti.Template.Model.Dominio;
using System.Web.Security;

namespace Giusti.Template.Business
{
    public class LogBusiness : BusinessBase
    {
        public IList<Log> RetornaLogs()
        {
            LimpaValidacao();
            IList<Log> RetornoAcao = new List<Log>();
            if (IsValid())
            {
                using (LogData data = new LogData())
                {
                    RetornoAcao = data.RetornaLogs();
                }
            }

            return RetornoAcao;
        }
        public bool ExisteLog_UsuarioId(int id)
        {
            LimpaValidacao();
            bool RetornoAcao = false;
            if (IsValid())
            {
                using (LogData data = new LogData())
                {
                    RetornoAcao = data.ExisteLog_UsuarioId(id);
                }
            }

            return RetornoAcao;
        }

        public void SalvaLog(Log itemGravar)
        {
            ValidateService(itemGravar);
            ValidaRegrasNegocioLog(itemGravar);
            if (IsValid())
            {
                using (LogData data = new LogData())
                {
                    data.SalvaLog(itemGravar);
                }
            }
        }

        private void ValidaRegrasNegocioLog(Log itemGravar)
        {
        }
    }
}
