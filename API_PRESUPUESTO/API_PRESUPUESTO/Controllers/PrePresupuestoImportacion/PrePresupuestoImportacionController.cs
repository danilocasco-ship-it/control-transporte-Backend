using API_PRESUPUESTO.Configuration.ConnectionDb;
using Application.SQL.pre_presupuesto_importacion.DTO;
using Application.SQL.pre_presupuesto_importacion.Interface;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API_PRESUPUESTO.Controllers.PrePresupuestoImportacion
{
    [ApiController]
    [Route("[controller]")]
    public class PrePresupuestoImportacionController : ControllerBase
    {
        private readonly IConnection _connection;
        private readonly IPrePresupuestoImportacion _service;

        public PrePresupuestoImportacionController(
            IPrePresupuestoImportacion service,
            IConnection connection)
        {
            _service = service;
            _connection = connection;
        }

        // Buscar encabezado por numpre/codfase/codproceso y codrubro=2
        [HttpGet("Encabezado")]
        public IActionResult Encabezado(
            [FromQuery] string numpre,
            [FromQuery] string codfase,
            [FromQuery] string codproceso,
            [FromQuery] int codempresa)
        {
            try
            {
                var conn = _connection.GetConnectionStringDesarrolladoras(codempresa);
                if (string.IsNullOrEmpty(conn))
                    conn = _connection.GetConnectionString();

                var result = _service.ObtenerEncabezado(numpre, codfase, codproceso, conn);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Response.ContentType = "application/json";
                return StatusCode(500, new
                {
                    mensaje = ex.Message,
                    detalle = ex.InnerException?.Message,
                    stack = ex.StackTrace
                });
            }
        }

        // Cargar detalle en Pre_presupuesto_det
        [HttpPost("CargarDetalle")]
        public IActionResult CargarDetalle(
            [FromBody] PrePresupuestoCargaDTO carga,
            [FromQuery] int codempresa)
        {
            try
            {
                if (carga == null)
                    return BadRequest("Debe enviar el cuerpo de la carga.");

                var conn = _connection.GetConnectionStringDesarrolladoras(codempresa);
                if (string.IsNullOrEmpty(conn))
                    conn = _connection.GetConnectionString();

                var result = _service.CargarDetalle(carga, conn);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Response.ContentType = "application/json";
                return StatusCode(500, new
                {
                    mensaje = ex.Message,
                    detalle = ex.InnerException?.Message,
                    stack = ex.StackTrace
                });
            }
        }
    }
}
