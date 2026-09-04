using API_PRESUPUESTO.Configuration.ConnectionDb;
using API_PRESUPUESTO.Configuration.JwtSercive;
using Application.SQL.Login_pre.DTO;
using Application.SQL.Login_pre.Interface;

namespace IntegracionLogin
{
    public class FabricaTokenNativo : IFabricaTokenNativo
    {
        private readonly IJwtService _jwtService;
        private readonly ILogin_Services _loginServices;
        private readonly IConnection _connection;

        public FabricaTokenNativo(
            IJwtService jwtService,
            ILogin_Services loginServices,
            IConnection connection)
        {
            _jwtService = jwtService;
            _loginServices = loginServices;
            _connection = connection;
        }

        public string Crear(DatosHandoffModulo datos)
        {
            var codempresa = datos.codempresa > 0 ? datos.codempresa : 9;
            var db = _connection.GetConnectionStringDesarrolladoras(codempresa);

            var login = _loginServices.ObtenerUsuarioHandoff(codempresa, datos.codusuario ?? string.Empty, db)
                ?? MapearDesdeHandoff(datos);

            return _jwtService.GenerarToken(login);
        }

        private static Login_DTO MapearDesdeHandoff(DatosHandoffModulo datos)
        {
            if (string.IsNullOrWhiteSpace(datos.codusuario))
                throw new InvalidOperationException("Handoff sin codusuario");

            if (!int.TryParse(datos.tipousuario, out var tipousuario))
                throw new InvalidOperationException("Handoff con tipousuario invalido");

            return new Login_DTO
            {
                tipousuario = tipousuario,
                codusuario = datos.codusuario.Trim(),
                nomusuario = datos.nomusuario ?? string.Empty
            };
        }
    }
}
