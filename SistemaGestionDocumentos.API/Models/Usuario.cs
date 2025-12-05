using System;
using System.Collections.Generic;

namespace SistemaGestionDocumentos.API.Models
{
    /// <summary>
    ///jose
    /// </summary>
    public class Usuario
    {
        public int IdentificadorUsuario { get; set; }

        public string CorreoElectronicoDelUsuario { get; set; } = "";

        public string ContraseñaHashDelUsuario { get; set; } = "";

        public string NombreCompletoDelUsuario { get; set; } = "";

        public string RolDelUsuario { get; set; } = "Solicitante";

        public DateTime FechaCreacionDelUsuario { get; set; } = DateTime.UtcNow;






        public bool IndicadorUsuarioActivo { get; set; } = true;

        // Relaciones
        public ICollection<Documento> DocumentosDelUsuario { get; set; } = new List<Documento>();
        public ICollection<WorkflowAprobacion> AprobacionesDelUsuario { get; set; } = new List<WorkflowAprobacion>();
        public ICollection<AuditoriaAccion> AccionesAuditoriasDelUsuario { get; set; } = new List<AuditoriaAccion>();
        public ICollection<NotificacionPendiente> NotificacionesDelUsuario { get; set; } = new List<NotificacionPendiente>();
    }
}
