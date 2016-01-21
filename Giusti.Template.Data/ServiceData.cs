using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity.Infrastructure;
using Giusti.Template.Model;
using Giusti.Template.Data.Library;

namespace Giusti.Template.Data
{
    public class ServiceData : DataBase
    {
        #region Usuario

        public Usuario RetornaUsuario_Email(string email)
        {
            IQueryable<Usuario> query = Context.Usuarios;

            if (!string.IsNullOrEmpty(email))
                query = query.Where(d => d.Email == email);
            return query.FirstOrDefault();
        }
        public IList<Usuario> RetornaUsuarios()
        {
            IQueryable<Usuario> query = Context.Usuarios;

            return query.ToList();
        }

        #endregion

        #region Log

        public void SalvaLog(Log itemGravar)
        {
            Log itemBase = Context.Loges
            .Where(f => f.Id == itemGravar.Id).FirstOrDefault();
            if (itemBase == null)
            {
                itemBase = Context.Loges.Create();
                Context.Entry<Log>(itemBase).State = System.Data.Entity.EntityState.Added;
            }
            AtualizaPropriedades<Log>(itemBase, itemGravar);
            Context.SaveChanges();
            itemGravar.Id = itemBase.Id;
        }

        #endregion
    }
}
