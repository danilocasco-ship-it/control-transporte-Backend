using API_PRESUPUESTO.Configuration.ConnectionDb;
using Application.Presupuestos.Reporte.PresupuestoVsConsumoObra.Interfaces;
using Application.Presupuestos.Reporte.PresupuestoVsConsumoObra.Parametros;
using Application.Utilidades.ExportToExcel.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API_PRESUPUESTO.Controllers.Presupuestos.Reportes.PresupuestoVsConsumoObra
{
    [Route("api/[controller]")]
    [ApiController]
    public class PresupuestoVsConsumoObraController : ControllerBase
    {
        private readonly IPresupuestoVsConsumoObraServices _presupuestoVsConsumoObraService;
        private readonly IExportToExcel _exportToExcel;
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        public PresupuestoVsConsumoObraController(IPresupuestoVsConsumoObraServices presupuestoVsConsumoObraService,
                    IExportToExcel exportToExcel,
                    IConfiguration configuration,
                    IConnection connection)
        {
            _presupuestoVsConsumoObraService = presupuestoVsConsumoObraService;
            _exportToExcel = exportToExcel;
            _configuration = configuration;
            _connection = connection;
        }
        [HttpPost("PresupuestoVsConsumoObra")]
        public IActionResult GetPresupuestoVsConsumoObra(PresupuestoVsConsumoObraParameter parametros)
        {
            try
            {
                // Obtener la cadena de conexión desde el archivo de configuración
                var conexion = _connection.GetConnectionStringDesarrolladoras(parametros.codempresa);

                var result = _presupuestoVsConsumoObraService.ReportePresupuestoVsConsumoObra(parametros, conexion);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("ExportarExcelPresupuestoVsConsumoObra")]
        public IActionResult ExportarExcelPresupuestoVsConsumoObra(PresupuestoVsConsumoObraParameter parametros)
        {
            try
            {
                // Obtener la cadena de conexión desde el archivo de configuración
                var conexion = _connection.GetConnectionStringDesarrolladoras(parametros.codempresa);

                var result = _presupuestoVsConsumoObraService.ReportePresupuestoVsConsumoObra(parametros, conexion);

                var excelFile = _exportToExcel.ExportToExcels(result, "Reporte Presu Vs Cons Obra");

                return File(excelFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Reporte_Presupuesto_Vs_Consumo_Obra.xlsx");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
