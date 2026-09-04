using Application.Configuration;
using Application.SQL.Asignaciones.ListaAsignaciones.DTO;
using Application.SQL.Asignaciones.ListaAsignaciones.Interface;
using Application.SQL.Asignaciones.ListaAsignaciones.Parameter;
using Infrastructure.DataConnection.ConnectionSQL;
using System;
using System.Collections.Generic;
using System.Data;

namespace Application.SQL.Asignaciones.ListaAsignaciones.Service
{
    public class ListaAsignacionesService : IListaAsignaciones
    {
        private readonly IDbExecutor _dbExecutor;
        private readonly IDataMapper _dbMapper;

        public ListaAsignacionesService(IDbExecutor dbExecutor, IDataMapper dbMapper)
        {
            _dbExecutor = dbExecutor;
            _dbMapper = dbMapper;
        }

        public List<ListaAsignacionesDTO> GetListaAsignaciones(ListaAsignacionesParameter parameter)
        {
            // C# 7.3 compatible: (object)expr ?? DBNull.Value
            var pars = new
            {
                codempresa = parameter.CodEmpresa,
                codsucursal = (object)parameter.CodSucursal ?? DBNull.Value,
                codproyecto = (object)parameter.CodProyecto ?? DBNull.Value,
                poligono = (object)parameter.poligono ?? DBNull.Value,
                lote = (object)parameter.lote ?? DBNull.Value,
                fase = (object)parameter.fase ?? DBNull.Value,
                proceso = (object)parameter.proceso ?? DBNull.Value,
            };

            DataTable dt = _dbExecutor.ExecuteStoredProcedure(
                "SP_Consulta_Contratistas",
                pars,
                returnsData: true,
                connectionString: parameter.DbConnection
            );

            return _dbMapper.MapearDataTable<ListaAsignacionesDTO>(dt);
        }
    }
}
