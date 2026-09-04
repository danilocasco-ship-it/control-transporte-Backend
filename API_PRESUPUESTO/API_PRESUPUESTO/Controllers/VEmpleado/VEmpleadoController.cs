using API_PRESUPUESTO.Configuration.ConnectionDb;
using Application.SQL.v_empleado.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API_PRESUPUESTO.Controllers.VEmpleado
{
    [ApiController]
    [Route("[controller]")]
    public class VEmpleadoController : ControllerBase
    {
        public readonly IConnection _connection;
        private readonly string _connectionString;
        private readonly IVEmpleado _vEmpleado;
        public VEmpleadoController(IVEmpleado vEmpleado, IConnection connection)
        {
            _connection = connection;
            _vEmpleado = vEmpleado;
            _connectionString = _connection.GetConnectionString();
        }

        [HttpGet("Lista")]
        public dynamic Get()
        {
            var result = _vEmpleado.GetEmpleados(_connectionString);

            return Ok(result);
        }
    }
}
