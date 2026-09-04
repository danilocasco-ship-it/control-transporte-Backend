using API_PRESUPUESTO.Configuration.ConnectionDb;
using Application.SQL.lista_presupuestos.Interface;
using Application.SQL.lista_presupuestos.Parameter;
using Microsoft.AspNetCore.Mvc;

namespace API_PRESUPUESTO.Controllers.Listapresupuesto
{
    [ApiController]
    [Route("[controller]")]
    public class ListaPresupuestoController : ControllerBase
    {
        private readonly IConnection _connection;
        private readonly string _connectionString;
        private readonly IListaPresupuestoService _service;

        public ListaPresupuestoController(IListaPresupuestoService service, IConnection connection)
        {
            _connection = connection;
            _service = service;
            _connectionString = _connection.GetConnectionString();
        }

        [HttpGet("Lista")]
        public IActionResult Lista([FromQuery] int codsucursal, [FromQuery] int codproyecto)
        {
            var param = new ListaPresupuestoParam
            {
                codsucursal = codsucursal,
                codproyecto = codproyecto
            };

            var data = _service.Lista(param, _connectionString);
            return Ok(data);
        }
    }
}
