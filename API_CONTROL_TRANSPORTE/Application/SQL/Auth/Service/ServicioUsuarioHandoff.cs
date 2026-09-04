using Application.Configuration;
using Application.SQL.Auth.DTO;
using Application.SQL.Auth.Interface;
using Infrastructure.DataConnection.ConnectionSQL;
using System.Data;

namespace Application.SQL.Auth.Service
{
    public class ServicioUsuarioHandoff : IServicioUsuarioHandoff
    {
        private readonly IDbExecutor _dbExecutor;
        private readonly IDataMapper _dbMapper;

        public ServicioUsuarioHandoff(IDbExecutor dbExecutor, IDataMapper dbMapper)
        {
            _dbExecutor = dbExecutor;
            _dbMapper = dbMapper;
        }

        public DatosUsuarioSesion ObtenerUsuarioHandoff(int codempresa, string codusuario, string db)
        {
            if (string.IsNullOrWhiteSpace(codusuario))
                return null;

            const string sql = @"
                SELECT tipousuario, codusuario, nomusuario
                FROM sg_usuario
                WHERE codempresa = @codempresa
                  AND codusuario = @codusuario
                  AND estado = 1";

            DataTable dt = _dbExecutor.ExecuteQuery(sql, new
            {
                codempresa,
                codusuario = codusuario.Trim()
            }, true, db);

            if (dt == null || dt.Rows.Count == 0)
                return null;

            return _dbMapper.MapDataTableToObject<DatosUsuarioSesion>(dt);
        }
    }
}
