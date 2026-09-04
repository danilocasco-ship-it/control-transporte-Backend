using Application.SQL.Asignaciones.ListaAsignaciones.DTO;
using Application.SQL.Asignaciones.ListaAsignaciones.Parameter;
using System.Collections.Generic;

namespace Application.SQL.Asignaciones.ListaAsignaciones.Interface
{
    public interface IListaAsignaciones
    {
        List<ListaAsignacionesDTO> GetListaAsignaciones(ListaAsignacionesParameter param);
    }
}
