using Application.SQL.Auth.DTO;

namespace API_CONTROL_TRANSPORTE.Configuration.Jwt
{
    public interface IDecodeToken
    {
        SesionUsuarioDTO DecodeTokens(string token);
    }
}
