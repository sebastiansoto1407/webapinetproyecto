using System;
using System.Collections.Generic;

namespace SistemaGestionDocumentos.API.Models
{
    /// <summary>
    /// soto
    /// </summary>
    public class Documento
    {
        /// <summary>Identificador unico del documento</summary>
        public int IdentificadorDocumento { get; set; }
        

        /// <summary>Nombre descriptivo del documento (ej: "Propuesta de Proyecto Q4")</summary>
        public string NombreDescriptivoDelDocumento { get; set; } = "";

        /// <summary>Ruta fisica donde se almacena el archivo en el servidor</summary>
        public string RutaFisicaDelArchivoDocumento { get; set; } = "";

        /// <summary>Numde version actual del documento</summary>
        public int NumeroVersionActualDelDocumento { get; set; } = 1;

        /// <summary>ID del usuario que subio/creo el documento (propietario)</summary>
        public int IdentificadorUsuarioPropietarioDelDocumento { get; set; }

        /// <summary>Fecha y hora en que se subio inicialmente el documento</summary>
        public DateTime FechaSubidaDelDocumento { get; set; } = DateTime.UtcNow;

        /// <summary>Estado actual del documento: "Pendiente", "EnRevision", "Aprobado", "Rechazado"</summary>
        public string EstadoActualDelDocumento { get; set; } = "Pendiente";

        /// <summary>Descripcion o notas adicionales del documento</summary>
        public string DescripcionAdicionalesDelDocumento { get; set; } = "";

        /// <summary>Fecha de la ultima modificacion del documento</summary>
        public DateTime FechaUltimaModificacionDelDocumento { get; set; } = DateTime.UtcNow;

        // Relaciones 
        public Usuario UsuarioPropietarioDelDocumento { get; set; }
        public ICollection<VersionDocumento> VersionesDelDocumento { get; set; } = new List<VersionDocumento>();
        public ICollection<WorkflowAprobacion> FlujosAprobacionDelDocumento { get; set; } = new List<WorkflowAprobacion>();
        public ICollection<NotificacionPendiente> NotificacionesDelDocumento { get; set; } = new List<NotificacionPendiente>();
    }
}
