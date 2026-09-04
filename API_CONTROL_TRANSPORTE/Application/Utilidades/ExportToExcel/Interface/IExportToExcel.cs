using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utilidades.ExportToExcel.Interface
{
    public interface IExportToExcel
    {
        byte[] ExportToExcels<T>(List<T> data, string sheetName = "Sheet1");
    }
}
