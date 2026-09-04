using Application.Presupuestos.Reporte.PresupuestoVsConsumoObra.DTO;
using Application.Presupuestos.Reporte.PresupuestoVsConsumoObra.Parametros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Presupuestos.Reporte.PresupuestoVsConsumoObra.Interfaces
{
    public interface IPresupuestoVsConsumoObraServices
    {
        List<PresupuestoVsConsumoObraDTO> ReportePresupuestoVsConsumoObra(PresupuestoVsConsumoObraParameter parameter, string conexion);
    }
}
