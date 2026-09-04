using Application.SQL.Auth.DTO;

namespace API_CONTROL_TRANSPORTE.Configuration.Authorization
{
    public interface IAuthorizationServices
    {
        bool TienePermiso(List<PermisoDTO> permisos, string v);
        bool TokenExpirado(string token);
    }
}
