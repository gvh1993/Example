using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Padel.Infrastructure.Data;
using Padel.Infrastructure.Monitoring;
using Serilog;
using Serilog.Events;

namespace Padel.Infrastructure;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddInfrastructure(
            IConfiguration configuration,
            ConfigureHostBuilder host,
            IHostEnvironment environment,
            ILoggingBuilder loggingBuilder)
        {
            services.AddData(configuration)
                .AddLogging(configuration, host, environment, loggingBuilder);

            return services;
        }

        public IServiceCollection AddData(IConfiguration configuration)
        {
            var padelConnectionString = configuration.GetConnectionString("Padel")
                                        ?? throw new InvalidOperationException("The Padel database connectionstring should not be null");

            services.AddDbContext<PadelDbContext>(x => x
                .UseNpgsql(padelConnectionString, y => y.EnableRetryOnFailure()));

            services.AddHealthChecks()
                .AddNpgSql(padelConnectionString);

            return services;
        }

        public void AddLogging(
            IConfiguration configuration,
            ConfigureHostBuilder host,
            IHostEnvironment environment,
            ILoggingBuilder loggingBuilder)
        {
            var openTelemetryOptions = new OpenTelemetryOptions();
            configuration.GetSection(OpenTelemetryOptions.OpenTelemetryConfigSection)
                .Bind(openTelemetryOptions);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .Enrich.WithEnvironmentName()
                .Enrich.WithProcessId()
                .Enrich.WithMachineName()
                .Enrich.WithThreadId()
                .WriteTo.Console(
                    LogEventLevel.Information,
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}",
                    new CultureInfo("en-US"))
                .WriteTo.Seq(serverUrl: configuration.GetValue<string>("Seq:Url")!, formatProvider: new CultureInfo("en-US"))
                .WriteTo.OpenTelemetry(options =>
                {
                    options.Endpoint = openTelemetryOptions.LogsEndpoint.AbsoluteUri;
                    options.Protocol = Serilog.Sinks.OpenTelemetry.OtlpProtocol.Grpc;
                    options.Headers = configuration.GetSection("OpenTelemetry:Headers").Get<Dictionary<string, string>>() ?? new Dictionary<string, string>();
                    options.ResourceAttributes = new Dictionary<string, object>
                    {
                        ["service.name"] = environment.ApplicationName,
                        ["service.version"] = "1.0.0",
                        ["deployment.environment"] = environment.EnvironmentName
                    };
                })
                .CreateLogger();

            host.UseSerilog();

            services.AddOpenTelemetry()
                .ConfigureResource(x => x.AddService(environment.ApplicationName))
                .WithTracing(tracingBuilder => tracingBuilder
                    .AddHttpClientInstrumentation()
                    .AddAspNetCoreInstrumentation()
                    .AddNpgsql()
                    .AddOtlpExporter(otlpOptions =>
                    {
                        otlpOptions.Endpoint = openTelemetryOptions.TracesEndpoint;
                    }))
                .WithMetrics(metricsBuilder => metricsBuilder
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddRuntimeInstrumentation()
                    .AddOtlpExporter(otlpOptions =>
                    {
                        otlpOptions.Endpoint = openTelemetryOptions.MetricsEndpoint;
                    }))
                .WithLogging(loggingBuilder => loggingBuilder
                        .AddOtlpExporter(otlpOptions =>
                        {
                            otlpOptions.Endpoint = openTelemetryOptions.LogsEndpoint;
                        }),
                    options =>
                    {
                        options.IncludeScopes = true;
                        options.IncludeFormattedMessage = true;
                        options.ParseStateValues = true;
                    });

            loggingBuilder.AddOpenTelemetry(x =>
            {
                x.IncludeScopes = true;
                x.IncludeFormattedMessage = true;
            });
        }
    }
}
