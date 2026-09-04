using Application.SQL.pre_obras_importacion.DTO;
using System.Collections.Generic;

namespace Application.SQL.pre_obras_importacion.Interface
{
    public interface IPreObrasImportacion
    {
        (byte[] excel, List<ObraImportExcelDTO> detalle)
     ImportarObrasDesdeExcel(byte[] fileBytes, string connectionString, string usuario);

    }
}
