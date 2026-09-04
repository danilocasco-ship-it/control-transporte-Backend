using API_PRESUPUESTO.Configuration.ConnectionDb;
using Application.SQL.MapaVivienda.Interface;
using Application.SQL.MapaVivienda.Parameter;
using Application.SQL.pre_presupuesto.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API_PRESUPUESTO.Controllers.MapaVivienda
{
    [ApiController]
    [Route("[controller]")]
    public class MapaViviendaController : ControllerBase
    {
        public readonly IConnection _connection;
        private readonly string _connectionString;
        private readonly IPresupuestoProgramacion _preProgramacion;
        public MapaViviendaController(IPresupuestoProgramacion preProgramacion, IConnection connection)
        {
            _connection = connection;
            _preProgramacion = preProgramacion;
            _connectionString = _connection.GetConnectionString();
        }

        [HttpGet("Lista")]
        public IActionResult Get([FromQuery] ParameterMapaVivienda parametros)
        {
            var result = _preProgramacion.ListaPresupuesto(_connectionString, parametros );

            return Ok(result);
        }
    }
}
