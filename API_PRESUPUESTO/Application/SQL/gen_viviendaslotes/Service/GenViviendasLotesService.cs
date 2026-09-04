using Application.Configuration;
using Application.SQL.gen_proyecto.DTO;
using Application.SQL.gen_viviendaslotes.DTO;
using Application.SQL.gen_viviendaslotes.Interface;
using Infrastructure.DataConnection.ConnectionSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQL.gen_viviendaslotes.Service
{
    public class GenViviendasLotesService: IGenViviendasLotes
    {
        private readonly IDbExecutor _dbExecutor;
        private readonly IDataMapper _dbMapper;

        public GenViviendasLotesService(IDbExecutor dbExecutor, IDataMapper dbMapper)
        {
            _dbExecutor = dbExecutor;
            _dbMapper = dbMapper;
        }

        public List<gen_viviendaslotesDTO> GetViviendaslotes(string db)
        {
            DataTable dt = _dbExecutor.ExecuteStoredProcedure("GET_GEN_VIVIENDAS_PRE", null, true, db);

            List<gen_viviendaslotesDTO> result = _dbMapper.MapearDataTable<gen_viviendaslotesDTO>(dt);

            return result;
        }

        public List<gen_poligonoDTO> GetViviendasPoligonos(string db, int codsucursal, int codproyecto)
        {
            DataTable dt = _dbExecutor.ExecuteStoredProcedure("GET_GEN_POLIGONOS", new {

                codsucursal = codsucursal,
                codproyecto = codproyecto
            }, true,db);

            List<gen_poligonoDTO> result = _dbMapper.MapearDataTable<gen_poligonoDTO>(dt);

            return result;
        }

        public List<gen_loteDTO> Getlotes(string db, int codsucursal, int codproyecto, string codpoligonovivienda)
        {
            DataTable dt = _dbExecutor.ExecuteStoredProcedure("GET_GEN_LOTES", new {

                codsucursal = codsucursal,
                codproyecto = codproyecto,
                codpoligonovivienda = codpoligonovivienda
            },true, db);

            List<gen_loteDTO> result = _dbMapper.MapearDataTable<gen_loteDTO>(dt);
            return result;
        }
    }
}
