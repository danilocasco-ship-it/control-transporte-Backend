using API_PRESUPUESTO.Configuration.ConnectionDb;
using Application.SQL.Asignaciones.ListaAsignaciones.Interface;
using Application.SQL.Asignaciones.ListaAsignaciones.Parameter;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/listaasignaciones")]
public class ListaAsignacionesController : ControllerBase
{
    private readonly IListaAsignaciones _svc;
    private readonly IConnection _conn;

    public ListaAsignacionesController(IListaAsignaciones svc, IConnection conn)
    {
        _svc = svc;
        _conn = conn;
    }

    [HttpGet("consulta")]
    public IActionResult Consultar(
        [FromHeader(Name = "X-CodEmpresa")] int codempresa,
        [FromQuery] int? codsucursal,
        [FromQuery] int? codproyecto,
        [FromQuery] string poligono = null,
        [FromQuery] string lote = null,
        [FromQuery] int? fase = null,
        [FromQuery] int? proceso = null)
    {
        // Normaliza strings vacíos a null
        if (string.IsNullOrWhiteSpace(poligono)) poligono = null;
        if (string.IsNullOrWhiteSpace(lote)) lote = null;

        var param = new ListaAsignacionesParameter
        {
            CodEmpresa = codempresa,
            CodSucursal = codsucursal,
            CodProyecto = codproyecto,
            poligono = poligono,   // <= PascalCase según tu DTO
            lote = lote,       // <= PascalCase
            fase = fase,       // <= agrega estas props en el DTO si no existen
            proceso = proceso,
            DbConnection = _conn.GetConnectionStringDesarrolladoras(codempresa)
        };

        var data = _svc.GetListaAsignaciones(param);
        return Ok(data);
    }
}
