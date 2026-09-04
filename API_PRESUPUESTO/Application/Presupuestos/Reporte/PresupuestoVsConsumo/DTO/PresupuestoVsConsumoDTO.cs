using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Presupuestos.Reporte.PresupuestoVsConsumo.DTO
{
    public class PresupuestoVsConsumoDTO
    {
        public int numpre { get; set; }
        public string nompre { get; set; }
        public int codetapa { get; set; }
        public int codfase { get; set; }
        public string nomfase { get; set; }
        public int codproceso { get; set; }
        public string nomproceso { get; set; }
        public string codlinea { get; set; }
        public string nomproducto { get; set; }
        public string nomunidadmedida { get; set; }
        public decimal costopromedio { get; set; }
        public decimal CantiPre { get; set; }
        public decimal CantiConsumo { get; set; }
        public decimal TotalPre { get; set; }
        public decimal TotalConsumo { get; set; }
    }
}
