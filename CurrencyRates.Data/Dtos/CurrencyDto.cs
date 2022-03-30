namespace CurrencyRates.Data.Dtos;

public sealed class CurrencyDto
{
    public CurrencyDto(string name, string charCode, double currentRate)
    {
        Name = name;
        CharCode = charCode;
        CurrentRate = currentRate;
    }

    public string Name { get; }
    public string CharCode { get; }
    public double CurrentRate { get; }
}