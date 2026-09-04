using Application.SQL.pre_presupuesto_importacion.DTO;
using Application.SQL.pre_presupuesto_importacion.Interface;
using Infrastructure.DataConnection.ConnectionSQL;
using System;
using System.Data;

namespace Application.SQL.pre_presupuesto_importacion.Service
{
    public class PrePresupuestoImportacionService : IPrePresupuestoImportacion
    {
        private readonly IDbExecutor _db;
        private const int COD_EMPRESA = 9;

        public PrePresupuestoImportacionService(IDbExecutor db)
        {
            _db = db;
        }

        public PrePresupuestoEncabezadoDTO ObtenerEncabezado(string numpre, string codfase, string codproceso, string connectionString)
        {
            var parametros = new
            {
                codempresa = COD_EMPRESA,
                numpre,
                codfase,
                codproceso
            };

            // OJO: codrubro = 2 (tu corrección)
            DataTable dt = _db.ExecuteQuery(@"
                SELECT TOP 1 correlativo
                FROM Pre_presupuesto_enc
                WHERE codempresa = @codempresa
                  AND numpre = @numpre
                  AND codfase = @codfase
                  AND codproceso = @codproceso
                  AND codrubro = 2
                ORDER BY correlativo DESC
            ", parametros, true, connectionString);

            if (dt.Rows.Count == 0)
            {
                return new PrePresupuestoEncabezadoDTO
                {
                    Existe = false,
                    Correlativo = 0,
                    Mensaje = "No existe proceso con el Rubro de Obra."
                };
            }

            int correlativo = Convert.ToInt32(dt.Rows[0]["correlativo"]);

            return new PrePresupuestoEncabezadoDTO
            {
                Existe = true,
                Correlativo = correlativo,
                Mensaje = "Encabezado encontrado."
            };
        }

        public PrePresupuestoEncabezadoDTO CargarDetalle(PrePresupuestoCargaDTO carga, string connectionString)
        {
            if (carga == null)
                return new PrePresupuestoEncabezadoDTO { Existe = false, Mensaje = "Carga inválida (null)." };

            if (string.IsNullOrWhiteSpace(carga.NumPre) ||
                string.IsNullOrWhiteSpace(carga.CodFase) ||
                string.IsNullOrWhiteSpace(carga.CodProceso))
            {
                return new PrePresupuestoEncabezadoDTO
                {
                    Existe = false,
                    Mensaje = "Debe enviar NumPre, CodFase y CodProceso."
                };
            }

            if (carga.Items == null || carga.Items.Count == 0)
            {
                return new PrePresupuestoEncabezadoDTO
                {
                    Existe = false,
                    Mensaje = "No hay ítems para cargar."
                };
            }

            // 1) Encabezado
            var enc = ObtenerEncabezado(carga.NumPre, carga.CodFase, carga.CodProceso, connectionString);
            if (!enc.Existe) return enc;

            // 2) Max linea
            var pMax = new { codempresa = COD_EMPRESA, numpre = carga.NumPre, correlativo = enc.Correlativo };

            DataTable dtMax = _db.ExecuteQuery(@"
                SELECT ISNULL(MAX(linea), 0) AS max_linea
                FROM Pre_presupuesto_det
                WHERE codempresa = @codempresa
                  AND numpre = @numpre
                  AND correlativo = @correlativo
            ", pMax, true, connectionString);

            int maxLinea = Convert.ToInt32(dtMax.Rows[0]["max_linea"]);
            int lineaActual = maxLinea;

            int insertados = 0;
            int actualizados = 0;
            int omitidos = 0;

            // 3) Por cada item -> upsert
            foreach (var item in carga.Items)
            {
                try
                {
                    if (item == null ||
                        string.IsNullOrWhiteSpace(item.CodLinea) ||
                        item.Cantidad <= 0 ||
                        item.Valor < 0)
                    {
                        omitidos++;
                        continue;
                    }

                    // 3.1 validar si existe codlinea en el correlativo
                    var pExiste = new
                    {
                        codempresa = COD_EMPRESA,
                        numpre = carga.NumPre,
                        correlativo = enc.Correlativo,
                        codlinea = item.CodLinea
                    };

                    DataTable dtExiste = _db.ExecuteQuery(@"
                        SELECT TOP 1 linea
                        FROM Pre_presupuesto_det
                        WHERE codempresa = @codempresa
                          AND numpre = @numpre
                          AND correlativo = @correlativo
                          AND codlinea = @codlinea
                    ", pExiste, true, connectionString);

                    if (dtExiste.Rows.Count > 0)
                    {
                        // UPDATE (suma cantidad y valor)
                        var pUpd = new
                        {
                            codempresa = COD_EMPRESA,
                            numpre = carga.NumPre,
                            correlativo = enc.Correlativo,
                            codlinea = item.CodLinea,
                            cantidad = item.Cantidad,
                            valor = item.Valor,
                            observaciones = item.Observaciones ?? ""
                        };

                        _db.ExecuteQuery(@"
                            UPDATE Pre_presupuesto_det
                               SET cantidad = ISNULL(cantidad,0) + @cantidad,
                                   valor    = ISNULL(valor,0) + @valor,
                                   observaciones = CASE WHEN LTRIM(RTRIM(@observaciones)) = '' THEN observaciones ELSE @observaciones END
                             WHERE codempresa = @codempresa
                               AND numpre = @numpre
                               AND correlativo = @correlativo
                               AND codlinea = @codlinea
                        ", pUpd, false, connectionString);

                        actualizados++;
                    }
                    else
                    {
                        // INSERT (linea incremental real)
                        lineaActual++;

                        var pIns = new
                        {
                            codempresa = COD_EMPRESA,
                            numpre = carga.NumPre,
                            correlativo = enc.Correlativo,
                            linea = lineaActual,
                            codlinea = item.CodLinea,
                            cantidad = item.Cantidad,
                            valor = item.Valor,
                            estado = 1,
                            observaciones = item.Observaciones ?? "",
                            origen = 5,
                            codunidadmedida = 17,
                            codcontratistamo = 0,
                            cantidadpadre = 0
                        };

                        _db.ExecuteQuery(@"
                            INSERT INTO Pre_presupuesto_det
                            (codempresa, numpre, correlativo, linea, codlinea, cantidad, valor, estado, observaciones, origen, codunidadmedida, codcontratistamo, cantidadpadre)
                            VALUES
                            (@codempresa, @numpre, @correlativo, @linea, @codlinea, @cantidad, @valor, @estado, @observaciones, @origen, @codunidadmedida, @codcontratistamo, @cantidadpadre)
                        ", pIns, false, connectionString);

                        insertados++;
                    }
                }
                catch
                {
                    // si querés log, aquí
                    omitidos++;
                }
            }

            enc.Mensaje = $"Proceso completado. Insertados: {insertados}, Actualizados: {actualizados}, Omitidos: {omitidos}.";
            return enc;
        }
    }
}
