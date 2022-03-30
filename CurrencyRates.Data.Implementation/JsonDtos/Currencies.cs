using System.Collections.Generic;
using CurrencyRates.Data.Dtos;

namespace CurrencyRates.Data.Implementation.JsonDtos;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class Currencies
{
    // ReSharper disable once MemberCanBePrivate.Global
    public Dictionary<string, Currency> Valute { get; } = new();

    public CurrenciesDto ToCurrenciesDto()
    {
        var currencyDtoList = new List<CurrencyDto>();

        foreach (var currency in Valute.Values)
        {
            var currencyDto = currency.ToDto();
            if (currencyDto is not null)
            {
                currencyDtoList.Add(currencyDto);
            }
        }

        return new CurrenciesDto(currencyDtoList);
    }
}