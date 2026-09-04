using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQL.pre_presupuesto_importacion.DTO
{
    public class PrePresupuestoCargaDTO
    {
        public string NumPre { get; set; }
        public string CodFase { get; set; }
        public string CodProceso { get; set; }
        public List<PrePresupuestoItemDetalleDTO> Items { get; set; }
        public string Usuario { get; set; } // opcional si querés auditar
    }
}

