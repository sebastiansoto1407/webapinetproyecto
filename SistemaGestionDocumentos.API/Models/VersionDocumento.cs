using System;


namespace SistemaGestionDocumentos.API.Models
{
    /// <summary>
    /// Entidad que registra todas las acciones realizadas en el sistema
    /// Quién hizo qué, cuándo, dónde y en qué documento (si aplica)
    /// </summary>
    public class AuditoriaAccion
    {
        /// <summary>Identificador único del registro de auditoría - Clave Primaria</summary>
        public int IdentificadorRegistroAuditoria { get; set; }

        /// <summary>ID del usuario que realizó la acción (Clave Foránea)</summary>
        public int IdentificadorUsuarioQueRealizaAccion { get; set; }

        /// <summary>Descripción de la acción realizada</summary>
        public string DescripcionAccionRealizada { get; set; } = "";

        /// <summary>Fecha y hora exacta de la acción en UTC</summary>
        public DateTime FechaHoraExactaAccion { get; set; } = DateTime.UtcNow;

        /// <summary>Detalles adicionales o contexto de la acción</summary>
        public string DetallesAdicionalesAccion { get; set; } = "";

        /// <summary>ID del documento relacionado a la acción (opcional)</summary>
        public int? IdentificadorDocumentoRelacionadoAccion { get; set; }

        /// <summary>Dirección IP del dispositivo desde donde se realizó la acción</summary>
        public string DireccionIPDelDispositivoAccion { get; set; } = "";

        /// <summary>Navegación - Relación con Usuario</summary>
        public Usuario UsuarioQueRealiza { get; set; }
    }
}
