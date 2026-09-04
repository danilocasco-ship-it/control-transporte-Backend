using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Presupuestos.Reporte.PresupuestoVsConsumo.Parametros
{
    public class PresupuestoVsConsumoParameter
    {
        public int codempresa { get; set; }
        public int codsucursal { get; set; }
        public int codproyecto { get; set; }
        public string poligono { get; set; }
        public string lote { get; set; }
    }
}
