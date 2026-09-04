using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQL.pre_presupuesto_importacion.DTO
{
    public class PrePresupuestoItemDetalleDTO
    {
        public string CodLinea { get; set; }      // codlinea = código de obra
        public string Descripcion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Valor { get; set; }        // costo_total
        public string Observaciones { get; set; }
    }
}
