using System.IO;
using System.Threading.Tasks;
using brady.Models;
using brady.Services;
using brady.Wrappers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace brady
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            
            var serviceProvider = serviceCollection.BuildServiceProvider();
            serviceProvider.GetService<App>()?.Run();
        }
        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(new LoggerFactory());
            services.AddLogging(cfg=>cfg.AddConsole().AddDebug());
            
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .Build();
            services.AddOptions();
            services.Configure<AppSettings>(configuration.GetSection("App"));
            
            services.AddSingleton<ICalculationService, CalculationService>();
            services.AddSingleton<IFileSystemWatcherWrapper, FileSystemWatcherWrapper>();
            services.AddSingleton<IFileWatcher, FileWatcher>();
            services.AddSingleton<IReferenceService, ReferenceService>();
            
            services.AddSingleton<FileSystemWatcher>();
            services.AddSingleton<App>();
        }
    }
}