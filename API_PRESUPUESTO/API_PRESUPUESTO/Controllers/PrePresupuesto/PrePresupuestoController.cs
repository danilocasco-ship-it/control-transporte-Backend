using API_PRESUPUESTO.Configuration.ConnectionDb;
using Application.SQL.pre_presupuesto.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API_PRESUPUESTO.Controllers.PrePresupuesto
{
    [ApiController]
    [Route("[controller]")]
    public class PrePresupuestoController : ControllerBase
    {
        public readonly IConnection _connection;
        private readonly string _connectionString;
        private readonly IPrePresupuesto _prePresupuesto;
        public PrePresupuestoController(IPrePresupuesto prePresupuesto, IConnection connection)
        {
            _connection = connection;
            _prePresupuesto = prePresupuesto;
            _connectionString = _connection.GetConnectionString();
        }

        [HttpGet("Lista")]
        public dynamic Get()
        {
            var result = _prePresupuesto.GetPrePresuspuestos(_connectionString);

            return Ok(result);
        }
    }
}
