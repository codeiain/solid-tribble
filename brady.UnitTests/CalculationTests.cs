using System.Collections.Generic;
using brady.Models;
using brady.Models.Output;
using brady.Models.Reference;
using brady.Models.Report;
using brady.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Day = brady.Models.Report.Day;
using EmissionsFactor = brady.Models.Reference.EmissionsFactor;

namespace brady.UnitTests
{
    
    [TestClass]
    public class CalculationTests
    {
        private Mock<ILogger<App>> _loggerMock;
        private Mock<IReferenceService> _referenceServiceMock;
            private ReferenceData _refData;
            private CalculationService _testService;
        
            [TestInitialize]
            public void Setup()
            {
                _loggerMock = new Mock<ILogger<App>>();
                _referenceServiceMock = new Mock<IReferenceService>();
                _refData = new ReferenceData()
                {
                    Factors = new Factors()
                    {
                        ValueFactor = new ValueFactor()
                        {
                            High = "1",
                            Medium = "2",
                            Low = "3",
                        },
                        EmissionsFactor = new EmissionsFactor()
                        {
                            High = "1",
                            Medium = "2",
                            Low = "3"
                        }
                    }
                };
                _referenceServiceMock.Setup(r => r.GetStandingData()).Returns(_refData);
                _testService = new CalculationService( _loggerMock.Object, _referenceServiceMock.Object);
            }

            [TestMethod]
            public void TotalDayGenerationValueTest_SingleDay()
            {
                var coal = new CoalGenerator()
                {
                    Name = "coal",
                    Generation = new Generation()
                    {
                        Day = new List<Day>()
                        {
                            new Day()
                            {
                                Date = "today",
                                Energy = "5",
                                Price = "5"
                            }
                        }
                    }
                };
                var assert = new Generator()
                {
                    Name = "coal",
                    Total = "125"
                };
                var result = _testService.TotalDayGenerationValue(coal, 5);
                result.Should().BeEquivalentTo(assert);

            }
            
            [TestMethod]
            public void TotalDayGenerationValueTest_MultipleDay()
            {
                var coal = new CoalGenerator()
                {
                    Name = "coal",
                    Generation = new Generation()
                    {
                        Day = new List<Day>()
                        {
                            new Day()
                            {
                                Date = "today",
                                Energy = "5",
                                Price = "5"
                            },
                            new Day()
                            {
                                Date = "yesterday",
                                Energy = "2",
                                Price = "2"
                            }
                        }
                    }
                };
                var assert = new Generator()
                {
                    Name = "coal",
                    Total = "145"
                };
                var result = _testService.TotalDayGenerationValue(coal, 5);
                result.Should().BeEquivalentTo(assert);

            }

            [TestMethod]
            public void CalculateEmissionsForEachDay_SingleDay()
            {
                var coal = new CoalGenerator()
                {
                    Name = "coal",
                    EmissionsRating = "5",
                    Generation = new Generation()
                    {
                        Day = new List<Day>()
                        {
                            new Day()
                            {
                                Date = "today",
                                Energy = "5",
                                Price = "5"
                            }
                        }
                    }
                };
                var results = _testService.CalculateEmissionsForEachDay(coal);
                List<brady.Models.Output.Day> asserts = new List<Models.Output.Day>()
                {
                    new Models.Output.Day()
                    {
                        Date = "today",
                        Name = "coal",
                        Emission = "25"
                    }
                };
                results.Should().BeEquivalentTo(asserts);
            }
            
            [TestMethod]
            public void CalculateEmissionsForEachDay_MultipleDay()
            {
                var coal = new CoalGenerator()
                {
                    Name = "coal",
                    EmissionsRating = "5",
                    Generation = new Generation()
                    {
                        Day = new List<Day>()
                        {
                            new Day()
                            {
                                Date = "today",
                                Energy = "5",
                                Price = "5"
                            },
                            new Day()
                            {
                                Date = "tomorrow",
                                Energy = "5",
                                Price = "5"
                            }
                        }
                    }
                };
                var results = _testService.CalculateEmissionsForEachDay(coal);
                List<brady.Models.Output.Day> asserts = new List<Models.Output.Day>()
                {
                    new Models.Output.Day()
                    {
                        Date = "today",
                        Name = "coal",
                        Emission = "25"
                    },
                    new Models.Output.Day()
                    {
                        Date = "tomorrow",
                        Name = "coal",
                        Emission = "25"
                    }
                };
                results.Should().BeEquivalentTo(asserts);
            }
    }
}