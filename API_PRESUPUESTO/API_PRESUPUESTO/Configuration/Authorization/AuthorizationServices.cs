using Application.SQL.Login.DTO;
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

        public bool TienePermiso(List<PermisosDTO> permisos, string v)
        {
            // Verificar si el usuario tiene el permiso especificado
            return permisos.Any(p => p.nombre_permiso == v);
        }

        public bool TokenExpirado(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]); // Obtener la clave del archivo appsettings.json

                // Validar el token JWT
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true // Habilitar la validación del tiempo de expiración del token
                };

                SecurityToken validatedToken;
                var claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);

                if (validatedToken.ValidTo < DateTime.UtcNow)
                {
                    // El token ha expirado
                    return true;
                }
                else
                {
                    // El token no ha expirado
                    return false;
                }
            }
            catch (SecurityTokenExpiredException)
            {
                // El token ha expirado
                return true;
            }
            catch
            {
                // Otros errores
                return true;
            }
        }
    }
}
