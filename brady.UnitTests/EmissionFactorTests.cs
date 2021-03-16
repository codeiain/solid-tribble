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
    public class EmissionFactorTests
    {
        private Mock<ILogger<App>> _loggerMock;
        private Mock<IOptions<AppSettings>> _configMock;
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
        public void GetEmissionFactorTest_CoalGenerator()
        {
            var testGenerator = new CoalGenerator();
            var result = _testService.GetEmissionFactor(testGenerator);
            _referenceServiceMock.Verify(r=>r.GetStandingData(), Times.Once);
            result.Should().Be(1);
        }
        
        [TestMethod]
        public void GetEmissionFactorTest_GasGenerator()
        {
            var testGenerator = new GasGenerator();
            var result = _testService.GetEmissionFactor(testGenerator);
            _referenceServiceMock.Verify(r=>r.GetStandingData(), Times.Once);
            result.Should().Be(2);
        }
        
        [TestMethod]
        public void GetEmissionFactorTest_WindGenerator()
        {
            var testGenerator = new WindGenerator();
            var result = _testService.GetEmissionFactor(testGenerator);
            _referenceServiceMock.Verify(r=>r.GetStandingData(), Times.Once);
            result.Should().Be(0);
        }
    }
}