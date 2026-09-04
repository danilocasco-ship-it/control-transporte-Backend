using Application.Utilidades.EnvioCorreo.Interface;
using Application.Utilidades.EnvioCorreo.Parameter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using API_PRESUPUESTO.Configuration.ConnectionDb;
using API_PRESUPUESTO.Configuration.CustomAuthorize;

namespace API_PRESUPUESTO.Controllers.EnviarCorreo
{
   
    [Route("[controller]")]
    [ApiController]
    public class EnviarCorreoController : ControllerBase
    {
        private readonly ISendEmailsService _SendEmailsService;
        private readonly IConfiguration _configuration;

        // public readonly IConnection _connection;
        //private readonly string _connectionString;


        public EnviarCorreoController(ISendEmailsService sendEmailService, IConfiguration configuration)
        {
            _SendEmailsService = sendEmailService;
            _configuration = configuration;
            //_connection = connection;
        }

        [HttpPost("EnviarEmail")]
       // [CustomAuthorizeAttributes("EMAIL")]
        public IActionResult EnviarEmail([FromBody] ParameterSendEmail parameter)
        {
            if (parameter == null )
            {
                return BadRequest("Parámetros incorrectos.");
            }

            // Cargar la configuración del correo desde appsettings.json
            var configEmail = new ConfigCredentialsEmail
            {
                CorreoRemitente = _configuration["CredencialesEmail:CorreoRemitente"],
                Password = _configuration["CredencialesEmail:Password"],
                Dominio = _configuration["CredencialesEmail:Dominio"],
                Port = int.Parse(_configuration["CredencialesEmail:Port"]),
                EnableSsl = bool.Parse(_configuration["CredencialesEmail:EnableSsl"])
            };

            var result = _SendEmailsService.SendEmails(parameter, configEmail);

            return Ok(result);
        }
        
    }
}
