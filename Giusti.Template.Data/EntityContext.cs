using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Giusti.Template.Model;
using Giusti.Template.Data.Configuration;

namespace Giusti.Template.Data
{
    public class EntityContext : DbContext, IDisposable
    {
        public EntityContext(string connectionName)
            : base(connectionName)
        {
            this.Configuration.AutoDetectChangesEnabled = true;
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.ValidateOnSaveEnabled = false;

        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Log> Loges { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<EntityContext>(null);
            modelBuilder.Configurations.Add(new UsuarioConfiguration());
            modelBuilder.Configurations.Add(new LogConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}