using System.Threading.Tasks;
using CurrencyRates.Data.Dtos;

namespace CurrencyRates.Data;

public interface ICurrenciesProvider
{
    Task<CurrenciesDto?> Provide();
}