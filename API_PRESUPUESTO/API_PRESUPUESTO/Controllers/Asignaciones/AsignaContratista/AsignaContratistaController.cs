using API_PRESUPUESTO.Configuration.ConnectionDb;
using Application.Presupuestos.Asignacion.AsignaContratista.Interface;
using Application.Presupuestos.Asignacion.AsignaContratista.Parametros;
using Microsoft.AspNetCore.Mvc;

namespace API_PRESUPUESTO.Controllers.Asignaciones.AsignaContratista
{
    [ApiController]
    [Route("[controller]")]
    public class AsignaContratistaController : ControllerBase
    {
        public readonly IConnection _connection;
        private readonly string _connectionString;
        private readonly IAsignaContratista _service;

        public AsignaContratistaController(IAsignaContratista service, IConnection connection)
        {
            _connection = connection;
            _service = service;
            _connectionString = _connection.GetConnectionString();
        }

        [HttpPost("Asignar")]
        public IActionResult Asignar([FromBody] AsignaContratistaParametros parametros)
        {
            if (parametros.numpre <= 0)
                return BadRequest("numpre inválido");

            if (parametros.responsable <= 0)
                return BadRequest("responsable inválido");

            var result = _service.Asignar(_connectionString, parametros);

            return Ok(new
            {
                success = result,
                message = "Asignación realizada correctamente"
            });
        }
    }
}