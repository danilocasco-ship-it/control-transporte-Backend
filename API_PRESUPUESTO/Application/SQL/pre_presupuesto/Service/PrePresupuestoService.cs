using Application.Configuration;
using Application.SQL.pre_presupuesto.DTO;
using Application.SQL.pre_presupuesto.Interface;
using Infrastructure.DataConnection.ConnectionSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQL.pre_presupuesto.Service
{
    public class PrePresupuestoService: IPrePresupuesto
    {
        private readonly IDbExecutor _dbExecutor;
        private readonly IDataMapper _dbMapper;

        public PrePresupuestoService(IDbExecutor dbExecutor, IDataMapper dbMapper)
        {
            _dbExecutor = dbExecutor;
            _dbMapper = dbMapper;
        }

        public List<pre_presupuestoDTO> GetPrePresuspuestos(string db)
        {
            DataTable dt = _dbExecutor.ExecuteStoredProcedure("", null, true, db);

            List<pre_presupuestoDTO> result = _dbMapper.MapearDataTable<pre_presupuestoDTO>(dt);

            return result;
        }
    }
}
