using Application.Configuration;
using Application.SQL.v_empleado.DTO;
using Application.SQL.v_empleado.Interface;
using Infrastructure.DataConnection.ConnectionSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQL.v_empleado.Service
{
    public class VEmpleadoService : IVEmpleado
    {
        private readonly IDbExecutor _dbExecutor;
        private readonly IDataMapper _dbMapper;

        public VEmpleadoService(IDbExecutor dbExecutor, IDataMapper dbMapper)
        {
            _dbExecutor = dbExecutor;
            _dbMapper = dbMapper;
        }

        public List<v_empleadoDTO> GetEmpleados(string db)
        {
            DataTable dt = _dbExecutor.ExecuteStoredProcedure("GET_V_EMPLEADO", null, true, db);

            List<v_empleadoDTO> result = _dbMapper.MapearDataTable<v_empleadoDTO>(dt);

            return result;
        }
    }
}
