using System.Collections.Generic;
using System.Threading.Tasks;
using CurrencyRates.BusinessLogic;
using CurrencyRates.Ui.ViewModels.Implementation.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CurrencyRates.Ui.ViewModels.Implementation.Tests.Factories
{
    [TestClass]
    public class CurrencySelectionsViewModelFactoryTests
    {
        [TestMethod]
        public void Create_CorrectDataProvided_ViewModelReturned()
        {
            var relativeCurrencyRates = new List<RelativeCurrencyRate>
            {
                new(0.5, "TestFirstCurrencyName", "TestFirstCurrencyCharCode",
                    "TestSecondCurrencyName", "TestSecondCurrencyCharCode"),
                new(0.5, "TestSecondCurrencyName", "TestSecondCurrencyCharCode",
                    "TestFirstCurrencyName", "TestFirstCurrencyCharCode")
            };
            var relativeCurrencyRatesProvider = new Mock<IRelativeCurrencyRatesProvider>();
            relativeCurrencyRatesProvider.Setup(p => p.ProvideAll())
                .Returns(Task.FromResult((IReadOnlyList<RelativeCurrencyRate>)relativeCurrencyRates));

            var constructor = (RelativeCurrencyRate relativeCurrencyRate) =>
                new RelativeCurrencyRateViewModel(relativeCurrencyRate);

            var factory = new CurrencySelectionsViewModelFactory(relativeCurrencyRatesProvider.Object, constructor);

            var createdViewModel = factory.Create().Result;


            Assert.AreEqual(2, createdViewModel.CurrencyNames.Count);
            CurrencySelectionsViewModelAssert(createdViewModel, 0, 1, "TestFirstCurrencyCharCode");
            CurrencySelectionsViewModelAssert(createdViewModel, 1, 0, "TestSecondCurrencyCharCode");
        }

        private void CurrencySelectionsViewModelAssert(ICurrencySelectionsViewModel createdViewModel, int firstIndex,
            int secondIndex, string charCode)
        {
            createdViewModel.FirstSelectedCurrency.SelectedCurrencyIndex = firstIndex;
            createdViewModel.SecondSelectedCurrency.SelectedCurrencyIndex = secondIndex;
            var relativeCurrencyRateViewModel = createdViewModel.GetSelectedRelativeCurrencyRateViewModel();
            Assert.IsNotNull(relativeCurrencyRateViewModel);
            Assert.AreEqual(charCode,
                relativeCurrencyRateViewModel.FirstCurrencyCharCode);
        }
    }
}