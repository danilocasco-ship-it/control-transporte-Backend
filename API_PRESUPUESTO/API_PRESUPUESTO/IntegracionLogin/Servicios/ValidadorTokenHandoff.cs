using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IntegracionLogin
{
    public class ValidadorTokenHandoff : IValidadorTokenHandoff
    {
        private readonly OpcionesPortalLogin _opciones;

        public ValidadorTokenHandoff(IOptions<OpcionesPortalLogin> opciones)
        {
            _opciones = opciones.Value;
        }

        public DatosHandoffModulo? Validar(string token)
        {
            var jwt = _opciones.JwtHandoff;
            if (string.IsNullOrWhiteSpace(jwt.Key))
                return null;

            var handler = new JwtSecurityTokenHandler { MapInboundClaims = false };

            try
            {
                var principal = handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key)),
                    ValidateIssuer = true,
                    ValidIssuer = jwt.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwt.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(1)
                }, out _);

                // Si PortalLogin:Slug está vacío, acepta cualquier claim acceso_*.
                // Si tiene valor, exige exactamente acceso_{slug}.
                var claim = string.IsNullOrWhiteSpace(_opciones.Slug)
                    ? principal.Claims.FirstOrDefault(c =>
                        c.Type != null &&
                        c.Type.StartsWith("acceso_", StringComparison.OrdinalIgnoreCase) &&
                        !string.IsNullOrWhiteSpace(c.Value))
                    : principal.FindFirst(ContratoHandoffModulo.ClaimPorSlug(_opciones.Slug));

                if (claim == null || string.IsNullOrWhiteSpace(claim.Value))
                    return null;

                return JsonSerializer.Deserialize<DatosHandoffModulo>(
                    claim.Value,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch
            {
                return null;
            }
        }
    }
}
