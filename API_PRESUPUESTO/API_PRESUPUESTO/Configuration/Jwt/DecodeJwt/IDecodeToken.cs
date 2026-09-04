using Application.SQL.Login.DTO;

namespace API_PRESUPUESTO.Configuration.Jwt
{
    public interface IDecodeToken
    {
        LoginDTO DecodeTokens(string token);
    }
}
