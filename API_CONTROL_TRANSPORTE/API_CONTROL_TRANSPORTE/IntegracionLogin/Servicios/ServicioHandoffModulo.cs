using Microsoft.Extensions.Options;

namespace IntegracionLogin
{
    public class ServicioHandoffModulo : IServicioHandoffModulo
    {
        private readonly OpcionesPortalLogin _opciones;
        private readonly IValidadorTokenHandoff _validador;
        private readonly IFabricaTokenNativo _fabricaToken;
        private readonly IAlmacenSesionHandoff _almacen;

        public ServicioHandoffModulo(
            IOptions<OpcionesPortalLogin> opciones,
            IValidadorTokenHandoff validador,
            IFabricaTokenNativo fabricaToken,
            IAlmacenSesionHandoff almacen)
        {
            _opciones = opciones.Value;
            _validador = validador;
            _fabricaToken = fabricaToken;
            _almacen = almacen;
        }

        public (bool ok, int codigoHttp, string? error, RespuestaValidarHandoff? respuesta) Validar(string? token)
        {
            if (!_opciones.Activo)
                return (false, 503, "Integracion con loginM desactivada", null);

            if (string.IsNullOrWhiteSpace(token))
                return (false, 400, "El token es requerido", null);

            if (string.IsNullOrWhiteSpace(_opciones.UrlFrontend))
                return (false, 500, "UrlFrontend no configurada", null);

            var tokenLimpio = token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase)
                ? token[7..].Trim()
                : token.Trim();

            var datos = _validador.Validar(tokenLimpio);
            if (datos == null || string.IsNullOrWhiteSpace(datos.codusuario))
                return (false, 401, "Token de handoff invalido", null);

            // Si Slug está vacío, acepta handoffs de cualquier módulo (cuentasPorPagar / activarPresupuesto).
            if (!string.IsNullOrWhiteSpace(_opciones.Slug)
                && !string.IsNullOrWhiteSpace(datos.slug)
                && !string.Equals(datos.slug, _opciones.Slug, StringComparison.OrdinalIgnoreCase))
            {
                return (false, 401, "El token no corresponde a este modulo", null);
            }

            var tokenNativo = _fabricaToken.Crear(datos);
            var uuid = Guid.NewGuid().ToString("N");
            var segundos = Math.Max(15, _opciones.SegundosExpiracionSesion);

            _almacen.Guardar(new SesionHandoff
            {
                Uuid = uuid,
                TokenNativo = tokenNativo,
                Codusuario = datos.codusuario,
                Nomusuario = datos.nomusuario ?? string.Empty,
                Expiracion = DateTime.UtcNow.AddSeconds(segundos)
            });

            return (true, 200, null, new RespuestaValidarHandoff
            {
                url = _opciones.UrlFrontend.TrimEnd('/'),
                uuid = uuid
            });
        }

        public RespuestaSesionHandoff? ConsumirSesion(string? uuid)
        {
            if (string.IsNullOrWhiteSpace(uuid))
                return null;

            if (!_almacen.IntentarConsumir(uuid.Trim(), out var sesion) || sesion == null)
                return null;

            return new RespuestaSesionHandoff
            {
                token = sesion.TokenNativo,
                codusuario = sesion.Codusuario,
                nomusuario = sesion.Nomusuario
            };
        }
    }
}
