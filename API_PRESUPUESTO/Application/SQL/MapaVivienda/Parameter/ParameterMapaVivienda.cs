using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQL.MapaVivienda.Parameter
{
    public class ParameterMapaVivienda
    {
        [JsonProperty("codsucursal")]
        public int codsucursal { get; set; }

        [JsonProperty("codproyecto")]
        public int codproyecto { get; set; }

        [JsonProperty("poligono")]
        public string poligono { get; set; }

        [JsonProperty("lote")]
        public string lote { get; set; }
    }
}
