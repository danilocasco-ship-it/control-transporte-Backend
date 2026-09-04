using API_PRESUPUESTO.Configuration.ConnectionDb;
using Application.SQL.pre_fase.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API_PRESUPUESTO.Controllers.PreFase
{
    [ApiController]
    [Route("[controller]")]
    public class PreFaseController : ControllerBase
    {
        public readonly IConnection _connection;
        private readonly string _connectionString;
        private readonly IPreFase _preFase;
        public PreFaseController(IPreFase preFase, IConnection connection)
        {
            _connection = connection;
            _preFase = preFase;
            _connectionString = _connection.GetConnectionString();
        }

        [HttpGet("Lista")]
        public dynamic Get()
        {
            var result = _preFase.GetPreFases(_connectionString);

            return Ok(result);
        }
    }
}
