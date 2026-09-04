using Application.Configuration;
using Application.SQL.Usuario.DTO;
using Application.SQL.Usuario.Interface;
using Infrastructure.DataConnection.ConnectionSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQL.Usuario.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IDbExecutor _dbExecutor;
        private readonly IDataMapper _dbMapper;

        public UsuarioService(IDbExecutor dbExecutor, IDataMapper dbMapper)
        {
            _dbExecutor = dbExecutor;
            _dbMapper = dbMapper;
        }

        public List<UsuarioDTO> ObtenerUsuariosActivos(string db)
        {
            DataTable dt = _dbExecutor.ExecuteStoredProcedure("CENTRIES_LISTA_USUARIO_ACTIVOS", null, true, db);

            List<UsuarioDTO> result = _dbMapper.MapearDataTable<UsuarioDTO>(dt);

            return result;
        }

        public List<UsuarioDTO> ObtenerUsuariosInactivos(string db)
        {
            DataTable dt = _dbExecutor.ExecuteStoredProcedure("CENTRIES_LISTA_USUARIO_INACTIVOS", null, true, db);

            List<UsuarioDTO> result = _dbMapper.MapearDataTable<UsuarioDTO>(dt);

            return result;
        }

        public UsuarioDTO ObtenerUsuarioId(string db, int? id)
        {
            DataTable dt = _dbExecutor.ExecuteStoredProcedure("CENTRIES_OBTENER_USUARIO_POR_ID", new
            {
                id = id
            }, true, db);

            UsuarioDTO result = _dbMapper.MapDataTableToObject<UsuarioDTO>(dt);

            return result;
        }

        public ResultUsuarioDTO GuardarUsuario(string db, GuardarEditarUsuarioDTO usuario)
        {
            DataTable dt = _dbExecutor.ExecuteStoredProcedure("CENTRIES_CREAR_USUARIO", new
            {
                usuario = usuario.usuario,
                codigo_vendedor = usuario.codigo_vendedor,
                mac_adrees = usuario.mac_adrees,
                nom_colaborador = usuario.nom_colaborador,
                id_cargo = usuario.id_cargo,
                id_perfil = usuario.id_perfil
            }, true, db);

            ResultUsuarioDTO result = _dbMapper.MapDataTableToObject<ResultUsuarioDTO>(dt);

            return result;
        }

        public ResultUsuarioDTO ActualizarUsuario(string db, GuardarEditarUsuarioDTO usuario)
        {
            DataTable dt = _dbExecutor.ExecuteStoredProcedure("CENTRIES_ACTUALIZAR_USUARIO", new
            {
                id = usuario.id,
                usuario = usuario.usuario,
                codigo_vendedor = usuario.codigo_vendedor,
                mac_adrees = usuario.mac_adrees,
                nom_colaborador = usuario.nom_colaborador,
                id_cargo = usuario.id_cargo,
                id_perfil = usuario.id_perfil
            }, true, db);

            ResultUsuarioDTO result = _dbMapper.MapDataTableToObject<ResultUsuarioDTO>(dt);

            return result;
        }

        public ResultUsuarioDTO DesactivarUsuario(string db, int? id)
        {
            DataTable dt = _dbExecutor.ExecuteStoredProcedure("CENTRIES_DESACTIVAR_USUARIO", new
            {
                id = id
            }, true, db);

            ResultUsuarioDTO result = _dbMapper.MapDataTableToObject<ResultUsuarioDTO>(dt);

            return result;
        }

        public ResultUsuarioDTO ActivarUsuario(string db, int? id)
        {
            DataTable dt = _dbExecutor.ExecuteStoredProcedure("CENTRIES_ACTIVAR_USUARIO", new
            {
                id = id
            }, true, db);

            ResultUsuarioDTO result = _dbMapper.MapDataTableToObject<ResultUsuarioDTO>(dt);

            return result;
        }

        public ResultUsuarioDTO ResetearClave(string db, int? id)
        {
            DataTable dt = _dbExecutor.ExecuteStoredProcedure("CENTRIES_RESET_PASSWORD", new
            {
                id = id
            }, true, db);

            ResultUsuarioDTO result = _dbMapper.MapDataTableToObject<ResultUsuarioDTO>(dt);

            return result;
        }
    }
}
