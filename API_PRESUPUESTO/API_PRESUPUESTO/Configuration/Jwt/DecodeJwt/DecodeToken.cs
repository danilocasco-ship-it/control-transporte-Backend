using Application.SQL.Auth.DTO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace API_PRESUPUESTO.Configuration.Jwt
{
    public class DecodeToken : IDecodeToken
    {
        public SesionUsuarioDTO DecodeTokens(string token)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var key = configuration.GetSection("Jwt").GetSection("Key").Value;

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                SecurityToken securityToken;
                tokenHandler.ValidateToken(token, validationParameters, out securityToken);

                var jwtToken = securityToken as JwtSecurityToken;

                if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    return null;

                var payload = jwtToken.Payload;
                int.TryParse(payload.ContainsKey("tipousuario") ? payload["tipousuario"]?.ToString() : "0", out var tipousuario);

                return new SesionUsuarioDTO
                {
                    tipousuario = tipousuario,
                    codusuario = payload.ContainsKey("codusuario") ? payload["codusuario"]?.ToString() : string.Empty,
                    nomusuario = payload.ContainsKey("nomusuario") ? payload["nomusuario"]?.ToString() : string.Empty,
                    expiracion = payload.ValidTo,
                    permisos = new List<PermisoDTO>()
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al decodificar token: {ex.Message}");
                return null;
            }
        }
    }
}
