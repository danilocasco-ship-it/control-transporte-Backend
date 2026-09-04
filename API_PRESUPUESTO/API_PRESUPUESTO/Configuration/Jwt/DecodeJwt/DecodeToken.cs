using Application.SQL.Login.DTO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace API_PRESUPUESTO.Configuration.Jwt
{
    public class DecodeToken : IDecodeToken
    {
        public LoginDTO DecodeTokens(string token)
        {
            LoginDTO login = new LoginDTO();

            //Extraer key de appsettings.json jwt
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
                var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

                var jwtToken = securityToken as JwtSecurityToken;

                if (jwtToken != null && jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    var payload = jwtToken.Payload;

                    var id = int.Parse(payload["Id"].ToString());
                    var usuario = payload["Usuario"].ToString();
                    var permisos = payload["Permisos"].ToString();
                    var mac_adress = payload["Mac_Adress"].ToString();
                    var expiracion = payload.ValidTo;

                    login.id = id;
                    login.usuario = usuario;
                    login.mac_maquina = mac_adress;
                    login.permisos = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Collections.Generic.List<PermisosDTO>>(permisos);
                    login.expiracion = expiracion;
                }
                else
                {
                    // Token no válido
                    return null;
                }

                return login;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                Console.WriteLine($"Error al decodificar token: {ex.Message}");
                return null;
            }
        }
    }
}
