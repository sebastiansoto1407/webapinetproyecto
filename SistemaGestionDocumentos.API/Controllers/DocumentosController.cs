using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGestionDocumentos.API.Data;
using SistemaGestionDocumentos.API.Models;

namespace SistemaGestionDocumentos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentosController : ControllerBase
    {
        private readonly SistemaGestionDocumentosDbContext _contextoBD;
        private readonly ILogger<DocumentosController> _registradorDeLog;

        public DocumentosController(SistemaGestionDocumentosDbContext contextoBD, ILogger<DocumentosController> registrador)
        {
            _contextoBD = contextoBD;
            _registradorDeLog = registrador;
        }

        /// <summary>
        /// Obtener todos los documentos
        /// GET: api/documentos
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> ObtenerTodosLosDocumentos()
        {
            try
            {
                var documentosDelSistema = await _contextoBD.DocumentosDelSistema
                    .Include(d => d.UsuarioPropietarioDelDocumento)
                    .Select(d => new
                    {
                        d.IdentificadorDocumento,
                        d.NombreDescriptivoDelDocumento,
                        d.NumeroVersionActualDelDocumento,
                        d.EstadoActualDelDocumento,
                        d.FechaSubidaDelDocumento,
                        d.FechaUltimaModificacionDelDocumento,
                        UsuarioPropietario = d.UsuarioPropietarioDelDocumento.NombreCompletoDelUsuario
                    })
                    .OrderByDescending(d => d.FechaSubidaDelDocumento)
                    .ToListAsync();

                return Ok(documentosDelSistema);
            }
            catch (Exception excepcion)
            {
                _registradorDeLog.LogError($"Error obteniendo documentos: {excepcion.Message}");
                return StatusCode(500, new { mensaje = "Error interno del servidor", exitoso = false });
            }
        }

        /// <summary>
        /// Obtener documento por ID
        /// GET: api/documentos/{id}
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> ObtenerDocumentoPorId(int id)
        {
            try
            {
                var documentoEncontrado = await _contextoBD.DocumentosDelSistema
                    .Include(d => d.UsuarioPropietarioDelDocumento)
                    .Include(d => d.VersionesDelDocumento)
                    .FirstOrDefaultAsync(d => d.IdentificadorDocumento == id);

                if (documentoEncontrado == null)
                    return NotFound(new { mensaje = "Documento no encontrado", exitoso = false });

                return Ok(new
                {
                    documentoEncontrado.IdentificadorDocumento,
                    documentoEncontrado.NombreDescriptivoDelDocumento,
                    documentoEncontrado.NumeroVersionActualDelDocumento,
                    documentoEncontrado.EstadoActualDelDocumento,
                    documentoEncontrado.FechaSubidaDelDocumento,
                    documentoEncontrado.DescripcionAdicionalesDelDocumento,
                    UsuarioPropietario = new
                    {
                        documentoEncontrado.UsuarioPropietarioDelDocumento.IdentificadorUsuario,
                        documentoEncontrado.UsuarioPropietarioDelDocumento.NombreCompletoDelUsuario,
                        documentoEncontrado.UsuarioPropietarioDelDocumento.CorreoElectronicoDelUsuario
                    },
                    Versiones = documentoEncontrado.VersionesDelDocumento.Select(v => new
                    {
                        v.IdentificadorVersionDocumento,
                        v.NumeroSecuencialDelVersionDocumento,
                        v.FechaCreacionDelVersionDocumento,
                        v.ComentariosDelCambioDeVersion
                    })
                });
            }
            catch (Exception excepcion)
            {
                _registradorDeLog.LogError($"Error obteniendo documento: {excepcion.Message}");
                return StatusCode(500, new { mensaje = "Error interno del servidor", exitoso = false });
            }
        }

        /// <summary>
        /// Obtener documentos por usuario
        /// GET: api/documentos/por-usuario/{usuarioId}
        /// </summary>
        [HttpGet("por-usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<object>>> ObtenerDocumentosPorUsuario(int usuarioId)
        {
            try
            {
                var documentosDelUsuario = await _contextoBD.DocumentosDelSistema
                    .Where(d => d.IdentificadorUsuarioPropietarioDelDocumento == usuarioId)
                    .Select(d => new
                    {
                        d.IdentificadorDocumento,
                        d.NombreDescriptivoDelDocumento,
                        d.NumeroVersionActualDelDocumento,
                        d.EstadoActualDelDocumento,
                        d.FechaSubidaDelDocumento
                    })
                    .OrderByDescending(d => d.FechaSubidaDelDocumento)
                    .ToListAsync();

                return Ok(documentosDelUsuario);
            }
            catch (Exception excepcion)
            {
                _registradorDeLog.LogError($"Error obteniendo documentos por usuario: {excepcion.Message}");
                return StatusCode(500, new { mensaje = "Error interno del servidor", exitoso = false });
            }
        }

        /// <summary>
        /// Eliminar documento
        /// DELETE: api/documentos/{id}
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<object>> EliminarDocumento(int id)
        {
            try
            {
                var documentoAEliminar = await _contextoBD.DocumentosDelSistema
                    .Include(d => d.VersionesDelDocumento)
                    .FirstOrDefaultAsync(d => d.IdentificadorDocumento == id);

                if (documentoAEliminar == null)
                    return NotFound(new { mensaje = "Documento no encontrado", exitoso = false });

                _contextoBD.DocumentosDelSistema.Remove(documentoAEliminar);
                await _contextoBD.SaveChangesAsync();

                _registradorDeLog.LogInformation($"Documento {id} eliminado");

                return Ok(new { mensaje = "Documento eliminado exitosamente", exitoso = true });
            }
            catch (Exception excepcion)
            {
                _registradorDeLog.LogError($"Error eliminando documento: {excepcion.Message}");
                return StatusCode(500, new { mensaje = "Error interno del servidor", exitoso = false });
            }
        }
    }
}
