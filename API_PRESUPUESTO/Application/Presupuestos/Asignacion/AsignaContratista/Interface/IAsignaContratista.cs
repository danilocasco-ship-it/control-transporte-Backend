using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Presupuestos.Asignacion.AsignaContratista.Parametros;

namespace Application.Presupuestos.Asignacion.AsignaContratista.Interface
{
    public interface IAsignaContratista
    {
        bool Asignar(string db, AsignaContratistaParametros parametros);
    }
}