using System.Threading.Tasks;

namespace CurrencyRates.Ui.ViewModels.Factories;

public interface ICurrencySelectionsViewModelFactory
{
    Task<ICurrencySelectionsViewModel> Create();
}