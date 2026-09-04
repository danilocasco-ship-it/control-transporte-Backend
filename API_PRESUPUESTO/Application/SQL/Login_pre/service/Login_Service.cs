using Application.Configuration;
using Application.SQL.Login_pre.DTO;
using Application.SQL.Login_pre.Interface;
using Application.SQL.Login_pre.Parameter;
using Infrastructure.DataConnection.ConnectionSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.SQL.Login_pre.service



{
    public class Login_Service : ILogin_Services
    {
        private readonly IDbExecutor _dbExecutor;
        private readonly IDataMapper _dbMapper;

        public Login_Service(IDbExecutor dbExecutor, IDataMapper dbMapper)
        {
            _dbExecutor = dbExecutor;
            _dbMapper = dbMapper;
        }

        public Login_DTO Login(Parameter_Login parameter, string db)
        {
            // 🔐 Aplicar SimpleCrypt a la clave ingresada
            string claveEncriptada = SimpleCrypt(parameter.clave);

            // ✅ Usar la clave encriptada en el SP
            DataTable dt = _dbExecutor.ExecuteStoredProcedure("VERIFICAR_AUTENTICACION", new
            {
                codempresa = parameter.codempresa,
                codusuario = parameter.codusuario,
                clave = claveEncriptada // ← este es el cambio correcto
            }, true, db);

            Login_DTO result = _dbMapper.MapDataTableToObject<Login_DTO>(dt);
            return result;
        }

        /// <summary>
        /// Obtiene los mismos datos que VERIFICAR_AUTENTICACION sin validar clave
        /// (identidad ya verificada por loginM en el handoff).
        /// </summary>
        public Login_DTO ObtenerUsuarioHandoff(int codempresa, string codusuario, string db)
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

            return _dbMapper.MapDataTableToObject<Login_DTO>(dt);
        }

        private string SimpleCrypt(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            char[] chars = input.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                int ascii = (int)chars[i];
                if (ascii < 128)
                    chars[i] = (char)(ascii + 128);
                else if (ascii > 128)
                    chars[i] = (char)(ascii - 128);
            }

            return new string(chars);
        }



        //public Response_PasswordDTO PasswordChange(Parameter_PasswordDTO parameter, string db)

        public Response_PasswordDTO PasswordChanger(Parameter_PasswordDTO parameter, string db)

        {
            DataTable dt = _dbExecutor.ExecuteStoredProcedure("CENTRIES_CAMBIO_CONTRASENA", new
            {
                id = parameter.id,
                codusuario = parameter.codusuario,
                clave = parameter.clave
            }, true, db);

            Response_PasswordDTO result = _dbMapper.MapDataTableToObject<Response_PasswordDTO>(dt);
            return result;
        }
        public List<Permisos_DTO> permisos(int Id, string db)
        {
            // Aquí va tu lógica para obtener los permisos desde la base de datos
            // Por ahora puedes dejar un return vacío para que compile
            return new List<Permisos_DTO>();
        }




    }




}
