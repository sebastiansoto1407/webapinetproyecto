using Microsoft.EntityFrameworkCore;
using SistemaGestionDocumentos.API.Data;

var constructorAplicacion = WebApplication.CreateBuilder(args);

// CONFIGURAR SERVICIOS 

// Agregar DbContext con SQL Server
constructorAplicacion.Services.AddDbContext<SistemaGestionDocumentosDbContext>(opcionesConfiguración =>
    opcionesConfiguración.UseSqlServer(
        constructorAplicacion.Configuration.GetConnectionString("ConexionBaseDatosPrincipal")
    )
);

// Agregar controladores
constructorAplicacion.Services.AddControllers();

// Agregar Swagger para documentación de API
constructorAplicacion.Services.AddEndpointsApiExplorer();
constructorAplicacion.Services.AddSwaggerGen();

// Agregar CORS para permitir peticiones desde React
constructorAplicacion.Services.AddCors(opcionesConfiguracionCors =>
{
    opcionesConfiguracionCors.AddPolicy("PoliticaPermisivaCors", constructor =>
    {
        constructor
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// CONSTRUIR LA APLICACIoN 

var aplicacion = constructorAplicacion.Build();

//CONFIGURAR EL PIPELINE HTTP 

if (aplicacion.Environment.IsDevelopment())
{
    aplicacion.UseSwagger();
    aplicacion.UseSwaggerUI();
    aplicacion.UseDeveloperExceptionPage();
}

// Redirigir HTTP a HTTPS
aplicacion.UseHttpsRedirection();

// Usar CORS
aplicacion.UseCors("PoliticaPermisivaCors");

// Usar autenticacion y autorizacion
aplicacion.UseAuthentication();
aplicacion.UseAuthorization();

// Mapear controladores
aplicacion.MapControllers();

// Ejecutar la aplicación
aplicacion.Run();
