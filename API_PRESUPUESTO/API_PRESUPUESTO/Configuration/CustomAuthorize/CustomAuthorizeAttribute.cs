using API_PRESUPUESTO.Configuration.Authorization;
using Application.SQL.Login.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using API_PRESUPUESTO.Configuration.Jwt;

namespace API_PRESUPUESTO.Configuration.CustomAuthorize
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    
    public class CustomAuthorizeAttributes : TypeFilterAttribute
    {
        public CustomAuthorizeAttributes(string permission) : base(typeof(AuthorizationFilter))
        {
            Arguments = new object[] { permission };
        }

        private class AuthorizationFilter : IAsyncAuthorizationFilter
        {
            private readonly IDecodeToken _decodeToken;
            private readonly IAuthorizationServices _authorizationService;
            private readonly string _permission;

            public AuthorizationFilter(IDecodeToken decodeToken, IAuthorizationServices authorizationService, string permission)
            {
                _decodeToken = decodeToken;
                _authorizationService = authorizationService;
                _permission = permission;
            }

            public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
            {
                try
                {
                    var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(' ').Last();
                    if (token == null)
                    {
                        context.Result = new ObjectResult(new { respuesta = "No se proporcionó el token de autorización" })
                        {
                            StatusCode = 401
                        };
                        return;
                    }

                    LoginDTO loginDTO = _decodeToken.DecodeTokens(token);


                    if (loginDTO == null || loginDTO.usuario == null)
                    {
                        context.Result = new ObjectResult(new { respuesta = "Token no válido" })
                        {
                            StatusCode = 401
                        };
                        return;
                    }


                    var permisoResult = _authorizationService.TienePermiso(loginDTO.permisos, _permission);
                    if (!permisoResult)
                    {
                        context.Result = new ObjectResult(new { respuesta = "No tiene permiso para ejecutar esta acción" })
                        {
                            StatusCode = 403
                        };
                        return;
                    }

                    var tokenResult = _authorizationService.TokenExpirado(token);
                    if (tokenResult)
                    {
                        context.Result = new ObjectResult(new { respuesta = "Su token Expiró" })
                        {
                            StatusCode = 403
                        };
                        return;
                    }
                }
                catch (Exception ex)
                {
                    context.Result = new ObjectResult(new { mensaje = ex.Message })
                    {
                        StatusCode = 500
                    };
                }
            }
        }
    }
}
