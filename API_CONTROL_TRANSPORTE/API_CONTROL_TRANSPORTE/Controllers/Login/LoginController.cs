using API_CONTROL_TRANSPORTE.Configuration.ConnectionDb;
using API_CONTROL_TRANSPORTE.Configuration.JwtSercive;
using Application.SQL.Auth.Interface;
using Application.SQL.Auth.Parameter;
using Microsoft.AspNetCore.Mvc;

namespace API_CONTROL_TRANSPORTE.Controllers.Login
{
    /// <summary>
    /// Login local del módulo (desarrollo / activo=false en portal-auth).
    /// Emite el mismo JWT nativo que el handoff SSO.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IServicioLoginLocal _loginLocal;
        private readonly IConnection _connection;
        private readonly IJwtService _jwtService;

        public LoginController(
            IServicioLoginLocal loginLocal,
            IConnection connection,
            IJwtService jwtService)
        {
            _loginLocal = loginLocal;
            _connection = connection;
            _jwtService = jwtService;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] Parameter_Login parameter)
        {
            try
            {
                if (parameter == null
                    || string.IsNullOrWhiteSpace(parameter.codusuario)
                    || string.IsNullOrWhiteSpace(parameter.clave))
                {
                    return BadRequest(new { respuesta = "Usuario y contraseña son requeridos" });
                }

                var db = _connection.GetConnectionStringDesarrolladoras(parameter.codempresa);
                if (string.IsNullOrWhiteSpace(db))
                    db = _connection.GetConnectionString();

                var result = _loginLocal.Login(parameter, db);

                if (result == null || string.IsNullOrWhiteSpace(result.codusuario))
                    return BadRequest(new { respuesta = "Usuario o contraseña incorrectos" });

                var token = _jwtService.GenerarToken(result);
                if (string.IsNullOrWhiteSpace(token))
                    return BadRequest(new { respuesta = "Error al generar el token" });

                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { respuesta = ex.Message });
            }
        }
    }
}
