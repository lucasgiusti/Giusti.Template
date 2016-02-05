using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using Giusti.Template.Model;

namespace Giusti.Template.Data.Configuration
{
    public partial class AcessoConfiguration : EntityTypeConfiguration<Acesso>
    {
        public AcessoConfiguration()
        {
            string Schema = System.Configuration.ConfigurationManager.AppSettings["Schema"];
            if (string.IsNullOrEmpty(Schema))

                this.ToTable("Acesso");
            else
                this.ToTable("Acesso", Schema);
            this.HasKey(i => new { i.Id });
            this.Property(i => i.Id).HasColumnName("Id");
            this.Property(i => i.PerfilId).HasColumnName("PerfilId");
            this.Property(i => i.FuncionalidadeId).HasColumnName("FuncionalidadeId");

        }
    }
}

