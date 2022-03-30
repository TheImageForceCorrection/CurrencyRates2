using CurrencyRates.Data.Dtos;

namespace CurrencyRates.Data.Implementation.JsonDtos;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class Currency
{
    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public string? Name { get; set; }

    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public string? CharCode { get; set; }

    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public double Value { get; set; }

    public CurrencyDto? ToDto()
    {
        if (Name is null || CharCode is null || Value == 0)
        {
            return null;
        }

        return new CurrencyDto(Name, CharCode, Value);
    }
}