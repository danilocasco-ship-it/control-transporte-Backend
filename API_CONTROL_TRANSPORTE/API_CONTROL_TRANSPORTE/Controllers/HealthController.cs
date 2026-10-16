using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_CONTROL_TRANSPORTE.Controllers
{
    // Publico a proposito: sirve para comprobar disponibilidad sin credenciales.
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok(new { status = "ok", auth = "sso-portal" });
    }
}
