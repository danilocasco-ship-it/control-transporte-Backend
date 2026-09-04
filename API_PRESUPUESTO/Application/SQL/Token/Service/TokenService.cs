using Application.Configuration;
using Application.SQL.Token.DTO;
using Application.SQL.Token.Interface;
using Application.WCF.InicioSesionUIF.Interface;
using Application.WCF.InicioSesionUIF.Parameter;
using Infrastructure.DataConnection.ConnectionSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQL.Token.Service
{
    public class TokenService : ITokenService
    {
        private readonly IDbExecutor _dbExecutor;
        private readonly IDataMapper _dbMapper;
        private readonly IIniciarSesionService _iniciarSesionService;

        public TokenService(IDbExecutor dbExecutor, IDataMapper dbMapper, IIniciarSesionService iniciarSesionService)
        {
            _dbExecutor = dbExecutor;
            _dbMapper = dbMapper;
            _iniciarSesionService = iniciarSesionService;
        }
        

        public TokenDTO ExtraerToken(ParameterInicioSesion parameterToken, string db)
        {
            DataTable dt = _dbExecutor.ExecuteStoredProcedure("CENTRIES_EXTRAER_TOKEN", null, true, db);

            TokenDTO result = _dbMapper.MapDataTableToObject<TokenDTO>(dt);

            if (result.codigo == 0)
            {
                WCF.InicioSesionUIF.DTO.Result model = _iniciarSesionService.IniciarSesionUIF(parameterToken);

                if (model.codigoMensaje == 0)
                {
                    Result guardar = AddToken(model.codigoMensaje, model.descripcionMensaje, model.token, db);

                    if (guardar.codigo != 0)
                    {
                        result.codigo = guardar.codigo;
                        result.token = guardar.mensaje;
                    }

                    return result;
                }

                result.codigo = model.codigoMensaje;
                result.token = model.token;
            }

            return result;
        }

        public Result AddToken(int codigoMensaje, string descripcionMensaje, string token, string db)
        {
            DataTable dt = _dbExecutor.ExecuteStoredProcedure("CENTRIES_ADD_TOKEN", new
            {
                codigoMensaje = codigoMensaje,
                descripcionMensaje = descripcionMensaje,
                token = token
            }, true, db);

            Result result = _dbMapper.MapDataTableToObject<Result>(dt);

            return result;

        }
    }
}
