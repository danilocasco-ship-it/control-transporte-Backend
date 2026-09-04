using API_PRESUPUESTO.Configuration.ConnectionDb;
using Application.Presupuestos.DetallePresupuesto.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API_PRESUPUESTO.Controllers.Presupuestos
{
    [ApiController]
    [Route("[controller]")]
    public class ListaProcesosController : ControllerBase
    {
        public readonly IConnection _connection;
        private readonly string _connectionString;
        private readonly IListaProcesos _service;

        public ListaProcesosController(IListaProcesos service, IConnection connection)
        {
            _connection = connection;
            _service = service;
            _connectionString = _connection.GetConnectionString();
        }

        [HttpGet("Lista")]
        public IActionResult Get([FromQuery] int codempresa, [FromQuery] int numpre)
        {
            var result = _service.GetDetalle(_connectionString, codempresa, numpre);
            return Ok(result);
        }
    }
}