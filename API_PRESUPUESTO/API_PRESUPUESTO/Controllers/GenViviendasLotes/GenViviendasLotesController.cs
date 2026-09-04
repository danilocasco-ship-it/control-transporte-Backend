using API_PRESUPUESTO.Configuration.ConnectionDb;
using Application.SQL.gen_proyecto.Interface;
using Application.SQL.gen_viviendaslotes.DTO;
using Application.SQL.gen_viviendaslotes.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API_PRESUPUESTO.Controllers.GenViviendasLotes
{
    [ApiController]
    [Route("[controller]")]
    public class GenViviendasLotesController : ControllerBase
    {
        public readonly IConnection _connection;
        private readonly string _connectionString;
        private readonly IGenViviendasLotes _genViviendasLotes;

        public GenViviendasLotesController(IGenViviendasLotes genViviendasLotes, IConnection connection)
        {
            _connection = connection;
            _genViviendasLotes = genViviendasLotes;
         //   _genPoligono = genViviendasLotes;
            _connectionString = _connection.GetConnectionString();
        }

        //[HttpGet("Lista")]
        //public dynamic Get()
        //{
        //    var result = _genViviendasLotes.GetViviendaslotes(_connectionString);

        //    return Ok(result);
        //}

        //[HttpGet("Lista_poligonos")]
        //public dynamic Lista_poligonos()
        //{
        //    var result = _genViviendasLotes.GetViviendasPoligonos(_connectionString);

        //    return Ok(result);
        //}


        [HttpGet("Lista_poligonos")]
        public IActionResult Get([FromQuery] int codsucursal, int codproyecto)
        {
            var result = _genViviendasLotes.GetViviendasPoligonos(_connectionString, codsucursal, codproyecto);

            return Ok(result);
        }





        //[HttpGet("Lista_lotes")]
        //public dynamic Lista_lotes()
        //{
        //    var result = _genViviendasLotes.GetViviendaslotes(_connectionString);

        //    return Ok(result);
        //}

        [HttpGet("Lista_lotes")]
        public IActionResult Get([FromQuery] int codsucursal, int codproyecto, string codpoligonovivienda)
        {
            var result = _genViviendasLotes.Getlotes(_connectionString, codsucursal, codproyecto, codpoligonovivienda);

            return Ok(result);
        }

    }
}
