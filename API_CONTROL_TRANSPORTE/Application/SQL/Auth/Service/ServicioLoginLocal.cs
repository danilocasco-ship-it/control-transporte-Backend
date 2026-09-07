using Application.Configuration;
using Application.SQL.Auth.DTO;
using Application.SQL.Auth.Interface;
using Application.SQL.Auth.Parameter;
using Infrastructure.DataConnection.ConnectionSQL;
using System.Data;

namespace Application.SQL.Auth.Service
{
    public class ServicioLoginLocal : IServicioLoginLocal
    {
        private readonly IDbExecutor _dbExecutor;
        private readonly IDataMapper _dbMapper;

        public ServicioLoginLocal(IDbExecutor dbExecutor, IDataMapper dbMapper)
        {
            _dbExecutor = dbExecutor;
            _dbMapper = dbMapper;
        }

        public DatosUsuarioSesion Login(Parameter_Login parameter, string db)
        {
            var claveEncriptada = SimpleCrypt(parameter.clave ?? string.Empty);

            DataTable dt = _dbExecutor.ExecuteStoredProcedure("VERIFICAR_AUTENTICACION", new
            {
                codempresa = parameter.codempresa,
                codusuario = parameter.codusuario,
                clave = claveEncriptada
            }, true, db);

            if (dt == null || dt.Rows.Count == 0)
                return null;

            return _dbMapper.MapDataTableToObject<DatosUsuarioSesion>(dt);
        }

        private static string SimpleCrypt(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            char[] chars = input.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                int ascii = chars[i];
                if (ascii < 128)
                    chars[i] = (char)(ascii + 128);
                else if (ascii > 128)
                    chars[i] = (char)(ascii - 128);
            }

            return new string(chars);
        }
    }
}
