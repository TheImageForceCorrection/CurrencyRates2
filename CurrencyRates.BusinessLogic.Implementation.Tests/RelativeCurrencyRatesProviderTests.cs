using System.Linq;
using System.Threading.Tasks;
using CurrencyRates.Data;
using CurrencyRates.Data.Dtos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CurrencyRates.BusinessLogic.Implementation.Tests
{
    [TestClass]
    public class RelativeCurrencyRatesProviderTests
    {
        [TestMethod]
        public void ProvideAll_NullProvided_EmptyListReturned()
        {
            var currenciesProvider = new Mock<ICurrenciesProvider>();
            currenciesProvider.Setup(p => p.Provide()).Returns(Task.FromResult((CurrenciesDto?)null));

            var relativeCurrencyRatesProvider =
                new RelativeCurrencyRatesProvider(currenciesProvider.Object,
                    Mock.Of<IRelativeCurrencyRateCalculator>());

            var relativeCurrencyRates = relativeCurrencyRatesProvider.ProvideAll().Result.ToList();

            Assert.AreEqual(0, relativeCurrencyRates.Count);
        }


        [TestMethod]
        public void ProvideAll_CurrenciesProvided_RelativeCurrenciesReturned()
        {
            var currenciesProvider = new Mock<ICurrenciesProvider>();
            var firstCurrencyDto = new CurrencyDto("FirstCurrencyName", "FirstCurrencyCharCode", 1.0);
            var secondCurrencyDto = new CurrencyDto("SecondCurrencyName", "SecondCurrencyCharCode", 2.0);

            var currenciesDto = new CurrenciesDto(new[] { firstCurrencyDto, secondCurrencyDto });
            var relativeRateCalculator = new Mock<IRelativeCurrencyRateCalculator>();
            relativeRateCalculator
                .Setup(c => c.Calculate(It.Is<double>(r => r - 1.0 < double.Epsilon),
                    It.Is<double>(r => r - 2.0 < double.Epsilon))).Returns(0.5);
            relativeRateCalculator
                .Setup(c => c.Calculate(It.Is<double>(r => r - 2.0 < double.Epsilon),
                    It.Is<double>(r => r - 1.0 < double.Epsilon))).Returns(2);
            currenciesProvider.Setup(p => p.Provide()).Returns(Task.FromResult(currenciesDto)!);
            var relativeCurrencyRatesProvider =
                new RelativeCurrencyRatesProvider(currenciesProvider.Object, relativeRateCalculator.Object);

            var relativeCurrencyRates = relativeCurrencyRatesProvider.ProvideAll().Result.ToList();

            Assert.AreEqual(2, relativeCurrencyRates.Count);
            AssertRelativeCurrencyRates(relativeCurrencyRates[0], 0.5, firstCurrencyDto.Name, firstCurrencyDto.CharCode,
                secondCurrencyDto.Name, secondCurrencyDto.CharCode);

            AssertRelativeCurrencyRates(relativeCurrencyRates[1], 2, secondCurrencyDto.Name,
                secondCurrencyDto.CharCode,
                firstCurrencyDto.Name, firstCurrencyDto.CharCode);
        }

        private void AssertRelativeCurrencyRates(RelativeCurrencyRate relativeCurrencyRate, double relativeRate,
            string firstCurrencyName, string firstCurrencyCharCode, string secondCurrencyName,
            string secondCurrencyCharCode)
        {
            Assert.AreEqual(firstCurrencyCharCode, relativeCurrencyRate.FirstCurrencyCharCode);
            Assert.AreEqual(firstCurrencyName, relativeCurrencyRate.FirstCurrencyName);
            Assert.AreEqual(secondCurrencyCharCode, relativeCurrencyRate.SecondCurrencyCharCode);
            Assert.AreEqual(secondCurrencyName, relativeCurrencyRate.SecondCurrencyName);
            Assert.AreEqual(relativeRate, relativeCurrencyRate.RelativeRate);
        }
    }
}