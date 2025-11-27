using System;
using System.Collections.Generic;

namespace SistemaGestionDocumentos.API.Models
{
    /// <summary>
    ///jose
    /// </summary>
    public class Usuario
    {
        /// <summary>Identificador unico del usuario</summary>
        public int IdentificadorUsuario { get; set; }

        /// <summary>Correo electronico unico del usuario (login)</summary>
        public string CorreoElectronicoDelUsuario { get; set; } = "";

        /// <summary>Contraseña hasheada del usuario </summary>
        public string ContraseñaHashDelUsuario { get; set; } = "";

        /// <summary>Nombre completo del usuario</summary>
        public string NombreCompletoDelUsuario { get; set; } = "";

        /// <summary>Rol del usuario: "Solicitante", "Aprobador", "Admin"</summary>
        public string RolDelUsuario { get; set; } = "Solicitante";

        /// <summary>Fecha y hora en que se creo el usuario</summary>
        public DateTime FechaCreacionDelUsuario { get; set; } = DateTime.UtcNow;

        /// <summary>Indica si el usuario esta activo o desactivado
        public bool IndicadorUsuarioActivo { get; set; } = true;

        // Relaciones
        public ICollection<Documento> DocumentosDelUsuario { get; set; } = new List<Documento>();
        public ICollection<WorkflowAprobacion> AprobacionesDelUsuario { get; set; } = new List<WorkflowAprobacion>();
        public ICollection<AuditoriaAccion> AccionesAuditoriasDelUsuario { get; set; } = new List<AuditoriaAccion>();
        public ICollection<NotificacionPendiente> NotificacionesDelUsuario { get; set; } = new List<NotificacionPendiente>();
    }
}
