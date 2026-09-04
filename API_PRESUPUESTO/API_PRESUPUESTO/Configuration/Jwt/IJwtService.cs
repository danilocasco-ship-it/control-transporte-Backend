using Application.SQL.Auth.DTO;

namespace API_PRESUPUESTO.Configuration.JwtSercive
{
    public interface IJwtService
    {
        string GenerarToken(DatosUsuarioSesion usuario);
    }
}
