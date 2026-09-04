using Application.SQL.gen_proyecto.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQL.gen_proyecto.Interface
{
    public interface IGenProyecto
    {
        List<gen_proyectoDTO> GetProyectos(string db, int codsucursal);
    }
}
