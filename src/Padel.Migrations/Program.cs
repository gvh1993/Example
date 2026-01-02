using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Padel.Infrastructure.Data;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

builder.Services.AddLogging(configure => configure.AddConsole());

builder.Services.AddData(builder.Configuration);

var host = builder.Build();

//using (var scope = host.Services.CreateScope())
//{
//    var context = scope.ServiceProvider.GetRequiredService<PadelDbContext>();
//    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

//    try
//    {
//        logger.LogInformation("Applying migrations...");
//        await context.Database.MigrateAsync();
//        logger.LogInformation("Migrations applied successfully.");
//    }
//    catch (Exception ex)
//    {
//        logger.LogError(ex, "An error occurred while applying migrations.");
//        throw;
//    }
//}

await host.RunAsync();
