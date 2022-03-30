using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyRates.BusinessLogic;

public interface IRelativeCurrencyRatesProvider
{
    Task<IReadOnlyList<RelativeCurrencyRate>> ProvideAll();
}