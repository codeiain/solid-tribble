using System.IO;

namespace brady.Wrappers
{
    public interface IFileSystemWatcherWrapper
    {
        event FileSystemEventHandler Created;
        bool EnableRaisingEvents { get; set; }
    }
}