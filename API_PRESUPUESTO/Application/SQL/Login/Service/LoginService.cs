using Application.Configuration;
using Application.SQL.Login.DTO;
using Application.SQL.Login.Interface;
using Application.SQL.Login.Parameter;
using Infrastructure.DataConnection.ConnectionSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQL.Login.Service
{
    public class LoginService: ILoginService
    {
        private readonly IDbExecutor _dbExecutor;
        private readonly IDataMapper _dbMapper;

        public LoginService(IDbExecutor dbExecutor, IDataMapper dbMapper)
        {
            _dbExecutor = dbExecutor;
            _dbMapper = dbMapper;
        }

        public LoginDTO Login(ParameterLogin parameter, string db)
        {
            DataTable dt = _dbExecutor.ExecuteStoredProcedure("CENTRIES_VERIFICAR_AUTENTICACION", new
            {
                usuario = parameter.usuario,
                clave = parameter.clave
              //  mac_maquina = "" /*parameter.mac_addres*/
            }, true, db);

            LoginDTO result = _dbMapper.MapDataTableToObject<LoginDTO>(dt);

            return result;
        }

        public ResponsePasswordChangeDTO PasswordChange(ParameterPasswordChangeDTO parameter, string db)
        {
            DataTable dt = _dbExecutor.ExecuteStoredProcedure("CENTRIES_CAMBIO_CONTRASENA", new
            {
                id = parameter.id,
                usuario = parameter.usuario,
                clave = parameter.clave
            }, true, db);

            ResponsePasswordChangeDTO result = _dbMapper.MapDataTableToObject<ResponsePasswordChangeDTO>(dt);

            return result;
        }

        public List<PermisosDTO> Permisos(int Id, string db)
        {
            DataTable dt = _dbExecutor.ExecuteStoredProcedure("CENTRIES_OBTENER_PERMISOS_POR_USUARIO", new
            {
                UserId = Id
            }, true, db);

            List<PermisosDTO> result = _dbMapper.MapearDataTable<PermisosDTO>(dt);

            return result;
        }
    }
}
