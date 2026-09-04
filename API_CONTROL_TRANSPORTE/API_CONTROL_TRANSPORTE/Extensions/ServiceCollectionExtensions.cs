using Application.Configuration;
using Application.SQL.Auth.Interface;
using Application.SQL.Auth.Service;
using Application.Utilidades.EnvioCorreo.Interface;
using Application.Utilidades.EnvioCorreo.Services;
using Application.Utilidades.ExportToExcel.Interface;
using Application.Utilidades.ExportToExcel.Services;
using Application.Configuration.SerializeToXml;
using Infrastructure.DataConnection.ConnectionOracle;
using Infrastructure.DataConnection.ConnectionSQL;
using Infrastructure.DataConnection.ConnectionWCF;
using API_CONTROL_TRANSPORTE.Configuration.Authorization;
using API_CONTROL_TRANSPORTE.Configuration.ConnectionDb;
using API_CONTROL_TRANSPORTE.Configuration.Jwt;
using API_CONTROL_TRANSPORTE.Configuration.JwtSercive;

namespace API_CONTROL_TRANSPORTE.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IDbExecutor, SqlDbExecutor>();
            services.AddScoped<IDbOracleExecutor, OracleDbExecutor>();
            services.AddScoped<IDataMapper, DataMapper>();
            services.AddScoped<IConnectionFactory, SqlConnectionFactory>();
            services.AddScoped<IOracleConnectionFactory, OracleConnectionFactory>();
            services.AddScoped<IConnection, Connection>();
            services.AddScoped<IConnectionWCF, ConnectionWCF>();

            services.AddScoped<IServicioUsuarioHandoff, ServicioUsuarioHandoff>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IDecodeToken, DecodeToken>();
            services.AddScoped<IAuthorizationServices, AuthorizationServices>();

            services.AddScoped<ISerializeToXml, SerializeToXml>();
            services.AddScoped<ISendEmailsService, SendEmailsService>();
            services.AddScoped<IExportToExcel, ExportToExcel>();

            return services;
        }
    }
}
