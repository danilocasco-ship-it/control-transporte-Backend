using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IntegracionLogin
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/handoff")]
    public class ControladorHandoffModulo : ControllerBase
    {
        private readonly IServicioHandoffModulo _servicio;

        public ControladorHandoffModulo(IServicioHandoffModulo servicio)
        {
            _servicio = servicio;
        }

        [HttpPost("validar")]
        public IActionResult Validar([FromBody] SolicitudValidarHandoff? solicitud)
        {
            var resultado = _servicio.Validar(solicitud?.token);
            if (!resultado.ok)
                return StatusCode(resultado.codigoHttp, new { respuesta = resultado.error });

            return Ok(resultado.respuesta);
        }

        [HttpPost("sesion")]
        public IActionResult Sesion([FromBody] SolicitudSesionHandoff? solicitud)
        {
            var sesion = _servicio.ConsumirSesion(solicitud?.uuid);
            if (sesion == null)
                return Unauthorized(new { respuesta = "Sesion invalida, expirada o ya usada" });

            return Ok(sesion);
        }
    }
}
