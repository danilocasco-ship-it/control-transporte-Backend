using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Presupuestos.Reporte.PresupuestoVsConsumoObra.DTO
{
    public class PresupuestoVsConsumoObraDTO
    {
        public int numpre { get; set; }
        public string nompre { get; set; }

        public string poligono { get; set; }

        public string lote { get; set; }

        public int codetapa { get; set; }
        public int codfase { get; set; }
        public string nomfase { get; set; }
        public int codproceso { get; set; }
        public string nomproceso { get; set; }
        public string codlinea { get; set; }
        public string nomobra { get; set; }
        public string nomunidadmedida { get; set; }
        public decimal valor { get; set; }
        public decimal CantidadPre { get; set; }
        public decimal CantidadPagada { get; set; }
        public decimal TotalPre { get; set; }
        public decimal TotalPago { get; set; }
    }
}
