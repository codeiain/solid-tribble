using System.IO;
using System.Xml.Serialization;
using brady.Helper;
using brady.Models;
using brady.Models.Reference;
using brady.Models.Report;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace brady.Services
{
    public class ReferenceService : IReferenceService
    {
        private readonly ILogger<App> _logger;
        private readonly AppSettings _config;

        public ReferenceService(IOptions<AppSettings> config, ILogger<App> logger)
        {
            Guard.IsNotNull(logger, nameof(logger));
            Guard.IsNotNull(config, nameof(config));
            _logger = logger;
            _config = config.Value;
        }

        public ReferenceData GetStandingData()
        {
            return XmlToObject.Convert<ReferenceData>(_config.ReferenceFile);
        }
        

    }
}