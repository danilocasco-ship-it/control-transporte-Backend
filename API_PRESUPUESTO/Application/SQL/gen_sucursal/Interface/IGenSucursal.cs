using Application.SQL.gen_sucursal.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQL.gen_sucursal.Interface
{
    public interface IGenSucursal
    {
        List<gen_sucursalDTO> GetSucursales(string db);
    }
}
