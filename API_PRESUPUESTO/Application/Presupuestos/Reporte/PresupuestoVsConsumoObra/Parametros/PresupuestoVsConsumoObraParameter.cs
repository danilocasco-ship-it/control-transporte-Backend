using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Presupuestos.Reporte.PresupuestoVsConsumoObra.Parametros
{
    public class PresupuestoVsConsumoObraParameter
    {
        public int codempresa { get; set; }
        public int codsucursal { get; set; }
        public int codproyecto { get; set; }
        public string poligono { get; set; }
        public string lote { get; set; }
    }
}
