using Application.SQL.Token.DTO;
using Application.WCF.InicioSesionUIF.Parameter;

namespace Application.SQL.Token.Interface
{
    public interface ITokenService
    {
        TokenDTO ExtraerToken(ParameterInicioSesion parameterToken, string db);
    }
}
