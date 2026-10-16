using Application.PrestamosEquipos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_CONTROL_TRANSPORTE.Controllers
{
    // Exige un JWT valido: el que emite api/Login/Login.
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PrestamoEquipoController : ControllerBase
    {
        private readonly IPrestamoEquipoService _prestamoService;
        public PrestamoEquipoController(IPrestamoEquipoService prestamoService) { _prestamoService = prestamoService; }

        [HttpGet("empresa/{codempresa}")]
        public IActionResult ObtenerTodos(int codempresa) { try { return Ok(_prestamoService.ObtenerTodos(codempresa)); } catch (Exception ex) { return BadRequest(new { respuesta = ex.Message }); } }

        [HttpGet("{codempresa}/{codsucursal}/{codcontratista}/{codequipo}")]
        public IActionResult ObtenerPorId(int codempresa, int codsucursal, int codcontratista, int codequipo)
        {
            try { var prestamo = _prestamoService.ObtenerPorId(codempresa, codsucursal, codcontratista, codequipo); return prestamo == null ? NotFound(new { respuesta = "Préstamo no encontrado." }) : Ok(prestamo); }
            catch (Exception ex) { return BadRequest(new { respuesta = ex.Message }); }
        }

        [HttpPost]
        public IActionResult Crear([FromBody] PrestamoEquipoDto prestamo)
        {
            if (!EsValido(prestamo)) return BadRequest(new { respuesta = "Empresa, sucursal, contratista, equipo y fecha de préstamo son obligatorios." });
            try { _prestamoService.Guardar(prestamo); return Ok(new { respuesta = "Préstamo registrado exitosamente.", datos = prestamo }); } catch (Exception ex) { return BadRequest(new { respuesta = $"Error al guardar: {ex.Message}" }); }
        }

        [HttpPut("{codempresa}/{codsucursal}/{codcontratista}/{codequipo}")]
        public IActionResult Actualizar(int codempresa, int codsucursal, int codcontratista, int codequipo, [FromBody] PrestamoEquipoDto prestamo)
        {
            if (!EsValido(prestamo)) return BadRequest(new { respuesta = "Los datos del préstamo son inválidos." });
            try { _prestamoService.Actualizar(codempresa, codsucursal, codcontratista, codequipo, prestamo); return Ok(new { respuesta = "Préstamo actualizado exitosamente.", datos = prestamo }); } catch (Exception ex) { return BadRequest(new { respuesta = $"Error al actualizar: {ex.Message}" }); }
        }

        [HttpDelete("{codempresa}/{codsucursal}/{codcontratista}/{codequipo}")]
        public IActionResult Eliminar(int codempresa, int codsucursal, int codcontratista, int codequipo)
        {
            try { _prestamoService.Eliminar(codempresa, codsucursal, codcontratista, codequipo); return Ok(new { respuesta = "Préstamo eliminado exitosamente." }); } catch (Exception ex) { return BadRequest(new { respuesta = $"Error al eliminar: {ex.Message}" }); }
        }

        private static bool EsValido(PrestamoEquipoDto prestamo) => prestamo != null && prestamo.Codempresa > 0 && prestamo.Codsucursal > 0 && prestamo.Codcontratista > 0 && prestamo.Codequipo > 0 && prestamo.Fechaprestamo.HasValue;
    }
}
