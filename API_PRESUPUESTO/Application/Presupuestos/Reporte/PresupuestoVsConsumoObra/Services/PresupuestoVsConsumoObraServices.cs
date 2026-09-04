using Application.Configuration;
using Application.Presupuestos.Reporte.PresupuestoVsConsumoObra.DTO;
using Application.Presupuestos.Reporte.PresupuestoVsConsumoObra.Interfaces;
using Application.Presupuestos.Reporte.PresupuestoVsConsumoObra.Parametros;
using Infrastructure.DataConnection.ConnectionSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Presupuestos.Reporte.PresupuestoVsConsumoObra.Services
{
    public class PresupuestoVsConsumoObraServices : IPresupuestoVsConsumoObraServices
    {
        private readonly IDbExecutor _dbExecutor;
        private readonly IDataMapper _dbMapper;

        public PresupuestoVsConsumoObraServices(IDbExecutor dbExecutor, IDataMapper dbMapper)
        {
            _dbExecutor = dbExecutor;
            _dbMapper = dbMapper;
        }

        public List<PresupuestoVsConsumoObraDTO> ReportePresupuestoVsConsumoObra(PresupuestoVsConsumoObraParameter parameter, string conexion)
        {
            string sql = File.ReadAllText("Query/Presupuestos/Reportes/reportePresupuesVsConsumoObra.sql");

            DataTable dt = _dbExecutor.ExecuteQuery(sql, new
            {
                codempresa = parameter.codempresa,
                codproyecto = parameter.codproyecto,
                codsucursal = parameter.codsucursal,
                poligono = parameter.poligono,
                lote = parameter.lote
            }, true, conexion);

            List<PresupuestoVsConsumoObraDTO> resultado = _dbMapper.MapearDataTable<PresupuestoVsConsumoObraDTO>(dt);

            return resultado;
        }
    }
}
