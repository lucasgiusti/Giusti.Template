using System;
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
        public DbSet<Funcionalidade> Funcionalidades { get; set; }
        public DbSet<Perfil> Perfis { get; set; }
        public DbSet<PerfilFuncionalidade> PerfilFuncionalidades { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<EntityContext>(null);
            modelBuilder.Configurations.Add(new UsuarioConfiguration());
            modelBuilder.Configurations.Add(new LogConfiguration());
            modelBuilder.Configurations.Add(new FuncionalidadeConfiguration());
            modelBuilder.Configurations.Add(new PerfilConfiguration());
            modelBuilder.Configurations.Add(new PerfilFuncionalidadeConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}