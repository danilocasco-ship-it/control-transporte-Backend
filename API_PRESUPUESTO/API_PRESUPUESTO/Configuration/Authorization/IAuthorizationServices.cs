using Application.SQL.Login.DTO;

namespace API_PRESUPUESTO.Configuration.Authorization
{
    public interface IAuthorizationServices
    {
        bool TienePermiso(List<PermisosDTO> permisos, string v);
        bool TokenExpirado(string token);
    }
}
