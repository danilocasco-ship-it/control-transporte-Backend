using API_PRESUPUESTO.Configuration.ConnectionDb;
using API_PRESUPUESTO.Configuration.JwtSercive;
using Application.SQL.Login_pre.DTO;
using Application.SQL.Login_pre.Interface;
using Application.SQL.Login_pre.Parameter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_PRESUPUESTO.Controllers.Login_pre
{
    // CAMBIAMOS EL PREFIJO para evitar colisiones con LoginController
    [ApiController]
    [Route("api/[controller]")]
    public class Login_PresupuestoController : ControllerBase
    {
        private readonly ILogin_Services _login_Service;
        public readonly IConnection _connection;
        public readonly IJwtService _jwtService;
        private readonly string _connectionString;

        public Login_PresupuestoController(ILogin_Services login_Service, IConnection connection, IJwtService jwtService)
        {
            _login_Service = login_Service;
            _connection = connection;
            _jwtService = jwtService;
            _connectionString = _connection.GetConnectionString();
        }
        /// COMENTARIADO SOLO PARA PRUEBA 

        [HttpPost("Login_Pre")]
        public IActionResult Login_pre([FromBody] Parameter_Login parameter)
        {
            try
            {
                if (parameter.codusuario == null || parameter.clave == null)
                {
                    return BadRequest(new { respuesta = "usuario, contraseña y mac_addres son requeridos" });
                }

                Login_DTO result = _login_Service.Login(parameter, _connectionString);

                if (result.codusuario == null)
                {
                    return BadRequest(new { respuesta = "Usuario o contraseña incorrectos" });
                }

                var token = _jwtService.GenerarToken(result);

                if (token == null)
                {
                    return BadRequest(new { respuesta = "Error al generar el token" });
                }
                var response = new
                {
                    token = token
                };


                return Ok(response);


                //return Ok(new
                //{
                //    tipousuario = result.tipousuario,
                //    codusuario = result.codusuario
                //});
            }
            catch (Exception ex)
            {
                return BadRequest(new { respuesta = ex.Message });
            }
        }


        ///SOLO PARA PRUEBA 
        ///

        /**/



        /**/

        [HttpPost("password-change")] // NOMBRE ÚNICO PARA EVITAR CONFLICTO
        public IActionResult PasswordChange([FromBody] Parameter_PasswordDTO parameter)
        {
            try
            {
                if (parameter.id == null || parameter.codusuario == null || parameter.clave == null)
                {
                    return BadRequest(new { respuesta = "id, usuario y clave son requeridos" });
                }

                var result = _login_Service.PasswordChanger(parameter, _connectionString);

                return Ok(new { respuesta = result.respuesta });
            }
            catch (Exception ex)
            {
                return BadRequest(new { respuesta = ex.Message });
            }
        }
    }
}
