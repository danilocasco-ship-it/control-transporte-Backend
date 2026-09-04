using Application.Presupuestos.Reporte.PresupuestoVsConsumo.DTO;
using Application.Presupuestos.Reporte.PresupuestoVsConsumo.Parametros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Presupuestos.Reporte.PresupuestoVsConsumo.Interfaces
{
    public interface IPresupuestoVsConsumoServices
    {
        List<PresupuestoVsConsumoDTO> ReportePresupuestoVsConsumo(PresupuestoVsConsumoParameter parameter, string conexion);
    }
}
