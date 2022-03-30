using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CurrencyRates.Ui.ViewModels.Implementation.Tests
{
    [TestClass]
    public class CurrencySelectionsViewModelTests
    {
        [TestMethod]
        public void Ctor_SettersNotInvoked_CorrectDefaultIndexesSet()
        {
            var currencyInfos = new IRelativeCurrencyRateViewModel[2, 2];
            var viewModel = new CurrencySelectionsViewModel(currencyInfos, new List<string> { "first", "second" });

            Assert.AreEqual(0, viewModel.FirstSelectedCurrency.SelectedCurrencyIndex);
            Assert.AreEqual(1, viewModel.SecondSelectedCurrency.SelectedCurrencyIndex);
        }

        [DataTestMethod]
        [DataRow(0, 0)]
        [DataRow(1, 1)]
        public void AreEqualIndexes_IndexesAreEqual_True(int firstIndex, int secondIndex)
        {
            var currencyInfos = new IRelativeCurrencyRateViewModel[2, 2];
            var viewModel = new CurrencySelectionsViewModel(currencyInfos, new List<string> { "first", "second" })
            {
                FirstSelectedCurrency =
                {
                    SelectedCurrencyIndex = firstIndex
                },
                SecondSelectedCurrency =
                {
                    SelectedCurrencyIndex = secondIndex
                }
            };

            var result = viewModel.AreEqualIndexes();
            Assert.IsTrue(result);
        }

        [DataTestMethod]
        [DataRow(0, 1)]
        [DataRow(1, 0)]
        public void AreEqualIndexes_IndexesAreNotEqual_False(int firstIndex, int secondIndex)
        {
            var currencyInfos = new IRelativeCurrencyRateViewModel[2, 2];
            var viewModel = new CurrencySelectionsViewModel(currencyInfos, new List<string> { "first", "second" })
            {
                FirstSelectedCurrency =
                {
                    SelectedCurrencyIndex = firstIndex
                },
                SecondSelectedCurrency =
                {
                    SelectedCurrencyIndex = secondIndex
                }
            };

            var result = viewModel.AreEqualIndexes();
            Assert.IsFalse(result);
        }

        [DataTestMethod]
        [DataRow(0, 0)]
        [DataRow(1, 1)]
        public void GetSelectedRelativeCurrencyRateViewModel_IndexesAreEqual_ReturnsNull(int firstIndex,
            int secondIndex)
        {
            var currencyInfos = new IRelativeCurrencyRateViewModel[2, 2];
            var viewModel = new CurrencySelectionsViewModel(currencyInfos, new List<string> { "first", "second" })
            {
                FirstSelectedCurrency =
                {
                    SelectedCurrencyIndex = firstIndex
                },
                SecondSelectedCurrency =
                {
                    SelectedCurrencyIndex = secondIndex
                }
            };

            var result = viewModel.GetSelectedRelativeCurrencyRateViewModel();
            Assert.IsNull(result);
        }

        [DataTestMethod]
        [DataRow(3, 0)]
        [DataRow(1, 5)]
        public void GetSelectedRelativeCurrencyRateViewModel_IndexesOutOfRange_ReturnsNull(int firstIndex,
            int secondIndex)
        {
            var currencyInfos = new IRelativeCurrencyRateViewModel[2, 2];
            var viewModel = new CurrencySelectionsViewModel(currencyInfos, new List<string> { "first", "second" })
            {
                FirstSelectedCurrency =
                {
                    SelectedCurrencyIndex = firstIndex
                },
                SecondSelectedCurrency =
                {
                    SelectedCurrencyIndex = secondIndex
                }
            };

            var result = viewModel.GetSelectedRelativeCurrencyRateViewModel();
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetSelectedRelativeCurrencyRateViewModel_CorrectIndexes_ReturnsCorrectResult()
        {
            var firstIndex = 0;
            var secondIndex = 1;

            var relativeCurrencyRateViewModel = new Mock<IRelativeCurrencyRateViewModel>();
            relativeCurrencyRateViewModel.Setup(m => m.FirstCurrencyCharCode).Returns("TestCharCode");

            var anotherRelativeCurrencyRateViewModel = new Mock<IRelativeCurrencyRateViewModel>();
            anotherRelativeCurrencyRateViewModel.Setup(m => m.FirstCurrencyCharCode).Returns("AnotherTestCharCode");


            var currencyInfos = new[,]
            {
                { anotherRelativeCurrencyRateViewModel.Object, relativeCurrencyRateViewModel.Object },
                { anotherRelativeCurrencyRateViewModel.Object, anotherRelativeCurrencyRateViewModel.Object }
            };
            var viewModel = new CurrencySelectionsViewModel(currencyInfos, new List<string> { "first", "second" })
            {
                FirstSelectedCurrency =
                {
                    SelectedCurrencyIndex = firstIndex
                },
                SecondSelectedCurrency =
                {
                    SelectedCurrencyIndex = secondIndex
                }
            };

            var result = viewModel.GetSelectedRelativeCurrencyRateViewModel();
            Assert.IsNotNull(result);
            Assert.AreEqual("TestCharCode", result.FirstCurrencyCharCode);
        }
    }
}