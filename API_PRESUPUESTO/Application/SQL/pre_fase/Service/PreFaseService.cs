using Application.Configuration;
using Application.SQL.pre_fase.DTO;
using Application.SQL.pre_fase.Interface;
using Infrastructure.DataConnection.ConnectionSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQL.pre_fase.Service
{
    public class PreFaseService: IPreFase
    {
        private readonly IDbExecutor _dbExecutor;
        private readonly IDataMapper _dbMapper;

        public PreFaseService(IDbExecutor dbExecutor, IDataMapper dbMapper)
        {
            _dbExecutor = dbExecutor;
            _dbMapper = dbMapper;
        }

        public List<pre_faseDTO> GetPreFases(string db)
        {
            DataTable dt = _dbExecutor.ExecuteStoredProcedure("GET_PRE_FASE", null, true, db);

            List<pre_faseDTO> result = _dbMapper.MapearDataTable<pre_faseDTO>(dt);

            return result;
        }
    }
}
