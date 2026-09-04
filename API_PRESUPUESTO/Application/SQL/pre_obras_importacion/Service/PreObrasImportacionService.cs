using Application.SQL.pre_obras_importacion.DTO;
using Application.SQL.pre_obras_importacion.Interface;
using ClosedXML.Excel;
using Infrastructure.DataConnection.ConnectionSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace Application.SQL.pre_obras_importacion.Service
{
    public class PreObrasImportacionService : IPreObrasImportacion
    {
        private readonly IDbExecutor _db;

        public PreObrasImportacionService(IDbExecutor db)
        {
            _db = db;
        }

        public (byte[] excel, List<ObraImportExcelDTO> detalle)
            ImportarObrasDesdeExcel(byte[] fileBytes, string connectionString, string usuario)
        {
            // 1) Leer Excel (col A = descripcion, col B = cantidad, col C = costo_total)
            var items = new List<ObraImportExcelDTO>();

            using (var ms = new MemoryStream(fileBytes))
            using (var wb = new XLWorkbook(ms))
            {
                var ws = wb.Worksheet(1);
                var lastRow = ws.LastRowUsed()?.RowNumber() ?? 1;

                for (int r = 2; r <= lastRow; r++)
                {
                    var desc = (ws.Cell(r, 1).GetString() ?? "").Trim();
                    var cantTxt = (ws.Cell(r, 2).GetString() ?? "").Trim();
                    var costoTxt = (ws.Cell(r, 3).GetString() ?? "").Trim();

                    if (string.IsNullOrWhiteSpace(desc)) continue;

                    decimal cantidad;
                    if (!decimal.TryParse(cantTxt, out cantidad)) cantidad = 0;

                    decimal costo;
                    if (!decimal.TryParse(costoTxt, out costo)) costo = 0;

                    items.Add(new ObraImportExcelDTO
                    {
                        descripcion = desc,
                        cantidad = cantidad,
                        costo_total = costo
                    });
                }
            }

            // 2) Obtener último correlativo que inicia con 19
            DataTable dtMax = _db.ExecuteQuery(
                "SELECT ISNULL(MAX(codobra), 190000) AS Ultimo FROM pre_mano_obra WHERE CAST(codobra AS VARCHAR(20)) LIKE '19%'",
                null, true, connectionString
            );

            int correlativo = Convert.ToInt32(dtMax.Rows[0]["Ultimo"]);

            // 3) Crear Excel de salida
            var outWb = new XLWorkbook();
            var outWs = outWb.Worksheets.Add("Resultado");

            outWs.Cell(1, 1).Value = "codobra";
            outWs.Cell(1, 2).Value = "descripcion";
            outWs.Cell(1, 3).Value = "cantidad";
            outWs.Cell(1, 4).Value = "costo_total";

            var headerRange = outWs.Range(1, 1, 1, 4);

            headerRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#1F4E78");
            headerRange.Style.Font.FontColor = XLColor.White;
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            headerRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

            headerRange.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            headerRange.Style.Border.BottomBorderColor = XLColor.Black;

            outWs.Row(1).Height = 22;
            outWs.SheetView.FreezeRows(1);

            int outRow = 2;

            foreach (var it in items)
            {
                correlativo++;
                it.codobra = correlativo;

                try
                {
                    // Validaciones mínimas
                    if (it.cantidad <= 0)
                        throw new Exception("Cantidad inválida (<= 0).");

                    if (it.costo_total <= 0)
                        throw new Exception("Costo total inválido (<= 0).");

                    var parametros = new
                    {
                        codempresa = 9,
                        codobra = correlativo,
                        nomobra = it.descripcion,
                        desobra = " ",
                        precio = it.costo_total,
                        codunidad = 17,
                        codobrapadre = 1,
                        estado = 1,
                        crea = usuario
                    };

                    _db.ExecuteQuery(@"
                        INSERT INTO pre_mano_obra
                        (codempresa, codobra, nomobra, desobra, precio, codunidad, codobrapadre, estado, crea)
                        VALUES
                        (@codempresa, @codobra, @nomobra, @desobra, @precio, @codunidad, @codobrapadre, @estado, @crea)",
                        parametros, false, connectionString
                    );

                    // Excel salida
                    outWs.Cell(outRow, 1).Value = correlativo;
                    outWs.Cell(outRow, 2).Value = it.descripcion;
                    outWs.Cell(outRow, 3).Value = it.cantidad;
                    outWs.Cell(outRow, 4).Value = it.costo_total;
                }
                catch
                {
                    // Igual escribimos la fila para que el usuario la vea
                    outWs.Cell(outRow, 1).Value = correlativo;
                    outWs.Cell(outRow, 2).Value = it.descripcion;
                    outWs.Cell(outRow, 3).Value = it.cantidad;
                    outWs.Cell(outRow, 4).Value = it.costo_total;
                }

                outRow++;
            }

            outWs.Columns().AdjustToContents();

            using (var outMs = new MemoryStream())
            {
                outWb.SaveAs(outMs);
                return (outMs.ToArray(), items);
            }
        }
    }
}
