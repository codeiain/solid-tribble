using brady.Models;
using brady.Models.Reference;
using brady.Models.Report;
using brady.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace brady.UnitTests
{
    [TestClass]
    public class ValueFactorTests
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
                    }
                }
            };
            _referenceServiceMock.Setup(r => r.GetStandingData()).Returns(_refData);
            _testService = new CalculationService(_loggerMock.Object, _referenceServiceMock.Object);
        }
        
        [TestMethod]
        public void GetWindValueFactorTest_Offshore()
        {
            var testWind = new WindGenerator();
            testWind.Location = "Offshore";
            var result = _testService.GetWindValueFactor(testWind);
            _referenceServiceMock.Verify(r=>r.GetStandingData(), Times.Once);
            result.Should().Be(3);
        }
        
        [TestMethod]
        public void GetWindValueFactorTest_Onshore()
        {
            var testWind = new WindGenerator();
            testWind.Location = "Onshore";

            var result = _testService.GetWindValueFactor(testWind);
            
            _referenceServiceMock.Verify(r=>r.GetStandingData(), Times.Once);
            result.Should().Be(1);
        }

        [TestMethod]
        public void GetCoalValueFactorTest()
        {
            var result = _testService.GetCoalValueFactor();
            result.Should().Be(2);
        }
        
        [TestMethod]
        public void GetGasValueFactorTest()
        {
            var result = _testService.GetGasValueFactor();
            result.Should().Be(2);
        }
    }
}