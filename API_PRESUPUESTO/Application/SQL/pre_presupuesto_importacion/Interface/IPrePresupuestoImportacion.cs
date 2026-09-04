using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.SQL.pre_presupuesto_importacion.DTO;

namespace Application.SQL.pre_presupuesto_importacion.Interface
{
    public interface IPrePresupuestoImportacion
    {
        PrePresupuestoEncabezadoDTO ObtenerEncabezado(string numpre, string codfase, string codproceso, string connectionString);
        PrePresupuestoEncabezadoDTO CargarDetalle(PrePresupuestoCargaDTO carga, string connectionString);
    }
}
