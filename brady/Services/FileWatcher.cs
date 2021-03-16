using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using brady.Helper;
using brady.Models;
using brady.Models.Output;
using brady.Models.Report;
using brady.Wrappers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace brady.Services
{
    public class FileWatcher : IFileWatcher
    {
        private readonly ILogger<App> _logger;
        private readonly AppSettings _config;
        private readonly IFileSystemWatcherWrapper _fileSystemWatcher;
        private readonly ICalculationService _calculationService;

        public FileWatcher(IOptions<AppSettings> config,
            ILogger<App> logger,
            IFileSystemWatcherWrapper fileSystemWatcher,
            ICalculationService calculationService)
        {
            Guard.IsNotNull(logger, nameof(logger));
            Guard.IsNotNull(config, nameof(config));
            Guard.IsNotNull(fileSystemWatcher, nameof(fileSystemWatcher));
            Guard.IsNotNull(calculationService, nameof(calculationService));
            _logger = logger;
            _config = config.Value;
            _fileSystemWatcher = fileSystemWatcher;
            _calculationService = calculationService;
            SetupWatcher();
        }

        /// <summary>
        /// Sets up the Folder watcher
        /// </summary>
        private void SetupWatcher()
        {
            _fileSystemWatcher.Created += (sender, args) =>
            {
                _logger.Log(LogLevel.Information, "New File detected");
                try
                {
                    var report = XmlToObject.Convert<GenerationReport>(args.FullPath);
                    var outputReport = new GenerationOutput();
                    var days = new List<brady.Models.Output.Day>();
                    foreach (var coalGenerator in report.Coal.CoalGenerator)
                    {
                        outputReport.Totals.Generator.Add(
                            _calculationService.TotalDayGenerationValue(coalGenerator,
                                _calculationService.GetCoalValueFactor()));
                        days.AddRange(_calculationService.CalculateEmissionsForEachDay(coalGenerator));
                        outputReport.ActualHeatRates.Add(new ActualHeatRates()
                        {
                            Name = coalGenerator.Name,
                            HeatRate = (double.Parse(coalGenerator.TotalHeatInput)
                                        / double.Parse(coalGenerator.ActualNetGeneration))
                                .ToString(CultureInfo.CurrentCulture)
                        });
                    }

                    foreach (var gasGenerator in report.Gas.GasGenerator)
                    {
                        outputReport.Totals.Generator.Add(_calculationService.TotalDayGenerationValue(gasGenerator,
                            _calculationService.GetGasValueFactor()));
                        days.AddRange(_calculationService.CalculateEmissionsForEachDay(gasGenerator));
                    }

                    foreach (var windGenerator in report.Wind.WindGenerator)
                    {
                        outputReport.Totals.Generator.Add(_calculationService.TotalDayGenerationValue(windGenerator,
                            _calculationService.GetGasValueFactor()));
                        days.AddRange(_calculationService.CalculateEmissionsForEachDay(windGenerator));
                    }

                    var query = days.GroupBy(d => d.Date)
                        .Select(g =>
                            g.OrderByDescending(d => double.Parse(d.Emission))
                                .Take(1)).SelectMany(x => x);
                    outputReport.MaxEmissionGenerators.Day.AddRange(query);

                    WriteXML(outputReport);
                    _logger.Log(LogLevel.Information, "Report generated");
                }
                catch (InvalidOperationException e)
                {
                    _logger.Log(LogLevel.Error, "Invalid File");
                }
                catch (Exception ex)
                {
                    _logger.Log(LogLevel.Error, ex.Message);
                }
                
            };
        }

        public void WriteXML(GenerationOutput report)
        {  
            
            System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(typeof(GenerationOutput));  
  
            var path = _config.OutputFolder + $"//GenerationOutput{DateTime.Now.Ticks}.xml";  
            System.IO.FileStream file = System.IO.File.Create(path);  
  
            writer.Serialize(file, report);  
            file.Close();  
        }  
    }
}