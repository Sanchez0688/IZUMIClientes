using Back.Data;
using Back.Repositories.Implementations.Clientes;
using Back.Repositories.Implementations.TiposCombos;
using Back.Repositories.Implementations.TiposCombosDetalles;
using Back.Repositories.Interfaces.Clientes;
using Back.Repositories.Interfaces.TiposCombos;
using Back.Repositories.Interfaces.TiposCombosDetalles;
using Back.Facade.Implementations.Clientes;
using Back.Facade.Implementations.TiposCombos;
using Back.Facade.Implementations.TiposCombosDetalles;
using Back.Facade.Interfaces.Clientes;
using Back.Facade.Interfaces.TiposCombos;
using Back.Facade.Interfaces.TiposCombosDetalles;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer("name=LocalConnection"));

builder.Services.AddScoped<ITiposComboDetalleRepository, TiposComboDetalleRepository>();
builder.Services.AddScoped<ITiposComboRepository, TiposComboRepository>();
builder.Services.AddScoped<IClientesRepository, ClientesRepository>();

builder.Services.AddScoped<ITiposComboFacade, TiposComboFacade>();
builder.Services.AddScoped<ITiposComboDetalleFacade, TiposComboDetalleFacade>();
builder.Services.AddScoped<IClienteFacade, ClienteFacede>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

// se modifica la seguridad
app.UseCors(x => x
		.AllowAnyMethod()
		.AllowAnyHeader()
		.SetIsOriginAllowed(origin => true)
		.AllowCredentials());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
