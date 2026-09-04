using Application.SQL.Auth.DTO;

namespace API_CONTROL_TRANSPORTE.Configuration.JwtSercive
{
    public interface IJwtService
    {
        string GenerarToken(DatosUsuarioSesion usuario);
    }
}
