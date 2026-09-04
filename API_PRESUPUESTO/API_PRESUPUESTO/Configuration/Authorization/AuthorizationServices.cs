using Application.SQL.Auth.DTO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace API_PRESUPUESTO.Configuration.Authorization
{
    public class AuthorizationServices : IAuthorizationServices
    {
        private readonly IConfiguration _configuration;

        public AuthorizationServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool TienePermiso(List<PermisoDTO> permisos, string v)
        {
            if (permisos == null || permisos.Count == 0)
                return true; // plantilla: sin catálogo de permisos aún

            return permisos.Any(p => p.nombre_permiso == v);
        }

        public bool TokenExpirado(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                };

                SecurityToken validatedToken;
                tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);
                return validatedToken.ValidTo < DateTime.UtcNow;
            }
            catch (SecurityTokenExpiredException)
            {
                return true;
            }
            catch
            {
                return true;
            }
        }
    }
}
