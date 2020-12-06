using GestaoJogos.CrossCutting.Auditing;
using GestaoJogos.CrossCutting.Auditing.EventArgs;
using GestaoJogos.Domain.Principal.Entities;
using GestaoJogos.Infrastructure.EntityFramework.Extensions;
using GestaoJogos.Infrastructure.EntityFramework.Persistence.Mappings;
using GestaoJogos.SharedKernel.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace GestaoJogos.Infrastructure.EntityFramework.Context
{
    public class GestaoJogosContext : DbContext, IDataContext
    {
        private List<EntityEntry> _newEntitiesList;
        private List<EntityEntry> _deletedEntitiesList;
        private List<EntityEntry> _modifiedEntitiesList;
        private readonly IAuditManager _auditManager;

        public GestaoJogosContext(
            DbContextOptions<GestaoJogosContext> options,
            IAuditManager auditManager
        ) : base(options)
        {
            _auditManager = auditManager;
        }

        public DbSet<Amigo> Amigos { get; set; }
        public DbSet<Jogo> Jogos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfiguration(new AmigoMap());
            modelBuilder.AddConfiguration(new JogoMap());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                     .SetBasePath(Directory.GetCurrentDirectory() + "/../../Presentation/GestaoJogos.Presentation.Api")
                     .AddJsonFile("appsettings.json", false, true)
                     .Build();
                var connectionString = configuration.GetConnectionString("GestaoJogosConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        public override int SaveChanges()
        {
            PreAudit();
            int r = base.SaveChanges();
            PostAudit();

            return r;
        }

        private void PreAudit()
        {
            ChangeTracker.DetectChanges();
            _newEntitiesList = ChangeTracker.Entries().Where(p => p.State == EntityState.Added).ToList();
            _modifiedEntitiesList = ChangeTracker.Entries().Where(p => p.State == EntityState.Modified).ToList();
            _deletedEntitiesList = ChangeTracker.Entries().Where(p => p.State == EntityState.Deleted).ToList();
        }

        private void PostAudit()
        {
            if (_auditManager != null)
            {
                RaiseAuditEntityEvent(_newEntitiesList, t => t.Action = AuditEventArgs.INSERT);
                RaiseAuditEntityEvent(_modifiedEntitiesList, t => t.Action = AuditEventArgs.UPDATE);
                RaiseAuditEntityEvent(_deletedEntitiesList, t => t.Action = AuditEventArgs.DELETE);
            }
        }

        private void RaiseAuditEntityEvent(List<EntityEntry> entradas, Action<AuditEventArgs> acao)
        {
            foreach (var item in entradas)
            {
                var eventArgs = new AuditEventArgs
                {
                    EntityNamespace = item.Entity.GetType().Namespace,
                    EntityName = item.Entity.GetType().Name,
                    EntityId = GetPrimaryKeyValue(item.Entity)
                };

                // configura a ação
                acao(eventArgs);

                var fields = GetEntityFields(item);
                eventArgs.Fields = fields;
                _auditManager.OnAuditEntity(eventArgs);
            }
        }

        private Dictionary<string, string> GetEntityFields(EntityEntry item)
        {
            var ret = new Dictionary<string, string>();
            foreach (var prop in item.Entity.GetType().GetTypeInfo().DeclaredProperties)
            {
                var propName = prop.Name;
                try
                {
                    // pode lancar exceção (property does not exist), por isso dentro do try catch 
                    var propVal = $"{item.Property(propName).CurrentValue}";
                    ret.Add(propName, propVal);
                }
                catch { }
            }
            return ret;
        }

        protected virtual int GetPrimaryKeyValue<T>(T entity)
        {
            try
            {
                var keyName = this.Model
                    .FindEntityType(entity.GetType())
                    .FindPrimaryKey()
                    .Properties
                    .Select(x => x.Name).Single();

                object keyVal = (int)entity.GetType().GetProperty(keyName).GetValue(entity, null);

                int value;
                if (int.TryParse($"{keyVal}", out value))
                {
                    return value;
                }
            }
            catch { }

            return -1;
        }
    }
}
