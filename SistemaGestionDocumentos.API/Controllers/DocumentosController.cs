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
/// Obtener todos los documentos del usuario autenticado
/// GET: api/documentos
/// </summary>
[HttpGet]
public async Task<ActionResult<IEnumerable<object>>> ObtenerTodosLosDocumentos()
{
    try
    {
        // Obtener el ID del usuario desde el header personalizado
        if (!Request.Headers.TryGetValue("X-Usuario-Id", out var usuarioIdHeader) || 
            !int.TryParse(usuarioIdHeader.ToString(), out int usuarioId))
        {
            return BadRequest(new { mensaje = "Usuario no identificado", exitoso = false });
        }
        

        // Obtener solo los documentos del usuario autenticado
        var documentosDelUsuario = await _contextoBD.DocumentosDelSistema
            .Where(d => d.IdentificadorUsuarioPropietarioDelDocumento == usuarioId)
            .Include(d => d.UsuarioPropietarioDelDocumento)
            .Select(d => new
            {
                d.IdentificadorDocumento,
                d.NombreDescriptivoDelDocumento,
                d.NumeroVersionActualDelDocumento,
                d.EstadoActualDelDocumento,
                d.FechaSubidaDelDocumento,
                d.FechaUltimaModificacionDelDocumento,
                
                d.DescripcionAdicionalesDelDocumento,
                UsuarioPropietario = d.UsuarioPropietarioDelDocumento.NombreCompletoDelUsuario
            })
            .OrderByDescending(d => d.FechaSubidaDelDocumento)
            .ToListAsync();

        return Ok(documentosDelUsuario);
    }
    catch (Exception excepcion)
    {
        _registradorDeLog.LogError($"Error obteniendo documentos: {excepcion.Message}");
        return StatusCode(500, new { mensaje = "Error interno del servidor", exitoso = false });
    }
}
    }
}