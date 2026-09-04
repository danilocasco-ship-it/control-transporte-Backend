using Application.SQL.v_empleado.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQL.v_empleado.Interface
{
    public interface IVEmpleado
    {
        List<v_empleadoDTO> GetEmpleados(string db);
    }
}
