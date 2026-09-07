using Application.SQL.Auth.DTO;
using Application.SQL.Auth.Parameter;

namespace Application.SQL.Auth.Interface
{
    public interface IServicioLoginLocal
    {
        DatosUsuarioSesion Login(Parameter_Login parameter, string db);
    }
}
