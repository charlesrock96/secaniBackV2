﻿using Core.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Infra
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<ContactoEntidad>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    var entity = entry.Entity;
                    entity.DateCreated = DateTime.UtcNow;
                    entity.CreatedByUserId = "1";
                    entity.IsDeleted = false;
                }

                if (entry.State == EntityState.Modified)
                {
                    var entity = entry.Entity;
                    entity.DateUpdated = DateTime.UtcNow;
                    entity.UpdatedByUserId = "2";
                }

                if (entry.State == EntityState.Deleted)
                {
                    var entity = entry.Entity;
                    entity.DateDeleted = DateTime.UtcNow;
                    entity.DeletedByUserId = "3";
                    entity.IsDeleted = true;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar la vista VwMenu como una entidad sin clave
            modelBuilder.Entity<VwMenuModel>(eb =>
            {
                eb.HasNoKey();
                eb.ToView("VwMenu"); // Esto es válido para versiones recientes de EF Core
            });
        }

        public DbSet<Alerta> Alertas { get; set; }
        public DbSet<AlertaSeguimiento> AlertaSeguimientos { get; set; }
        public DbSet<Notificacion> Notificacions { get; set; }
        public DbSet<Seguimiento> Seguimientos { get; set; }
        public DbSet<UsuarioAsignado> UsuarioAsignados { get; set; }
        public DbSet<NotificacionesUsuario> NotificacionesUsuarios { get; set; }
        public DbSet<AspNetUsers> AspNetUsers { get; set; }
        public DbSet<NotificacionEntidad> NotificacionesEntidad { get; set; }
        public DbSet<Entidad> Entidades { get; set; }
        public DbSet<NNA> NNAs { get; set; }
        public DbSet<Intento> Intentos { get; set; }
        public DbSet<ContactoNNA> ContactoNNAs { get; set; }
        public DbSet<ContactoEntidad> ContactoEntidades { get; set; }
        public DbSet<VwMenuModel> VwMenu { get; set; }
    }
}