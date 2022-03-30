using System;
using CurrencyRates.BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CurrencyRates.Ui.ViewModels.Implementation.Tests
{
    [TestClass]
    public class RelativeCurrencyRateViewModelTests
    {
        [TestMethod]
        public void FirstCurrencyAmount_Set_SecondCurrencyAmountCalculated()
        {
            var relativeCurrencyRate =
                new RelativeCurrencyRate(0.5, string.Empty, string.Empty, string.Empty, string.Empty);
            var viewModel = new RelativeCurrencyRateViewModel(relativeCurrencyRate);

            viewModel.FirstCurrencyAmount = 100.0;
            var actualSecondCurrencyAmount = viewModel.SecondCurrencyAmount;

            Assert.IsTrue(Math.Abs(actualSecondCurrencyAmount - 200.0) < double.Epsilon);
        }

        [TestMethod]
        public void SecondCurrencyAmount_Set_FirstCurrencyAmountCalculated()
        {
            var relativeCurrencyRate =
                new RelativeCurrencyRate(3, string.Empty, string.Empty, string.Empty, string.Empty);
            var viewModel = new RelativeCurrencyRateViewModel(relativeCurrencyRate);

            viewModel.SecondCurrencyAmount = 3.0;
            var actualFirstCurrencyAmount = viewModel.FirstCurrencyAmount;

            Assert.IsTrue(Math.Abs(actualFirstCurrencyAmount - 9.0) < double.Epsilon);
        }

        [TestMethod]
        public void ClearCurrencyAmounts_AfterNonZeroValuesSet_ZeroValuesReturned()
        {
            var relativeCurrencyRate =
                new RelativeCurrencyRate(3, string.Empty, string.Empty, string.Empty, string.Empty);
            var viewModel = new RelativeCurrencyRateViewModel(relativeCurrencyRate);

            viewModel.SecondCurrencyAmount = 3.0;

            Assert.IsFalse(Math.Abs(viewModel.FirstCurrencyAmount) < double.Epsilon);
            Assert.IsFalse(Math.Abs(viewModel.SecondCurrencyAmount) < double.Epsilon);

            viewModel.ClearCurrencyAmounts();

            Assert.IsTrue(Math.Abs(viewModel.FirstCurrencyAmount) < double.Epsilon);
            Assert.IsTrue(Math.Abs(viewModel.SecondCurrencyAmount) < double.Epsilon);
        }
    }
}