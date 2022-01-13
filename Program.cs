using DiFactoryTest.Impl;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MediatR;
using DiFactoryTest.Query;
using Microsoft.Extensions.Logging;

namespace DiFactoryTest;

/// <summary>
/// An example that uses an option to decide which service to create in the factory.
/// The factory doesn't directly create the service but uses DI to retrieve the instance.
/// </summary>
class Program
{
    static async Task Main(string[] args)
    {
        using IHost host = CreateHostBuilder(args).Build();
        var logger = host.Services.GetRequiredService<ILoggerFactory>().CreateLogger<Program>();
        SetMode(host.Services, true);
        logger.LogThreadInformation(host.Services.GetRequiredService<InfoReader>().GetInfoAsync().Result);

        SetMode(host.Services, false);
        logger.LogThreadInformation(host.Services.GetRequiredService<InfoReader>().GetInfoAsync().Result);

        await host.RunAsync();
    }

    private static void SetMode(IServiceProvider services, bool isPrimary)
    {
        // Change the mode on the options
        var opt = services.GetRequiredService<IOptions<ModeOptions>>();
        opt.Value.IsPrimary = isPrimary;
    }

    static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureLogging((_, builder) =>
                builder
                    .ClearProviders()
                    .AddConsole())
            .ConfigureServices((_, services) =>
                services
                    .AddMediatR(typeof(Program))
                    .AddSingleton(new PrimaryDependency("Primo!"))
                    .AddSingleton<InfoReader>()
                    .AddScoped<ServiceFactory>()
                    .AddScoped(sp => sp.GetRequiredService<ServiceFactory>().Create())

                     // Will be used by the factory depending on mode;
                    .AddScoped<MyPrimaryService>()
                    .AddScoped<MySecondaryService>()

                    // The options
                    .AddOptions<ModeOptions>());
}