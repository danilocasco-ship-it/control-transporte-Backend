using System.Text.Json;

namespace IntegracionLogin
{
    public class DatosHandoffModulo
    {
        public int codempresa { get; set; }
        public string? codusuario { get; set; }
        public string? nomusuario { get; set; }
        public string? tipousuario { get; set; }
        public int codmodulo { get; set; }
        public string? nombre_modulo { get; set; }
        public string? slug { get; set; }
    }

    public class SesionHandoff
    {
        public string Uuid { get; set; } = string.Empty;
        public string TokenNativo { get; set; } = string.Empty;
        public string Codusuario { get; set; } = string.Empty;
        public string Nomusuario { get; set; } = string.Empty;
        public DateTime Expiracion { get; set; }
    }

    public class SolicitudValidarHandoff
    {
        public string? token { get; set; }
    }

    public class RespuestaValidarHandoff
    {
        public string url { get; set; } = string.Empty;
        public string uuid { get; set; } = string.Empty;
    }

    public class SolicitudSesionHandoff
    {
        public string? uuid { get; set; }
    }

    public class RespuestaSesionHandoff
    {
        public string token { get; set; } = string.Empty;
        public string? codusuario { get; set; }
        public string? nomusuario { get; set; }
    }
}
