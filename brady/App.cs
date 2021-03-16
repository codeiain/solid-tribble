using System.IO;
using brady.Helper;
using brady.Models;
using brady.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace brady
{
    public class App
    {
        private readonly ILogger<App> _logger;
        private readonly AppSettings _config;

        public App(IOptions<AppSettings> config, 
            ILogger<App> logger)
        {
            Guard.IsNotNull(logger, nameof(logger));
            Guard.IsNotNull(config, nameof(config));
            _logger = logger;
            _config = config.Value;
        }

        public void Run()
        {
            _logger.Log(LogLevel.Information, "Application Started");
            if (Directory.Exists(_config.DropFolder))
            {
                _logger.Log(LogLevel.Information, $"Watching {_config.DropFolder}");
            }
            else
            {
                _logger.Log(LogLevel.Error, "Can't Find Drop Folder");
            }

            if (Directory.Exists(_config.OutputFolder))
            {
                _logger.Log(LogLevel.Information, $"results can be found {_config.OutputFolder}");
            }
            else
            {
                _logger.Log(LogLevel.Error, "Can't Find Output Folder");
            }
            System.Console.ReadKey();
        }
    }
}