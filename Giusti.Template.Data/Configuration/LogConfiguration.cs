﻿using System.Data.Entity.ModelConfiguration;
using Giusti.Template.Model;

namespace Giusti.Template.Data.Configuration
{
    public partial class LogConfiguration : EntityTypeConfiguration<Log>
    {
        public LogConfiguration()
        {
            string Schema = System.Configuration.ConfigurationManager.AppSettings["Schema"];
            if (string.IsNullOrEmpty(Schema))

                this.ToTable("Log");
            else
                this.ToTable("Log", Schema);
            this.HasKey(i => new { i.Id });
            this.Property(i => i.Id).HasColumnName("d");
            this.Property(i => i.UsuarioId).HasColumnName("UsuarioId");
            this.Property(i => i.FuncionalidadeId).HasColumnName("FuncionalidadeId");
            this.Property(i => i.RegistroId).HasColumnName("RegistroId");
            this.Property(i => i.Acao).HasColumnName("Acao");
            this.Property(i => i.OrigemAcesso).HasColumnName("OrigemAcesso");
            this.Property(i => i.IpMaquina).HasColumnName("IpMaquina");
            this.HasRequired(i => i.Usuario).WithMany().HasForeignKey(d => d.UsuarioId);
            this.HasRequired(i => i.Funcionalidade).WithMany().HasForeignKey(d => d.FuncionalidadeId);
            this.Property(i => i.DataInclusao).HasColumnName("DataInclusao");
        }
    }
}

