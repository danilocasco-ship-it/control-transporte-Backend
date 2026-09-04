using Application.Configuration;
using Application.SQL.lista_presupuestos.DTO;
using Application.SQL.lista_presupuestos.Interface;
using Application.SQL.lista_presupuestos.Parameter;
using Infrastructure.DataConnection.ConnectionSQL;
using System.Collections.Generic;
using System.Data;

namespace Application.SQL.lista_presupuestos.Service
{
    public class ListaPresupuestoService : IListaPresupuestoService
    {
        private readonly IDbExecutor _dbExecutor;
        private readonly IDataMapper _dbMapper;

        public ListaPresupuestoService(IDbExecutor dbExecutor, IDataMapper dbMapper)
        {
            _dbExecutor = dbExecutor;
            _dbMapper = dbMapper;
        }

        public List<ListaPresupuestoDTO> Lista(ListaPresupuestoParam parameter, string db)
        {
            DataTable dt = _dbExecutor.ExecuteStoredProcedure("PRE_LISTA_PRESUPUESTOS", new
            {
                codsucursal = parameter.codsucursal,
                codproyecto = parameter.codproyecto
            }, true, db);

            List<ListaPresupuestoDTO> result = _dbMapper.MapearDataTable<ListaPresupuestoDTO>(dt);

            return result;
        }
    }
}
