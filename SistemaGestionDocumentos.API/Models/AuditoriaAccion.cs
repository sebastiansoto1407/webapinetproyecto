using System;

namespace SistemaGestionDocumentos.API.Models
{
    /// <summary>
    /// haz esto laime caramba 
    /// Quién hizo qu, cundo y dnde
    /// </summary>
    public class AuditoriaAccion
    {
        public int IdentificadorRegistroAuditoria { get; set; }
        
        public int IdentificadorUsuarioQueRealizaAccion { get; set; }

        public string DescripcionAccionRealizada { get; set; } = "";

        public DateTime FechaHoraExactaAccion { get; set; } = DateTime.UtcNow;

        public string DetallesAdicionalesAccion { get; set; } = "";

        public int? IdentificadorDocumentoRelacionadoAccion { get; set; }

        public string DireccionIPDelDispositivoAccion { get; set; } = "";
        

        public Usuario UsuarioQueRealiza { get; set; }
        
    }
}
