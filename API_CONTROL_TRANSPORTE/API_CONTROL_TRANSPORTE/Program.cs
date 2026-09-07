using API_CONTROL_TRANSPORTE.Extensions;
using IntegracionLogin;

var builder = WebApplication.CreateBuilder(args);

// API local — ajustar puerto del módulo
builder.WebHost.UseUrls("http://localhost:8097");

builder.Services.AddControllers();
builder.Services.AddLogging();

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
app.UseAuthorization();
app.MapControllers();

app.Run();
