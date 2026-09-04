using Application.Configuration;
using Application.Presupuestos.Reporte.PresupuestoVsConsumo.DTO;
using Application.Presupuestos.Reporte.PresupuestoVsConsumo.Interfaces;
using Application.Presupuestos.Reporte.PresupuestoVsConsumo.Parametros;
using Infrastructure.DataConnection.ConnectionSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Presupuestos.Reporte.PresupuestoVsConsumo.Services
{
    public class PresupuestoVsConsumoServices : IPresupuestoVsConsumoServices
    {
        private readonly IDbExecutor _dbExecutor;
        private readonly IDataMapper _dbMapper;

        public PresupuestoVsConsumoServices(IDbExecutor dbExecutor, IDataMapper dbMapper)
        {
            _dbExecutor = dbExecutor;
            _dbMapper = dbMapper;
        }

        public List<PresupuestoVsConsumoDTO> ReportePresupuestoVsConsumo(PresupuestoVsConsumoParameter parameter, string conexion)
        {
            string sql = File.ReadAllText("Query/Presupuestos/Reportes/reportePresupuesVsConsumo.sql");

            DataTable dt = _dbExecutor.ExecuteQuery(sql, new
            {
                codempresa = parameter.codempresa,
                codproyecto = parameter.codproyecto,
                codsucursal = parameter.codsucursal,
                poligono = parameter.poligono,
                lote = parameter.lote
            }, true, conexion);
            
            List<PresupuestoVsConsumoDTO> resultado = _dbMapper.MapearDataTable<PresupuestoVsConsumoDTO>(dt);

            return resultado;
        }
    }
}
