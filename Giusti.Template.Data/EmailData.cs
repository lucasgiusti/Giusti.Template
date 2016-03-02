using System.Collections.Generic;
using System.Linq;
using System.Data;
using Giusti.Template.Model;
using Giusti.Template.Data.Library;

namespace Giusti.Template.Data
{
    public class EmailData : DataBase
    {
        public void SalvaEmail(Email itemGravar)
        {
            Email itemBase = Context.Emails
            .Where(f => f.Id == itemGravar.Id).FirstOrDefault();
            if (itemBase == null)
            {
                itemBase = Context.Emails.Create();
                Context.Entry<Email>(itemBase).State = System.Data.Entity.EntityState.Added;
            }
            AtualizaPropriedades<Email>(itemBase, itemGravar);
            Context.SaveChanges();
            itemGravar.Id = itemBase.Id;
        }
    }
}
