using Application.SQL.pre_fase.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQL.pre_fase.Interface
{
    public interface IPreFase
    {
        List<pre_faseDTO> GetPreFases(string db);
    }
}
