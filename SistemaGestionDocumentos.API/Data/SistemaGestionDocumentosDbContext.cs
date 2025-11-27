using Microsoft.EntityFrameworkCore;
using SistemaGestionDocumentos.API.Models;

namespace SistemaGestionDocumentos.API.Data
{
    /// <summary>
    /// Contexto de base de datos para el sistema de gestipn de documentos
    /// Configura todas las entidades y sus relaciones
    /// </summary>
    public class SistemaGestionDocumentosDbContext : DbContext
    {
        public SistemaGestionDocumentosDbContext(DbContextOptions<SistemaGestionDocumentosDbContext> opciones)
            : base(opciones)
        {
        }

        // DbSets para todas las entidades
        public DbSet<Usuario> UsuariosDelSistema { get; set; }
        public DbSet<Documento> DocumentosDelSistema { get; set; }
        public DbSet<VersionDocumento> VersionesDelSistema { get; set; }
        public DbSet<WorkflowAprobacion> WorkflowAprobacionDelSistema { get; set; }
        public DbSet<AuditoriaAccion> AuditoriaAccionDelSistema { get; set; }
        public DbSet<NotificacionPendiente> NotificacionesPendientesDelSistema { get; set; }

        protected override void OnModelCreating(ModelBuilder constructor)
        {
            base.OnModelCreating(constructor);


            // Relacion Usuario -> Documentos
            constructor.Entity<Documento>()
                .HasOne(d => d.UsuarioPropietarioDelDocumento)
                .WithMany(u => u.DocumentosDelUsuario)
                .HasForeignKey(d => d.IdentificadorUsuarioPropietarioDelDocumento)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacion Documento -> Versiones
            constructor.Entity<VersionDocumento>()
                .HasOne(v => v.DocumentoAlQuePerteneceLaVersion)
                .WithMany(d => d.VersionesDelDocumento)
                .HasForeignKey(v => v.IdentificadorDocumentoPerteneceVersion)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacion Documento -> Workflow Aprobación
            constructor.Entity<WorkflowAprobacion>()
                .HasOne(w => w.DocumentoEnAprobacion)
                .WithMany(d => d.FlujosAprobacionDelDocumento)
                .HasForeignKey(w => w.IdentificadorDocumentoAprobar)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacio Usuario Aprobador -> Workflow Aprobacion
            constructor.Entity<WorkflowAprobacion>()
                .HasOne(w => w.UsuarioQueAprueba)
                .WithMany(u => u.AprobacionesDelUsuario)
                .HasForeignKey(w => w.IdentificadorUsuarioAprobadorDelDocumento)
                .OnDelete(DeleteBehavior.NoAction);

            // Relacion Usuario -> Auditoria
            constructor.Entity<AuditoriaAccion>()
                .HasOne(a => a.UsuarioQueRealiza)
                .WithMany(u => u.AccionesAuditoriasDelUsuario)
                .HasForeignKey(a => a.IdentificadorUsuarioQueRealizaAccion)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacion Usuario -> Notificaciones (como receptor)
            constructor.Entity<NotificacionPendiente>()
                .HasOne(n => n.UsuarioReceptor)
                .WithMany(u => u.NotificacionesDelUsuario)
                .HasForeignKey(n => n.IdentificadorUsuarioReceptorNotificacion)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacion Documento -> Notificaciones
            constructor.Entity<NotificacionPendiente>()
                .HasOne(n => n.DocumentoRelacionado)
                .WithMany(d => d.NotificacionesDelDocumento)
                .HasForeignKey(n => n.IdentificadorDocumentoRelacionadoNotificacion)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
