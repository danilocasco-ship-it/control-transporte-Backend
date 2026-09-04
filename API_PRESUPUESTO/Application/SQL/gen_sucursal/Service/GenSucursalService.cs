using Application.Configuration;
using Application.SQL.gen_sucursal.DTO;
using Application.SQL.gen_sucursal.Interface;
using Infrastructure.DataConnection.ConnectionSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQL.gen_sucursal.Service
{
    public class GenSucursalService: IGenSucursal
    {
        private readonly IDbExecutor _dbExecutor;
        private readonly IDataMapper _dbMapper;

        public GenSucursalService(IDbExecutor dbExecutor, IDataMapper dbMapper)
        {
            _dbExecutor = dbExecutor;
            _dbMapper = dbMapper;
        }

        public List<gen_sucursalDTO> GetSucursales(string db)
        {
            DataTable dt = _dbExecutor.ExecuteStoredProcedure("GET_GEN_SUCURSALES_PRE", null, true, db);

            List<gen_sucursalDTO> result = _dbMapper.MapearDataTable<gen_sucursalDTO>(dt);

            return result;
        }
    }
}
