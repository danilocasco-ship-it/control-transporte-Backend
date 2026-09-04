namespace IntegracionLogin
{
    public static class RegistrarIntegracionLogin
    {
        public static IServiceCollection AgregarIntegracionLogin(
            this IServiceCollection services,
            IConfiguration configuracion)
        {
            services.Configure<OpcionesPortalLogin>(
                configuracion.GetSection(OpcionesPortalLogin.Seccion));

            services.AddSingleton<IAlmacenSesionHandoff, AlmacenSesionHandoffMemoria>();
            services.AddScoped<IValidadorTokenHandoff, ValidadorTokenHandoff>();
            services.AddScoped<IFabricaTokenNativo, FabricaTokenNativo>();
            services.AddScoped<IServicioHandoffModulo, ServicioHandoffModulo>();

            return services;
        }
    }
}
