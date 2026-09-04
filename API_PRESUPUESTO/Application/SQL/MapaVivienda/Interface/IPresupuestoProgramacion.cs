using Application.SQL.MapaVivienda.DTO;
using Application.SQL.MapaVivienda.Parameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQL.MapaVivienda.Interface
{
    public interface IPresupuestoProgramacion
    {
        List<PresupuestoProgramacionDTO> ListaPresupuesto(string db, ParameterMapaVivienda parametros);
    }
}
