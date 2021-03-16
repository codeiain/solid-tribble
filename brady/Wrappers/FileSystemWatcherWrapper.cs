using System;
using System.IO;
using brady.Helper;
using brady.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace brady.Wrappers
{
    public class FileSystemWatcherWrapper : IFileSystemWatcherWrapper
    {
        private readonly FileSystemWatcher _watcher;
        private readonly ILogger<App> _logger;
        private readonly AppSettings _config;

        public FileSystemWatcherWrapper(IOptions<AppSettings> config, ILogger<App> logger,FileSystemWatcher watcher)
        {
            Guard.IsNotNull(logger, nameof(logger));
            Guard.IsNotNull(config, nameof(config));
            Guard.IsNotNull(watcher, nameof(watcher));
            _logger = logger;
            _config = config.Value;
            _watcher = watcher;
            try
            {
                _watcher.Path = _config.DropFolder;
                _watcher.Created += FileSystemWatcher_Created;
                _watcher.EnableRaisingEvents = true;
            }
            catch(Exception exception)
            {
                _logger.Log(LogLevel.Debug, exception.Message);
            }
            
        }
        
        public event FileSystemEventHandler Created;
        
        private void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            FileSystemEventHandler handler = Created;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public bool EnableRaisingEvents
        {
            get { return _watcher.EnableRaisingEvents; }
            set { _watcher.EnableRaisingEvents = value; }
        }
    }
}