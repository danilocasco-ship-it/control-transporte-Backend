using Application.SQL.gen_viviendaslotes.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQL.gen_viviendaslotes.Interface
{
    public interface IGenViviendasLotes
    {
        List<gen_viviendaslotesDTO> GetViviendaslotes(string db);
        List<gen_poligonoDTO> GetViviendasPoligonos(string db, int codsucursal, int codproyecto);
        List<gen_loteDTO> Getlotes(string db, int codsucursal, int codproyecto, string codpoligonovivienda);
    }

}
