using Padel.API;
using Padel.Application;
using Padel.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services
    .AddInfrastructure(
        builder.Configuration,
        builder.Host,
        builder.Environment,
        builder.Logging)
    .AddApplication();

var app = builder.Build();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapHealthChecks("/health");

app.UseHttpsRedirection();

app.MapApiEndpoints();

await app.RunAsync();
