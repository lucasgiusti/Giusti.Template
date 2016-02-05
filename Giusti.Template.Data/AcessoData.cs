using System.Collections.Generic;
using System.Linq;
using System.Data;
using Giusti.Template.Model;
using Giusti.Template.Data.Library;

namespace Giusti.Template.Data
{
    public class AcessoData : DataBase
    {
        public Acesso RetornaAcesso_Id(int? id)
        {
            IQueryable<Acesso> query = Context.Acessos;
            if (id.HasValue)
                query = query.Where(d => d.Id == id);
            return query.FirstOrDefault();
        }
        public IList<Acesso> RetornaAcessos()
        {
            IQueryable<Acesso> query = Context.Acessos;
            return query.ToList();
        }
        public IList<Acesso> RetornaAcessos_PerfilId_FuncionalidadeId(int? perfilId, int? funcionalidadeId)
        {
            IQueryable<Acesso> query = Context.Acessos;
            if (perfilId.HasValue)
                query = query.Where(d => d.PerfilId == perfilId);
            if (funcionalidadeId.HasValue)
                query = query.Where(d => d.FuncionalidadeId == funcionalidadeId);
            return query.ToList();
        }
        public void SalvaAcesso(Acesso itemGravar)
        {
            Acesso itemBase = Context.Acessos.Where(f => f.Id == itemGravar.Id).FirstOrDefault();
            if (itemBase == null)
            {
                itemBase = Context.Acessos.Create();

                Context.Entry<Acesso>(itemBase).State = System.Data.Entity.EntityState.Added;
            }
            AtualizaPropriedades<Acesso>(itemBase, itemGravar);

            Context.SaveChanges();
            itemGravar.Id = itemBase.Id;

        }
        public void ExcluiAcesso(Acesso itemGravar)
        {
            Acesso itemExcluir = Context.Acessos.Where(f => f.Id == itemGravar.Id).FirstOrDefault();
            Context.Entry<Acesso>(itemExcluir).State = System.Data.Entity.EntityState.Deleted;

            Context.SaveChanges();
        }
    }
}
