using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CurrencyRates.BusinessLogic.Implementation.Tests
{
    [TestClass]
    public class RelativeCurrencyRateCalculatorTests
    {
        [DataTestMethod]
        [DataRow(0.0, 0.2)]
        [DataRow(0, 6.1)]
        [DataRow(0.0, 0.0)]
        public void Calculate_ZeroCurrencyRate_Throws(double firstCurrencyRate, double secondCurrencyRate)
        {
            var calculator = new RelativeCurrencyRateCalculator();
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                calculator.Calculate(firstCurrencyRate, secondCurrencyRate);
            });
        }

        [DataTestMethod]
        [DataRow(0.1, 0.2, 0.5)]
        [DataRow(100, 0.1, 1000)]
        [DataRow(0.4, 0.2, 2)]
        public void Calculate_NonZeroCurrencyRate_CorrectResultReturned(double firstCurrencyRate,
            double secondCurrencyRate,
            double expectedRelativeRate)
        {
            var calculator = new RelativeCurrencyRateCalculator();

            var actualRelativeRate = calculator.Calculate(firstCurrencyRate, secondCurrencyRate);

            Assert.AreEqual(expectedRelativeRate, actualRelativeRate);
        }
    }
}