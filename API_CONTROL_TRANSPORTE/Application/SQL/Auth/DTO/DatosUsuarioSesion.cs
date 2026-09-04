using System;

namespace Application.SQL.Auth.DTO
{
    /// <summary>
    /// Datos de usuario para emitir el JWT nativo tras handoff SSO.
    /// </summary>
    public class DatosUsuarioSesion
    {
        public int tipousuario { get; set; }
        public string codusuario { get; set; }
        public string nomusuario { get; set; }
        public bool estado { get; set; }
        public DateTime expiracion { get; set; }
    }
}
