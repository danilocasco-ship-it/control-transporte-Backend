using Application.Catalogos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_CONTROL_TRANSPORTE.Controllers
{
    // Exige un JWT valido: el que emite api/Login/Login.
    [Authorize]
    [ApiController]
    [Route("api/catalogos")]
    public class CatalogosEquiposController : ControllerBase
    {
        private readonly ICatalogoEquipoService _catalogoService;

        public CatalogosEquiposController(ICatalogoEquipoService catalogoService) { _catalogoService = catalogoService; }

        [HttpGet("tipos-maquinaria/{codempresa}")]
        public IActionResult ObtenerTiposMaquinaria(int codempresa)
        {
            try { return Ok(_catalogoService.ObtenerTiposMaquinaria(codempresa)); }
            catch (Exception ex) { return BadRequest(new { respuesta = $"Error al obtener tipos de maquinaria: {ex.Message}" }); }
        }

        [HttpGet("marcas/{codempresa}")]
        public IActionResult ObtenerMarcas(int codempresa)
        {
            try { return Ok(_catalogoService.ObtenerMarcas(codempresa)); }
            catch (Exception ex) { return BadRequest(new { respuesta = $"Error al obtener marcas: {ex.Message}" }); }
        }

        [HttpGet("sucursales/{codempresa}")]
        public IActionResult ObtenerSucursales(int codempresa)
        {
            try { return Ok(_catalogoService.ObtenerSucursales(codempresa)); }
            catch (Exception ex) { return BadRequest(new { respuesta = $"Error al obtener sucursales: {ex.Message}" }); }
        }

        [HttpGet("contratistas/{codempresa}")]
        public IActionResult ObtenerContratistas(int codempresa)
        {
            try { return Ok(_catalogoService.ObtenerContratistas(codempresa)); }
            catch (Exception ex) { return BadRequest(new { respuesta = $"Error al obtener contratistas: {ex.Message}" }); }
        }

        [HttpGet("estados/{codempresa}")]
        public IActionResult ObtenerEstados(int codempresa)
        {
            try { return Ok(_catalogoService.ObtenerEstados(codempresa)); }
            catch (Exception ex) { return BadRequest(new { respuesta = $"Error al obtener estados: {ex.Message}" }); }
        }
    }
}
