using System.Collections.Generic;
using brady.Models.Output;
using brady.Models.Report;

namespace brady.Services
{
    public interface ICalculationService
    {
        List<brady.Models.Output.Day> CalculateEmissionsForEachDay(IFuel fuel);
        double GetEmissionFactor(IFuel fuel);
        Generator TotalDayGenerationValue(IFuel fuel, double valueFactor);
        double GetWindValueFactor(WindGenerator wind);
        double GetCoalValueFactor();
        double GetGasValueFactor();
        double GetGasEmissionFactor();
        double GetCoalEmissionFactor();
        double GetWindEmissionFactor();
    }
}