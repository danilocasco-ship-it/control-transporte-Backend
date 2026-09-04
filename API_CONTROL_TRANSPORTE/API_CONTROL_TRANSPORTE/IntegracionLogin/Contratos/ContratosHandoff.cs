namespace IntegracionLogin
{
    public interface IValidadorTokenHandoff
    {
        DatosHandoffModulo? Validar(string token);
    }

    public interface IAlmacenSesionHandoff
    {
        void Guardar(SesionHandoff sesion);
        bool IntentarConsumir(string uuid, out SesionHandoff? sesion);
    }

    public interface IFabricaTokenNativo
    {
        string Crear(DatosHandoffModulo datos);
    }

    public interface IServicioHandoffModulo
    {
        (bool ok, int codigoHttp, string? error, RespuestaValidarHandoff? respuesta) Validar(string? token);
        RespuestaSesionHandoff? ConsumirSesion(string? uuid);
    }
}
