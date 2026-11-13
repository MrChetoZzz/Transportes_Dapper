using TransportOrders.Api.Data; // Asegúrate de tener la clase DapperService y DapperContext que te dio el profesor
using TransportOrders.Api.Repositories;
using TransportOrders.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registros para Dapper (Código de tu PDF)
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<IDapperService, DapperService>();

// Tus nuevos registros
builder.Services.AddScoped<ITransportOrderRepository, TransportOrderRepository>();
builder.Services.AddScoped<ITransportOrderService, TransportOrderService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();