using System.Collections.Generic;

namespace CurrencyRates.Data.Dtos;

public sealed class CurrenciesDto
{
    public CurrenciesDto(IReadOnlyList<CurrencyDto> currencyInfos)
    {
        CurrencyInfos = currencyInfos;
    }

    public IReadOnlyList<CurrencyDto> CurrencyInfos { get; }
}