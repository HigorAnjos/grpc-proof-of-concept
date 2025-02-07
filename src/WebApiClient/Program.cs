using CorrelationId.DependencyInjection;
using WebApiClient.DependencyInjection.Extensions;
using WebApiClient.DependencyInjection.Options;
using WebApiClient.GrpcClients;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Registra o cliente gRPC
// Modo God
builder.Services.AddGrpcClients(builder.Configuration);
// Modo JR
builder.Services.AddSingleton<GrpcAccountClient>();


// REGISTRA O SERVIÇO DE CORRELATION ID
builder.Services.AddDefaultCorrelationId(options =>
{
    options.RequestHeader = "X-Correlation-ID";
    options.ResponseHeader = "X-Correlation-ID";
    options.LoggingScopeKey = "CorrelationId";
    options.UpdateTraceIdentifier = true;
    options.AddToLoggingScope = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
