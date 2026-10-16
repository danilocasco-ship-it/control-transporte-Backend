using System.Collections.Generic;

namespace Application.Catalogos
{
    public interface ICatalogoEquipoService
    {
        List<TipoMaquinariaDto> ObtenerTiposMaquinaria(int codempresa);
        List<MarcaDto> ObtenerMarcas(int codempresa);
        List<SucursalDto> ObtenerSucursales(int codempresa);
        List<ContratistaDto> ObtenerContratistas(int codempresa);
        List<EstadoDto> ObtenerEstados(int codempresa);
    }

    public class TipoMaquinariaDto
    {
        public int Codempresa { get; set; }
        public int Codtipomaquinaria { get; set; }
        public string Nomtipomaquinaria { get; set; }
        public int? Codtipomaquinariapadre { get; set; }
    }

    public class MarcaDto
    {
        public int Codempresa { get; set; }
        public int Codmarca { get; set; }
        public string Nommarca { get; set; }
        public string Descripcion { get; set; }
    }

    public class SucursalDto { public int Codempresa { get; set; } public int Codsucursal { get; set; } public string Nomsucursal { get; set; } }
    public class ContratistaDto { public int Codempresa { get; set; } public string Codempleado { get; set; } public string Nomempleado { get; set; } public int Codestado { get; set; } }
    public class EstadoDto { public int Codempresa { get; set; } public int Codestado { get; set; } public string Nomestado { get; set; } }
}
