using Application.SQL.Auth.DTO;

namespace Application.SQL.Auth.Interface
{
    public interface IServicioUsuarioHandoff
    {
        /// <summary>
        /// Datos de usuario para JWT nativo. No valida credenciales
        /// (identidad ya verificada por el portal).
        /// </summary>
        DatosUsuarioSesion ObtenerUsuarioHandoff(int codempresa, string codusuario, string db);
    }
}
