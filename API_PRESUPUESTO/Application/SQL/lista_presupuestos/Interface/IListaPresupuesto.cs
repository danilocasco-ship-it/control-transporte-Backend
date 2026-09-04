using Application.SQL.lista_presupuestos.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.SQL.lista_presupuestos.Parameter;

namespace Application.SQL.lista_presupuestos.Interface
{
    public interface IListaPresupuestoService
    {
        List<ListaPresupuestoDTO> Lista(ListaPresupuestoParam parameter, string db);
    }
}