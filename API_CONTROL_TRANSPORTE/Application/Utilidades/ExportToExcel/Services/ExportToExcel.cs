using Application.Utilidades.ExportToExcel.Interface;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utilidades.ExportToExcel.Services
{
    public class ExportToExcel : IExportToExcel
    {
        public byte[] ExportToExcels<T>(List<T> data, string sheetName = "Sheet1")
        {
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add(sheetName);

            //if (data == null || !data.Any())
            //    throw new ArgumentException("La lista está vacía o es nula.");

            var properties = typeof(T).GetProperties();
            int totalColumns = properties.Length;

            // Escribir encabezados
            for (int i = 0; i < properties.Length; i++)
            {
                worksheet.Cell(1, i + 1).Value = properties[i].Name;
                worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                worksheet.Cell(1, i + 1).Style.Fill.BackgroundColor = XLColor.LightBlue;
                worksheet.Cell(1, i + 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

            }

            // Escribir datos
            for (int row = 0; row < data.Count; row++)
            {
                for (int col = 0; col < properties.Length; col++)
                {
                    var value = properties[col].GetValue(data[row]);
                    worksheet.Cell(row + 2, col + 1).Value = value?.ToString();

                    if (row % 2 == 0)
                    {
                        worksheet.Cell(row + 2, col + 1).Style.Fill.BackgroundColor = XLColor.AliceBlue;
                    }
                    else
                    {
                        worksheet.Cell(row + 2, col + 1).Style.Fill.BackgroundColor = XLColor.White;
                    }

                    worksheet.Cell(row + 2, col + 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                }
            }

            worksheet.Columns().AdjustToContents(); // Ajustar tamaño de columnas automáticamente

            var stream = new MemoryStream();

            workbook.SaveAs(stream);

            return stream.ToArray();
        }
    }
}
