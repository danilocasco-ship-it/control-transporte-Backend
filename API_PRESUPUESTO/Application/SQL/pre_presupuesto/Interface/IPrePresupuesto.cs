using Application.SQL.pre_presupuesto.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQL.pre_presupuesto.Interface
{
    public interface IPrePresupuesto
    {
        List<pre_presupuestoDTO> GetPrePresuspuestos(string db);
    }
}
