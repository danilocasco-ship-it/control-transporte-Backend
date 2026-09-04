using Application.SQL.Auth.DTO;

namespace API_PRESUPUESTO.Configuration.Jwt
{
    public interface IDecodeToken
    {
        SesionUsuarioDTO DecodeTokens(string token);
    }
}
