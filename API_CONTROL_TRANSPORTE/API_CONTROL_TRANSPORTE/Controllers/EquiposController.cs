using Application.Equipos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_CONTROL_TRANSPORTE.Controllers
{
    // Exige un JWT valido: el que emite api/Login/Login.
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EquiposController : ControllerBase
    {
        private readonly IEquipoService _equipoService;

        public EquiposController(IEquipoService equipoService) { _equipoService = equipoService; }

        [HttpPost]
        public IActionResult Crear([FromBody] EquipoDto equipo)
        {
            if (!EsValido(equipo)) return BadRequest(new { respuesta = "Los datos del equipo son inválidos." });
            try { _equipoService.GuardarEquipo(equipo); return Ok(new { respuesta = "Equipo guardado exitosamente.", datos = equipo }); }
            catch (Exception ex) { return BadRequest(new { respuesta = $"Error al guardar: {ex.Message}" }); }
        }

        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            try { return Ok(_equipoService.ObtenerTodos()); }
            catch (Exception ex) { return BadRequest(new { respuesta = $"Error al obtener equipos: {ex.Message}" }); }
        }

        [HttpGet("{codempresa}/{codequipo}")]
        public IActionResult ObtenerPorId(int codempresa, int codequipo)
        {
            try
            {
                var equipo = _equipoService.ObtenerPorId(codempresa, codequipo);
                return equipo == null ? NotFound(new { respuesta = "Equipo no encontrado." }) : Ok(equipo);
            }
            catch (Exception ex) { return BadRequest(new { respuesta = $"Error al obtener equipo: {ex.Message}" }); }
        }

        [HttpPut("{codempresa}/{codequipo}")]
        public IActionResult Actualizar(int codempresa, int codequipo, [FromBody] EquipoDto equipo)
        {
            if (!EsValido(equipo)) return BadRequest(new { respuesta = "Los datos del equipo son inválidos." });
            try { _equipoService.ActualizarEquipo(codempresa, codequipo, equipo); return Ok(new { respuesta = "Equipo actualizado exitosamente.", datos = equipo }); }
            catch (Exception ex) { return BadRequest(new { respuesta = $"Error al actualizar: {ex.Message}" }); }
        }

        [HttpDelete("{codempresa}/{codequipo}")]
        public IActionResult Eliminar(int codempresa, int codequipo)
        {
            try { _equipoService.EliminarEquipo(codempresa, codequipo); return Ok(new { respuesta = "Equipo desactivado exitosamente." }); }
            catch (Exception ex) { return BadRequest(new { respuesta = $"Error al desactivar: {ex.Message}" }); }
        }

        private static bool EsValido(EquipoDto equipo) => equipo != null && equipo.Codempresa > 0 && !string.IsNullOrWhiteSpace(equipo.Nomequipo);
    }
}
