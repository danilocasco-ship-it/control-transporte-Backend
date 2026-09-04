using Application.SQL.pre_proceso.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQL.pre_proceso.Interface
{
    public interface IPreProceso
    {
        List<pre_procesoDTO> GetPreProcesos(string db);
    }
}
