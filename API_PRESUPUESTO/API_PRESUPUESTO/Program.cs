


//using API_PRESUPUESTO.Extensions;
//using Application.Presupuestos.DetallePresupuesto.Interface;
//using Application.Presupuestos.DetallePresupuesto.Service;
//using Application.SQL.lista_presupuestos.Interface;
//using Application.SQL.lista_presupuestos.Service;
//using Application.SQL.Login_pre.Interface;
//using Application.SQL.Login_pre.service;
//using Application.SQL.pre_presupuesto_importacion.Interface;
//using Application.SQL.pre_presupuesto_importacion.Service;

//var builder = WebApplication.CreateBuilder(args);

//// 1) Levantar la API en local (HTTP)
////    - si solo la usarás en tu PC:
//builder.WebHost.UseUrls("http://localhost:8096");
////    - si la quieres accesible desde la red (por IP):
//// builder.WebHost.UseUrls("http://0.0.0.0:8096");

//builder.Services.AddControllers();
//builder.Services.AddLogging();

//builder.Services.AddScoped<ILogin_Services, Login_Service>();
//builder.Services.AddScoped<
//    Application.SQL.Asignaciones.ListaAsignaciones.Interface.IListaAsignaciones,
//    Application.SQL.Asignaciones.ListaAsignaciones.Service.ListaAsignacionesService
//>();

//builder.Services.AddScoped<IPrePresupuestoImportacion, PrePresupuestoImportacionService>();
//builder.Services.AddScoped<IListaPresupuestoService, ListaPresupuestoService>();
//builder.Services.AddScoped<IListaProcesos, ListaProcesosService>();

//builder.Services.AddCustomServices();

//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//// 2) CORS: aquí va el ORIGEN del FRONTEND (Angular), NO el de la API
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowSpecificOrigin", policy =>
//    {
//        policy.WithOrigins(
//                "http://localhost:8096"    // Angular en tu PC
//                // Angular por IP (si aplica) // "http://172.16.0.16:4200"   
//            )
//            .AllowAnyHeader()
//            .AllowAnyMethod()
//            .WithExposedHeaders("Content-Disposition");
//    });
//});

//var app = builder.Build();

//// Importante: UseCors antes de MapControllers
//app.UseCors("AllowSpecificOrigin");

//// app.UseHttpsRedirection(); // Déjalo comentado si estás trabajando en HTTP

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseAuthorization();

//app.MapControllers();

//app.Run();


using API_PRESUPUESTO.Extensions;
using IntegracionLogin;
using Application.Presupuestos.DetallePresupuesto.Interface;
using Application.Presupuestos.DetallePresupuesto.Service;
using Application.SQL.lista_presupuestos.Interface;
using Application.SQL.lista_presupuestos.Service;
using Application.SQL.Login_pre.Interface;
using Application.SQL.Login_pre.service;
using Application.SQL.pre_presupuesto_importacion.Interface;
using Application.SQL.pre_presupuesto_importacion.Service;

var builder = WebApplication.CreateBuilder(args);

// API local
builder.WebHost.UseUrls("http://localhost:8096");
// builder.WebHost.UseUrls("http://0.0.0.0:8096");

builder.Services.AddControllers();
builder.Services.AddLogging();

builder.Services.AddScoped<ILogin_Services, Login_Service>();
builder.Services.AddScoped<
    Application.SQL.Asignaciones.ListaAsignaciones.Interface.IListaAsignaciones,
    Application.SQL.Asignaciones.ListaAsignaciones.Service.ListaAsignacionesService
>();

builder.Services.AddScoped<IPrePresupuestoImportacion, PrePresupuestoImportacionService>();
builder.Services.AddScoped<IListaPresupuestoService, ListaPresupuestoService>();
builder.Services.AddScoped<IListaProcesos, ListaProcesosService>();

builder.Services.AddCustomServices();
builder.Services.AgregarIntegracionLogin(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins(
                "http://localhost:4200",
                "http://localhost:4201",
                "http://localhost:4203")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .WithExposedHeaders("Content-Disposition");
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigin");

// app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();