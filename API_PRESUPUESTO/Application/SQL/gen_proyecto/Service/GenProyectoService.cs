using Application.Configuration;
using Application.SQL.gen_proyecto.DTO;
using Application.SQL.gen_proyecto.Interface;
using DocumentFormat.OpenXml.Spreadsheet;
using Infrastructure.DataConnection.ConnectionSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQL.gen_proyecto.Service
{
    public class GenProyectoService: IGenProyecto
    {
        private readonly IDbExecutor _dbExecutor;
        private readonly IDataMapper _dbMapper;

        public GenProyectoService(IDbExecutor dbExecutor, IDataMapper dbMapper)
        {
            _dbExecutor = dbExecutor;
            _dbMapper = dbMapper;
        }

        public List<gen_proyectoDTO> GetProyectos(string db, int codsucursal)
        {
       

            DataTable dt = _dbExecutor.ExecuteStoredProcedure("GET_GEN_PROYECTOS_PRE", new {
                codsucursal = codsucursal
            }, true, db);

            List<gen_proyectoDTO> result = _dbMapper.MapearDataTable<gen_proyectoDTO>(dt);

            return result;
        }
    }
}
