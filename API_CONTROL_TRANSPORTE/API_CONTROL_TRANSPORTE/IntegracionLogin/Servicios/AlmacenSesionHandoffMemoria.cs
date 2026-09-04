using System.Collections.Concurrent;

namespace IntegracionLogin
{
    public class AlmacenSesionHandoffMemoria : IAlmacenSesionHandoff
    {
        private readonly ConcurrentDictionary<string, SesionHandoff> _sesiones = new();

        public void Guardar(SesionHandoff sesion)
        {
            _sesiones[sesion.Uuid] = sesion;
        }

        public bool IntentarConsumir(string uuid, out SesionHandoff? sesion)
        {
            sesion = null;
            if (!_sesiones.TryRemove(uuid, out var encontrada))
                return false;

            if (encontrada.Expiracion <= DateTime.UtcNow)
                return false;

            sesion = encontrada;
            return true;
        }
    }
}
