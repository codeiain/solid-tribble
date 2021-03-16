using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using brady.Helper;
using brady.Models;
using brady.Models.Output;
using brady.Models.Reference;
using brady.Models.Report;
using brady.Wrappers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Day = brady.Models.Report.Day;

namespace brady.Services
{
    public class CalculationService : ICalculationService
    {
        private readonly ILogger<App> _logger;
        private readonly ReferenceData _referenceData;
        Dictionary<Type, string> _typeDict = new Dictionary<Type, string>
        {
            {typeof(WindGenerator),"wind"},
            {typeof(CoalGenerator),"coal"},
            {typeof(GasGenerator),"gas"}
        };
        public CalculationService(ILogger<App> logger, IReferenceService referenceService)
        {
            Guard.IsNotNull(logger, nameof(logger));
            Guard.IsNotNull(referenceService, nameof(referenceService));
            _logger = logger;
            _referenceData = referenceService.GetStandingData();
        }

        public List<brady.Models.Output.Day> CalculateEmissionsForEachDay(IFuel fuel)
        {
            var emissionFactor = GetEmissionFactor(fuel);
            return fuel.Generation.Day.Select(day => new Models.Output.Day()
            {
                Date = day.Date,
                Emission = (double.Parse(day.Energy) * double.Parse(fuel.EmissionsRating) * emissionFactor).ToString(CultureInfo.CurrentCulture),
                Name = fuel.Name
            }).ToList();
        }


        public double GetEmissionFactor(IFuel fuel)
        {
            switch (_typeDict[fuel.GetType()])
            {
                case "wind":
                    return GetWindEmissionFactor();
                    break;
                case "coal":
                    return GetCoalEmissionFactor();
                    break;
                case "gas":
                    return GetGasEmissionFactor();
                    break;
            }
            return double.NaN;
        }

        public Generator TotalDayGenerationValue(IFuel fuel, double valueFactor)
        {
            var generators = new Generator {Name = fuel.Name};
            var total = fuel.Generation.Day.Sum(day =>
                double.Parse(day.Energy) * double.Parse(day.Price) * valueFactor);

            generators.Total = total.ToString(CultureInfo.CurrentCulture);
            return generators;
        }
        public double GetWindValueFactor(WindGenerator wind)
        {
            double valueFactor = 0;
            switch (wind.Location)
            {
                case "Offshore":
                    //Low
                    valueFactor = double.Parse(_referenceData.Factors.ValueFactor.Low);
                    break;
                case "Onshore":
                    //High
                    valueFactor = double.Parse(_referenceData.Factors.ValueFactor.High);
                    break;
            }
            return valueFactor;
        }
        public double GetCoalValueFactor()
        {
            return double.Parse(_referenceData.Factors.ValueFactor.Medium);
        }
        public double GetGasValueFactor()
        {
            return double.Parse(_referenceData.Factors.ValueFactor.Medium);
        }

        public double GetGasEmissionFactor()
        {
            return double.Parse(_referenceData.Factors.EmissionsFactor.Medium);
        }
        public double GetCoalEmissionFactor()
        {
            return double.Parse(_referenceData.Factors.EmissionsFactor.High);
        }
        public double GetWindEmissionFactor()
        {
            return 0d;
        }
    }
}