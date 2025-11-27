using System;

namespace SistemaGestionDocumentos.API.Models
{
    /// <summary>
    /// Entidad que registra las diferentes versiones de un documento
    /// sotooooo
    /// </summary>
    public class VersionDocumento
    {
        /// <summary>Identificador unico de la version</summary>
        public int IdentificadorVersionDocumento { get; set; }

        /// <summary>ID del documento al que pertenece esta version</summary>
        public int IdentificadorDocumentoPerteneceVersion { get; set; }

        /// <summary>Nombre original del archivo de esta version</summary>
        public string NombreArchivoDelVersionDocumento { get; set; } = "";

        /// <summary>Ruta fisica del archivo en esta version</summary>
        public string RutaFisicaDelArchivoVersion { get; set; } = "";

        /// <summary>Número secuencial de la version </summary>
        public int NumeroSecuencialDelVersionDocumento { get; set; }

        /// <summary>Fecha y hora de creacion de esta version</summary>
        public DateTime FechaCreacionDelVersionDocumento { get; set; } = DateTime.UtcNow;

        /// <summary>Comentarios o cambios realizados en esta versio</summary>
        public string ComentariosDelCambioDeVersion { get; set; } = "";

        // Relacion de navegacio
        public Documento DocumentoAlQuePerteneceLaVersion { get; set; }
    }
}
