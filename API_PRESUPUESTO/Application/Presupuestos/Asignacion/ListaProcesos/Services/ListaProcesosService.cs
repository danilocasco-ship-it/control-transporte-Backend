using Application.Configuration;
using Application.Presupuestos.DetallePresupuesto.DTO;
using Application.Presupuestos.DetallePresupuesto.Interface;
using Infrastructure.DataConnection.ConnectionSQL;
using System.Collections.Generic;
using System.Data;

namespace Application.Presupuestos.DetallePresupuesto.Service
{
    public class ListaProcesosService : IListaProcesos
    {
        private readonly IDbExecutor _dbExecutor;
        private readonly IDataMapper _dbMapper;

        public ListaProcesosService(IDbExecutor dbExecutor, IDataMapper dbMapper)
        {
            _dbExecutor = dbExecutor;
            _dbMapper = dbMapper;
        }

        public List<ListaProcesosDTO> GetDetalle(string db, int codempresa, int numpre)
        {
            string query = @"
                SELECT TOP (200)
                    dbo.pre_presupuesto_enc.numpre,
                    dbo.pre_presupuesto_enc.correlativo,
                    dbo.pre_presupuesto_enc.codetapa,
                    dbo.pre_etapa.nometapa,
                    dbo.pre_presupuesto_enc.codrubro,
                    dbo.pre_rubro.nomrubro,
                    dbo.pre_presupuesto_enc.codfase,
                    dbo.pre_fase.nomfase,
                    dbo.pre_presupuesto_enc.codproceso,
                    dbo.pre_proceso.nomproceso,
                    dbo.pre_presupuesto_enc.descripcion,
                    ISNULL(dbo.pre_presupuesto_enc.responsable,'') AS responsable,
                    LTRIM(RTRIM(
                        ISNULL(dbo.v_empleado.nomempleado1, '') + ' ' +
                        ISNULL(dbo.v_empleado.nomempleado2, '') + ' ' +
                        ISNULL(dbo.v_empleado.apellidosempleado1, '') + ' ' +
                        ISNULL(dbo.v_empleado.apellidosempleado2, '')
                    )) AS Contratista
                FROM dbo.pre_presupuesto_enc
                INNER JOIN dbo.pre_etapa
                    ON dbo.pre_presupuesto_enc.codetapa = dbo.pre_etapa.codetapa
                INNER JOIN dbo.pre_fase
                    ON dbo.pre_presupuesto_enc.codfase = dbo.pre_fase.codfase
                INNER JOIN dbo.pre_proceso
                    ON dbo.pre_presupuesto_enc.codproceso = dbo.pre_proceso.codproceso
                INNER JOIN dbo.pre_rubro
                    ON dbo.pre_presupuesto_enc.codrubro = dbo.pre_rubro.codrubro
                LEFT JOIN dbo.v_empleado
                    ON dbo.pre_presupuesto_enc.responsable = dbo.v_empleado.codempleado
                WHERE dbo.pre_presupuesto_enc.codempresa = @codempresa
                  AND dbo.pre_presupuesto_enc.numpre = @numpre";

            DataTable dt = _dbExecutor.ExecuteQuery(query, new
            {
                codempresa = codempresa,
                numpre = numpre
            }, true, db);

            var result = _dbMapper.MapearDataTable<ListaProcesosDTO>(dt);

            return result;
        }
    }
}