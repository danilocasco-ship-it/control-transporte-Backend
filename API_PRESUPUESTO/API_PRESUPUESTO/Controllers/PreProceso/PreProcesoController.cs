using API_PRESUPUESTO.Configuration.ConnectionDb;
using Application.SQL.pre_proceso.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API_PRESUPUESTO.Controllers.PreProceso
{
    [ApiController]
    [Route("[controller]")]
    public class PreProcesoController : ControllerBase
    {
        public readonly IConnection _connection;
        private readonly string _connectionString;
        private readonly IPreProceso _preProceso;
        public PreProcesoController(IPreProceso preProceso, IConnection connection)
        {
            _connection = connection;
            _preProceso = preProceso;
            _connectionString = _connection.GetConnectionString();
        }

        [HttpGet("Lista")]
        public dynamic Get()
        {
            var result = _preProceso.GetPreProcesos(_connectionString);

            return Ok(result);
        }
    }
}
