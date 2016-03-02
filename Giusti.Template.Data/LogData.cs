using System.Collections.Generic;
using System.Linq;
using System.Data;
using Giusti.Template.Model;
using Giusti.Template.Data.Library;

namespace Giusti.Template.Data
{
    public class LogData : DataBase
    {
        public bool ExisteLog_UsuarioId(int usuarioId)
        {
            IQueryable<Log> query = Context.Logs;

            query = query.Where(d => d.UsuarioId == usuarioId);
            return query.Any();
        }
        public void SalvaLog(Log itemGravar)
        {
            Log itemBase = Context.Logs
            .Where(f => f.Id == itemGravar.Id).FirstOrDefault();
            if (itemBase == null)
            {
                itemBase = Context.Logs.Create();
                Context.Entry<Log>(itemBase).State = System.Data.Entity.EntityState.Added;
            }
            AtualizaPropriedades<Log>(itemBase, itemGravar);
            Context.SaveChanges();
            itemGravar.Id = itemBase.Id;
        }
    }
}
