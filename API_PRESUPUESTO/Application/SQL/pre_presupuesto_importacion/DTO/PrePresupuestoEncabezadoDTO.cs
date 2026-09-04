using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQL.pre_presupuesto_importacion.DTO
{
    public class PrePresupuestoEncabezadoDTO
    {
        public bool Existe { get; set; }
        public int Correlativo { get; set; }
        public string Mensaje { get; set; }
    }
}

