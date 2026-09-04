using API_PRESUPUESTO.Configuration.ConnectionDb;
using Application.SQL.pre_obras_importacion.Interface;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;


namespace API_PRESUPUESTO.Controllers.PreObrasImportacion
{
    [ApiController]
    [Route("[controller]")]
    public class PreObrasImportacionController : ControllerBase
    {
        private readonly IConnection _connection;
        private readonly IPreObrasImportacion _service;

        public PreObrasImportacionController(
            IPreObrasImportacion service,
            IConnection connection)
        {
            _service = service;
            _connection = connection;
        }

        // ❌ SIN Authorize (así estaba cuando funcionaba)
        [HttpPost("Importar")]
        public IActionResult Importar(
       [FromForm] IFormFile archivo,
       [FromForm] int codempresa,
       [FromForm] string usuario
   )
        {
            try
            {
                if (archivo == null || archivo.Length == 0)
                    return BadRequest("Debe adjuntar un archivo Excel.");

                usuario = (usuario ?? "").Trim();

                if (string.IsNullOrWhiteSpace(usuario))
                    return BadRequest("Usuario requerido. Inicie sesión nuevamente.");

                if (usuario.Equals("SYSTEM", StringComparison.OrdinalIgnoreCase))
                    return BadRequest("Usuario inválido. Inicie sesión nuevamente.");

                var conn = _connection.GetConnectionStringDesarrolladoras(codempresa);
                if (string.IsNullOrEmpty(conn))
                    conn = _connection.GetConnectionString();

                byte[] bytes;
                using (var ms = new MemoryStream())
                {
                    archivo.CopyTo(ms);
                    bytes = ms.ToArray();
                }

                var resultado = _service.ImportarObrasDesdeExcel(bytes, conn, usuario);

                return Ok(new
                {
                    fileName = "Obras_Procesadas.xlsx",
                    contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    fileBase64 = Convert.ToBase64String(resultado.excel),
                    detalle = resultado.detalle
                });
            }
            catch (Exception ex)
            {
                Response.ContentType = "application/json";
                return StatusCode(500, new
                {
                    mensaje = ex.Message,
                    detalle = ex.InnerException?.Message,
                    stack = ex.StackTrace
                });
            }
        }


    }
}
