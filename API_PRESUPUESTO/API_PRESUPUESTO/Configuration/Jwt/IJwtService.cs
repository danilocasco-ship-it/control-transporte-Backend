using Application.SQL.Login_pre.DTO;

namespace API_PRESUPUESTO.Configuration.JwtSercive
{
    public interface IJwtService
    {
        string GenerarToken(Login_DTO login);

    }
}
