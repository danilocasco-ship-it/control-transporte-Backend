using API_PRESUPUESTO.Configuration.ConnectionDb;
using API_PRESUPUESTO.Configuration.JwtSercive;
using Application.SQL.Auth.DTO;
using Application.SQL.Auth.Interface;

namespace IntegracionLogin
{
    public class FabricaTokenNativo : IFabricaTokenNativo
    {
        private readonly IJwtService _jwtService;
        private readonly IServicioUsuarioHandoff _usuarioHandoff;
        private readonly IConnection _connection;

        public FabricaTokenNativo(
            IJwtService jwtService,
            IServicioUsuarioHandoff usuarioHandoff,
            IConnection connection)
        {
            _jwtService = jwtService;
            _usuarioHandoff = usuarioHandoff;
            _connection = connection;
        }

        public string Crear(DatosHandoffModulo datos)
        {
            var codempresa = datos.codempresa > 0 ? datos.codempresa : 1;
            var db = _connection.GetConnectionStringDesarrolladoras(codempresa);

            var usuario = _usuarioHandoff.ObtenerUsuarioHandoff(codempresa, datos.codusuario ?? string.Empty, db)
                ?? MapearDesdeHandoff(datos);

            return _jwtService.GenerarToken(usuario);
        }

        private static DatosUsuarioSesion MapearDesdeHandoff(DatosHandoffModulo datos)
        {
            if (string.IsNullOrWhiteSpace(datos.codusuario))
                throw new InvalidOperationException("Handoff sin codusuario");

            if (!int.TryParse(datos.tipousuario, out var tipousuario))
                throw new InvalidOperationException("Handoff con tipousuario invalido");

            return new DatosUsuarioSesion
            {
                tipousuario = tipousuario,
                codusuario = datos.codusuario.Trim(),
                nomusuario = datos.nomusuario ?? string.Empty
            };
        }
    }
}
