using Application.Configuration;
using Application.SQL.MapaVivienda.DTO;
using Application.SQL.MapaVivienda.Interface;
using Application.SQL.MapaVivienda.Parameter;
using Infrastructure.DataConnection.ConnectionSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQL.MapaVivienda.Service
{
    public class PresupuestoProgramacionService: IPresupuestoProgramacion
    {
        private readonly IDbExecutor _dbExecutor;
        private readonly IDataMapper _dbMapper;

        public PresupuestoProgramacionService(IDbExecutor dbExecutor, IDataMapper dbMapper)
        {
            _dbExecutor = dbExecutor;
            _dbMapper = dbMapper;
        }

        public List<PresupuestoProgramacionDTO> ListaPresupuesto(string db, ParameterMapaVivienda parametros)
        {
            DataTable dt = _dbExecutor.ExecuteStoredProcedure("GET_PRE_PRESUPUESTO_PROGRAMACION", new
            {
                codsucursal = parametros.codsucursal,
                codproyecto = parametros.codproyecto,
                poligono = parametros.poligono,
                lote = parametros.lote
            }, true, db);

            List<PresupuestoProgramacionDTO> result = _dbMapper.MapearDataTable<PresupuestoProgramacionDTO>(dt);

            return result;
        }

    }
}
