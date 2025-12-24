namespace Padel.Infrastructure.Monitoring;

internal sealed class OpenTelemetryOptions
{
    public const string OpenTelemetryConfigSection = "OpenTelemetry";

    public Uri LogsEndpoint { get; set; }
    public Uri TracesEndpoint { get; set; }
    public Uri MetricsEndpoint { get; set; }
}
