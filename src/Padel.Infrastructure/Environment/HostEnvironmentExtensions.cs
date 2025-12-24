using Microsoft.Extensions.Hosting;

namespace Padel.Infrastructure.Environment;

internal static class HostEnvironmentExtensions
{
    extension(IHostEnvironment environment)
    {
        public bool IsTest => environment.IsEnvironment("Test");

        public bool IsDevelopmentOrTest => environment.IsDevelopment() || environment.IsTest;
    }
}
