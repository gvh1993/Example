using System.Globalization;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Padel.API;
using Padel.Application;
using Padel.Infrastructure;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .Enrich.WithEnvironmentName()
    .Enrich.WithProcessId()
    .Enrich.WithMachineName()
    .Enrich.WithThreadId()
    .WriteTo.Console(
        LogEventLevel.Information,
        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}",
        new CultureInfo("en-US"))
    .WriteTo.Seq(serverUrl: builder.Configuration.GetValue<string>("Seq:Url")!, formatProvider: new CultureInfo("en-US"))
    .WriteTo.OpenTelemetry(options =>
    {
        options.Endpoint = builder.Configuration.GetValue<string>("OpenTelemetry:LogsEndpoint");
        options.Protocol = Serilog.Sinks.OpenTelemetry.OtlpProtocol.Grpc;
        options.Headers = builder.Configuration.GetSection("OpenTelemetry:Headers").Get<Dictionary<string, string>>() ?? new Dictionary<string, string>();
        options.ResourceAttributes = new Dictionary<string, object>
        {
            ["service.name"] = builder.Environment.ApplicationName,
            ["service.version"] = "1.0.0",
            ["deployment.environment"] = builder.Environment.EnvironmentName
        };
    })
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddOpenTelemetry()
    .ConfigureResource(x => x.AddService(builder.Environment.ApplicationName))
    .WithTracing(tracingBuilder => tracingBuilder
        .AddHttpClientInstrumentation()
        .AddAspNetCoreInstrumentation())
    .WithMetrics(metricsBuilder => metricsBuilder
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddRuntimeInstrumentation())
    .WithLogging(loggingBuilder => loggingBuilder
        .AddOtlpExporter(otlpOptions =>
        {
            otlpOptions.Endpoint = new Uri(builder.Configuration.GetValue<string>("OpenTelemetry:LogsEndpoint")!);
        }),
    options =>
        {
            options.IncludeScopes = true;
            options.IncludeFormattedMessage = true;
            options.ParseStateValues = true;
        });

builder.Logging.AddOpenTelemetry(x =>
{
    x.IncludeScopes = true;
    x.IncludeFormattedMessage = true;
});

builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplication();

var app = builder.Build();

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapApiEndpoints();

await app.RunAsync();
