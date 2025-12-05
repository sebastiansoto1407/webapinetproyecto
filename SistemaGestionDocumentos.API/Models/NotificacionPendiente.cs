using System;

namespace SistemaGestionDocumentos.API.Models
{
    /// <summary>
    /// max
    /// </summary>
    public class NotificacionPendiente
    {
        /// <summary>Identificador unico de la notificación</summary>
        public int IdentificadorNotificacionPendiente { get; set; }

        /// <summary>ID del usuario que recibirá la notificacion</summary>
        public int IdentificadorUsuarioReceptorNotificacion { get; set; }

        /// <summary>ID del documento relacionado </summary>
        public int? IdentificadorDocumentoRelacionadoNotificacion { get; set; }

        /// <summary>Tipo de notificacion: "SolicitudAprobacion", "Aprobado", "Rechazado", "NuevaVersion"</summary>
        public string TipoNotificacionEnviar { get; set; } = "";

        /// <summary>Asunto del correo de notificacion</summary>
        public string AsuntoNotificacionCorreo { get; set; } = "";

        /// <summary>Cuerpo del mensaje de notificacion</summary>
        public string ContenidoMensajeNotificacion { get; set; } = "";

        /// <summary>Indica si ya se leyo la notificación 
        public bool IndicadorNotificacionLeidaONo { get; set; } = false;

        /// <summary>Fecha y hora de creacion de la notificación</summary>
        public DateTime FechaCreacionNotificacion { get; set; } = DateTime.UtcNow;

        /// <summary>Fecha y hora de envio de la notificacion
        public DateTime? FechaEnvioNotificacion { get; set; }

        // Relaciones de navegaciom
        public Usuario UsuarioReceptor { get; set; }
        public Documento DocumentoRelacionado { get; set; }
        
    }
}
