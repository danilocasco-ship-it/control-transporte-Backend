using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataConnection.ConnectionWCF
{
    public class ConnectionWCF : IConnectionWCF
    {
        public async Task<string> LlamarServicioWCF(string url, string xml)
        {
            try
            {
                // Crear una instancia de HttpClient
                using (HttpClient client = new HttpClient())
                {
                    // Configurar el encabezado Content-Type
                    StringContent content = new StringContent(xml, Encoding.UTF8, "application/xml");

                    // Enviar la solicitud POST con el XML como contenido
                    HttpResponseMessage response = await client.PostAsync(url, new StringContent(xml));

                    // Verificar si la solicitud fue exitosa
                    if (response.IsSuccessStatusCode)
                    {
                        // Leer la respuesta del servicio
                        return await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        throw new Exception("Error al llamar al servicio: " + response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en LlamarServicioWCF: " + ex.Message);
            }
        }
    }
}
