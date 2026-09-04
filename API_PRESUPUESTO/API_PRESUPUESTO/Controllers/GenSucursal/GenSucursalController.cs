using API_PRESUPUESTO.Configuration.ConnectionDb;
using Application.SQL.gen_sucursal.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API_PRESUPUESTO.Controllers.GenSucursal
{
    [ApiController]
    [Route("[controller]")]
    public class GenSucursalController : ControllerBase
    {
        public readonly IConnection _connection;
        private readonly string _connectionString;
        private readonly IGenSucursal _genSucursal;
        public GenSucursalController(IGenSucursal genSucursal, IConnection connection)
        {
            _connection = connection;
            _genSucursal = genSucursal;
            _connectionString = _connection.GetConnectionString();
        }

        [HttpGet("Lista")]
        public dynamic Get()
        {
            var result = _genSucursal.GetSucursales(_connectionString);

            return Ok(result);
        }
    }
}
