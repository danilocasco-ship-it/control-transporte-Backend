using API_PRESUPUESTO.Configuration.ConnectionDb;
using Application.SQL.gen_proyecto.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API_PRESUPUESTO.Controllers.GenProyecto
{
    [ApiController]
    [Route("[controller]")]
    public class GenProyectoController : ControllerBase
    {
        public readonly IConnection _connection;
        private readonly string _connectionString;
        private readonly IGenProyecto _genProyecto;
        public GenProyectoController(IGenProyecto genProyecto, IConnection connection)
        {
            _connection = connection;
            _genProyecto = genProyecto;
            _connectionString = _connection.GetConnectionString();
        }

        //[HttpGet("Lista")]
        //public dynamic Get()
        //{
        //    var result = _genProyecto.GetProyectos(_connectionString);

        //    return Ok(result);
        //}

        [HttpGet("Lista")]
        public IActionResult Get([FromQuery] int codsucursal)
        {
            var result = _genProyecto.GetProyectos(_connectionString, codsucursal);
            return Ok(result);
        }

    }
}
