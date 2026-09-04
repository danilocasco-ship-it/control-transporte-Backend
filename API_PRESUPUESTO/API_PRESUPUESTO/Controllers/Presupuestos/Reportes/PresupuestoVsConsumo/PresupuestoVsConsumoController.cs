using API_PRESUPUESTO.Configuration.ConnectionDb;
using Application.Presupuestos.Reporte.PresupuestoVsConsumo.Interfaces;
using Application.Presupuestos.Reporte.PresupuestoVsConsumo.Parametros;
using Application.Utilidades.ExportToExcel.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API_PRESUPUESTO.Controllers.Presupuestos.Reportes.PresupuestoVsConsumo
{
    [ApiController]
    [Route("api/[controller]")]
    public class PresupuestoVsConsumoController : ControllerBase
    {
        private readonly IPresupuestoVsConsumoServices _presupuestoVsConsumoService;
        private readonly IExportToExcel _exportToExcel;
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        public PresupuestoVsConsumoController(IPresupuestoVsConsumoServices presupuestoVsConsumoService,
                    IExportToExcel exportToExcel,
                    IConfiguration configuration, 
                    IConnection connection)
        {
            _presupuestoVsConsumoService = presupuestoVsConsumoService;
            _exportToExcel = exportToExcel;
            _configuration = configuration;
            _connection = connection;
        }
        [HttpPost("PresupuestoVsConsumo")]
        public IActionResult GetPresupuestoVsConsumo(PresupuestoVsConsumoParameter parametros)
        {
            try
            {
                // Obtener la cadena de conexión desde el archivo de configuración
                var conexion = _connection.GetConnectionStringDesarrolladoras(parametros.codempresa);

                var result = _presupuestoVsConsumoService.ReportePresupuestoVsConsumo(parametros, conexion);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("ExportarExcelPresupuestoVsConsumo")]
        public IActionResult ExportarExcelPresupuestoVsConsumo(PresupuestoVsConsumoParameter parametros)
        {
            try
            {
                // Obtener la cadena de conexión desde el archivo de configuración
                var conexion = _connection.GetConnectionStringDesarrolladoras(parametros.codempresa);

                var result = _presupuestoVsConsumoService.ReportePresupuestoVsConsumo(parametros, conexion);

                var excelFile = _exportToExcel.ExportToExcels(result, "Reporte Presupuesto Vs Consumo");

                return File(excelFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Reporte_Presupuesto_Vs_Consumo.xlsx");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
