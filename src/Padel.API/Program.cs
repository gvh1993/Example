using Padel.API;
using Padel.Application;
using Padel.Infrastructure;
using Padel.Infrastructure.Environment;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services
    .AddInfrastructure(
        builder.Configuration,
        builder.Host,
        builder.Environment,
        builder.Logging)
    .AddApplication();

var app = builder.Build();

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapHealthChecks("/health");

app.UseHttpsRedirection();

await app.UseInfrastructure(app.Environment);

app.MapApiEndpoints();

await app.RunAsync();
