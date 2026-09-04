using Application.Presupuestos.DetallePresupuesto.DTO;
using System.Collections.Generic;

namespace Application.Presupuestos.DetallePresupuesto.Interface
{
    public interface IListaProcesos
    {
        List<ListaProcesosDTO> GetDetalle(string db, int codempresa, int numpre);
    }
}