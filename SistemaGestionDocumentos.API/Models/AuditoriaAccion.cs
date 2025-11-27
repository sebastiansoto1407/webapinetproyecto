using System;

namespace SistemaGestionDocumentos.API.Models
{
    /// <summary>
    /// haz esto laime caramba 
    /// Quién hizo qu, cundo y dnde
    /// </summary>
    public class AuditoriaAccion
    {
        /// <summary>Identificador único del registro de auditoria</summary>
        public int IdentificadorRegistroAuditoria { get; set; }

        /// <summary>ID del usuario que realizo la acción</summary>
        public int IdentificadorUsuarioQueRealizaAccion { get; set; }

        /// <summary>Descripcion de la accion realizada (ej: "Subida de Documento", "Aprobacion", etc)</summary>
        public string DescripcionAccionRealizada { get; set; } = "";

        /// <summary>Fecha y hora exacta de la accion</summary>
        public DateTime FechaHoraExactaAccion { get; set; } = DateTime.UtcNow;

        /// <summary>Detalles adicionales de la accion (IP, navegador, etc)</summary>
        public string DetallesAdicionalesAccion { get; set; } = "";

        /// <summary>ID del documento relacionado</summary>
        public int? IdentificadorDocumentoRelacionadoAccion { get; set; }

        /// <summary>Direccion IP desde donde se realizo la accion</summary>
        public string DireccionIPDelDispositivoAccion { get; set; } = "";

        public Usuario UsuarioQueRealiza { get; set; }
    }
}
