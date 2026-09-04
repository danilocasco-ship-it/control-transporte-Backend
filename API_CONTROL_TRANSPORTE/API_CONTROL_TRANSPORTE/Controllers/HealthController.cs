using Microsoft.AspNetCore.Mvc;

namespace API_CONTROL_TRANSPORTE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok(new { status = "ok", auth = "sso-portal" });
    }
}
