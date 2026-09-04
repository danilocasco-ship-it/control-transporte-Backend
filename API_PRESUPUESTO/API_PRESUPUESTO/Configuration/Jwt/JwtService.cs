using API_PRESUPUESTO.Configuration.ConnectionDb;
using Application.SQL.Login_pre.DTO;
using Application.SQL.Login_pre.Interface;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using API_PRESUPUESTO.Configuration.JwtSercive;
using Application.SQL.Login_pre.service;
using Application.SQL.Login.Interface;
using Application.SQL.Login.Service;

namespace API_PRESUPUESTO.Configuration.JwtSercive
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly ILoginService _loginService;
        public readonly IConnection _connection;
        private readonly string _connectionString;

        public JwtService(IConfiguration configuration, ILoginService login_service, IConnection connection)
        {
            _configuration = configuration;
            _loginService = login_service;
            _connectionString = connection.GetConnectionString();
        }

        public string GenerarToken(Login_DTO login)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            //List<PermisosDTO> modelPermisos = _loginService.Permisos(login.id, _connectionString);
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]); // Obtener la clave del archivo appsettings.json

            // Crear un objeto SecurityTokenDescriptor con la información del token



            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("tipousuario", login.tipousuario.ToString()),
                    new Claim("codusuario", login.codusuario),
                    //new Claim("Mac_Adress", login.mac_maquina),
                    //new Claim("Codigo_Vendedor", login.codigo_vendedor),
                    //new Claim("Cambio_Contrasena", login.cambio_contrasena.ToString()),
                    new Claim("nomusuario", login.nomusuario),
                    //new Claim("Cargo", login.cargo),
                    //new Claim("Permisos", JsonConvert.SerializeObject(modelPermisos)),

                }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration["Jwt:ExpireTime"])), // Obtener el tiempo de expiración del archivo appsettings.json
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // Crear un objeto AuthenticationHeaderValue con el esquema "Bearer" y el token JWT
            var authenticationHeader = new AuthenticationHeaderValue("Bearer", tokenString);

            // Convertir el objeto AuthenticationHeaderValue a su representación de cadena
            var bearerToken = authenticationHeader.ToString();

            return bearerToken;
        }

    }
}
