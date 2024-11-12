using Microsoft.EntityFrameworkCore;
using EP02_LP2.Data;
using EP02_LP2.Services;


var builder = WebApplication.CreateBuilder(args);

// Configuración de la cadena de conexión a PostgreSQL
builder.Services.AddDbContext<TenantDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar controladores y servicios adicionales
builder.Services.AddControllers();
builder.Services.AddScoped<ApartmentService>();
builder.Services.AddScoped<ElectricityConsumptionService>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Mapear los controladores
app.MapControllers();

app.Run();
