using Application.Configuration.SerializeToXml;
using Application.Configuration;
using Application.SQL.Login.Interface;
using Application.SQL.Login.Service;
using Application.SQL.Token.Interface;
using Application.SQL.Token.Service;
using Application.SQL.Usuario.Interface;
using Application.SQL.Usuario.Service;
using Application.Utilidades.EnvioCorreo.Interface;
using Application.Utilidades.EnvioCorreo.Services;
using Application.WCF.InicioSesionUIF.Interface;
using Application.WCF.InicioSesionUIF.Service;
using Application.WCF.ReporteEfectivoDiario.Interface;
using Application.WCF.ReporteEfectivoDiario.Service;
using Infrastructure.DataConnection.ConnectionOracle;
using Infrastructure.DataConnection.ConnectionSQL;
using Infrastructure.DataConnection.ConnectionWCF;
using API_PRESUPUESTO.Configuration.Authorization;
using API_PRESUPUESTO.Configuration.ConnectionDb;
using API_PRESUPUESTO.Configuration.Jwt;
using API_PRESUPUESTO.Configuration.JwtSercive;
using Application.SQL.gen_proyecto.Interface;
using Application.SQL.gen_proyecto.Service;
using Application.SQL.gen_sucursal.Interface;
using Application.SQL.gen_sucursal.Service;
using Application.SQL.gen_viviendaslotes.Interface;
using Application.SQL.gen_viviendaslotes.Service;
using Application.SQL.pre_fase.Interface;
using Application.SQL.pre_fase.Service;
using Application.SQL.pre_presupuesto.Interface;
using Application.SQL.pre_presupuesto.Service;
using Application.SQL.pre_proceso.Interface;
using Application.SQL.pre_proceso.Service;
using Application.SQL.v_empleado.Interface;
using Application.SQL.v_empleado.Service;
using Application.SQL.MapaVivienda.Interface;
using Application.SQL.MapaVivienda.Service;
using Application.Presupuestos.Reporte.PresupuestoVsConsumo.Services;
using Application.Presupuestos.Reporte.PresupuestoVsConsumo.Interfaces;
using Application.Utilidades.ExportToExcel.Services;
using Application.Utilidades.ExportToExcel.Interface;
using Application.Presupuestos.Reporte.PresupuestoVsConsumoObra.Services;
using Application.Presupuestos.Reporte.PresupuestoVsConsumoObra.Interfaces;


namespace API_PRESUPUESTO.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            // Registro de servicios
            services.AddScoped<IDbExecutor, SqlDbExecutor>();
            services.AddScoped<IDbOracleExecutor, OracleDbExecutor>();
            services.AddScoped<IDataMapper, DataMapper>();
            services.AddScoped<IConnectionFactory, SqlConnectionFactory>();
            services.AddScoped<IOracleConnectionFactory, OracleConnectionFactory>();
            services.AddScoped<IConnection, Connection>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IDecodeToken, DecodeToken>();
            services.AddScoped<IAuthorizationServices, AuthorizationServices>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ISerializeToXml, SerializeToXml>();
            services.AddScoped<IConnectionWCF, ConnectionWCF>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IIniciarSesionService, IniciarSesionService>();
            services.AddScoped<IReporteEfectivoDiario, ReporteEfectivoDiario>();
            services.AddScoped<ISendEmailsService, SendEmailsService>();
            services.AddScoped<ISendEmailsService, SendEmailsService>();
            services.AddScoped<IExportToExcel, ExportToExcel>();
            services.AddScoped<Application.SQL.pre_obras_importacion.Interface.IPreObrasImportacion,
                   Application.SQL.pre_obras_importacion.Service.PreObrasImportacionService>();


            //api_AdminProyects
            services.AddScoped<IGenProyecto, GenProyectoService>();
            services.AddScoped<IGenSucursal, GenSucursalService>();
            services.AddScoped<IGenViviendasLotes, GenViviendasLotesService>();
            services.AddScoped<IPreFase, PreFaseService>();
            services.AddScoped<IPrePresupuesto, PrePresupuestoService>();
            services.AddScoped<IPreProceso, PreProcesoService>();
            services.AddScoped<IVEmpleado, VEmpleadoService>();
            services.AddScoped<IPresupuestoProgramacion, PresupuestoProgramacionService>();

            //Presupuesto Reportes
            services.AddScoped<IPresupuestoVsConsumoServices, PresupuestoVsConsumoServices>();
            services.AddScoped<IPresupuestoVsConsumoObraServices, PresupuestoVsConsumoObraServices>();



            // Agrega otros registros si es necesario
            return services;
        }
    }
}
