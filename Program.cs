using DiFactoryTest.Impl;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

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

        Console.WriteLine(ShowInfo(host.Services, true));
        Console.WriteLine();

        Console.WriteLine(ShowInfo(host.Services, false));
        Console.WriteLine();

        await host.RunAsync();
    }

    private static string ShowInfo(IServiceProvider services, bool isPrimary)
    {
        using var scope = services.CreateScope();

        // Change the mode on the options
        var opt = scope.ServiceProvider.GetRequiredService<IOptions<ModeOptions>>();
        opt.Value.IsPrimary = isPrimary;

        // Use the factory to create the service and retrieve it's info
        return scope.ServiceProvider.GetRequiredService<IMyService>().GetInfo();
    }

    static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
                services
                    .AddSingleton(new PrimaryDependency("Primo!"))
                    .AddScoped<ServiceFactory>()
                    .AddScoped(sp => sp.GetRequiredService<ServiceFactory>().Create())
                     // Will be used by the factory depending on mode;
                    .AddScoped<MyPrimaryService>()
                    .AddScoped<MySecondaryService>()

                    // The options
                    .AddOptions<ModeOptions>());
}