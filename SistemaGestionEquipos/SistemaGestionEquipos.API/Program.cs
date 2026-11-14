

using Microsoft.EntityFrameworkCore;
using SistemaGestionEquipos.Application.Contract;
using SistemaGestionEquipos.Application.Services;
using SistemaGestionEquipos.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Conexion a SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar repositorios
builder.Services.AddScoped<EquipoRepository>();
builder.Services.AddScoped<EmpleadoRepository>();

// Registrar servicios de negocio
builder.Services.AddScoped<IEquipoService, EquipoService>();
builder.Services.AddScoped<IEmpleadoService, EmpleadoService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Crear base de datos y tablas si no existen
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider
        .GetRequiredService<AppDbContext>();

    context.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
