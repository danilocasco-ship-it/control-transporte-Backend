using Application.Configuration;
using Application.SQL.pre_proceso.DTO;
using Application.SQL.pre_proceso.Interface;
using Infrastructure.DataConnection.ConnectionSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQL.pre_proceso.Service
{
    public class PreProcesoService : IPreProceso
    {
        private readonly IDbExecutor _dbExecutor;
        private readonly IDataMapper _dbMapper;

        public PreProcesoService(IDbExecutor dbExecutor, IDataMapper dbMapper)
        {
            _dbExecutor = dbExecutor;
            _dbMapper = dbMapper;
        }

        public List<pre_procesoDTO> GetPreProcesos(string db)
        {
            DataTable dt = _dbExecutor.ExecuteStoredProcedure("GET_PRE_PROCESO", null, true, db);

            List<pre_procesoDTO> result = _dbMapper.MapearDataTable<pre_procesoDTO>(dt);

            return result;
        }
    }
}
