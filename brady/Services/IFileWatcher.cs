using brady.Models.Output;

namespace brady.Services
{
    public interface IFileWatcher
    {
        void WriteXML(GenerationOutput report);
    }
}