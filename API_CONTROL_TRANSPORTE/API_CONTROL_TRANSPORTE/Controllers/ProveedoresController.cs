// API_CONTROL_TRANSPORTE/Controllers/ProveedoresController.cs


using Application.Proveedores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_CONTROL_TRANSPORTE.Controllers
{
    // Exige un JWT valido: el que emite api/Login/Login.
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProveedoresController : ControllerBase
    {
        private readonly IProveedorService _proveedorService;

        // Inyectamos el servicio que creamos
        public ProveedoresController(IProveedorService proveedorService)
        {
            _proveedorService = proveedorService;
        }

        // POST: api/Proveedores
        [HttpPost]
        public IActionResult CrearProveedor([FromBody] ProveedorDto proveedor)
        {
            try
            {
                if (proveedor == null || string.IsNullOrWhiteSpace(proveedor.Nomproveedor))
                {
                    return BadRequest(new { respuesta = "Los datos del proveedor son inválidos" });
                }

                // Llamamos al servicio para guardar en la base de datos
                _proveedorService.GuardarProveedor(proveedor);

                return Ok(new
                {
                    respuesta = "✅ Proveedor guardado exitosamente",
                    datos = proveedor
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { respuesta = $"Error al guardar: {ex.Message}" });
            }
        }

        // GET: api/Proveedores
        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            try
            {
                var lista = _proveedorService.ObtenerTodos();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(new { respuesta = $"Error al obtener proveedores: {ex.Message}" });
            }
        }

        // GET: api/Proveedores/{codempresa}/{codproveedor}
        [HttpGet("{codempresa}/{codproveedor}")]
        public IActionResult ObtenerPorId(int codempresa, int codproveedor)
        {
            try
            {
                var proveedor = _proveedorService.ObtenerPorId(codempresa, codproveedor);

                if (proveedor == null)
                    return NotFound(new { respuesta = "Proveedor no encontrado" });

                return Ok(proveedor);
            }
            catch (Exception ex)
            {
                return BadRequest(new { respuesta = $"Error al obtener proveedor: {ex.Message}" });
            }
        }

        // PUT: api/Proveedores/{codempresa}/{codproveedor}
        [HttpPut("{codempresa}/{codproveedor}")]
        public IActionResult Actualizar(int codempresa, int codproveedor, [FromBody] ProveedorDto proveedor)
        {
            try
            {
                if (proveedor == null || string.IsNullOrWhiteSpace(proveedor.Nomproveedor))
                {
                    return BadRequest(new { respuesta = "Los datos del proveedor son inválidos" });
                }

                _proveedorService.ActualizarProveedor(codempresa, codproveedor, proveedor);

                return Ok(new
                {
                    respuesta = "✅ Proveedor actualizado exitosamente",
                    datos = proveedor
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { respuesta = $"Error al actualizar: {ex.Message}" });
            }
        }

        // DELETE: api/Proveedores/{codempresa}/{codproveedor}
        [HttpDelete("{codempresa}/{codproveedor}")]
        public IActionResult Eliminar(int codempresa, int codproveedor)
        {
            try
            {
                _proveedorService.EliminarProveedor(codempresa, codproveedor);

                return Ok(new { respuesta = "✅ Proveedor eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { respuesta = $"Error al eliminar: {ex.Message}" });
            }
        }


    }
}