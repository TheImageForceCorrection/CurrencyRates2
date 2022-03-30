namespace CurrencyRates.BusinessLogic;

public sealed class RelativeCurrencyRate
{
    public RelativeCurrencyRate(double relativeRate, string firstCurrencyName, string firstCurrencyCharCode,
        string secondCurrencyName, string secondCurrencyCharCode)
    {
        RelativeRate = relativeRate;
        FirstCurrencyName = firstCurrencyName;
        FirstCurrencyCharCode = firstCurrencyCharCode;
        SecondCurrencyName = secondCurrencyName;
        SecondCurrencyCharCode = secondCurrencyCharCode;
    }

    public double RelativeRate { get; }
    public string FirstCurrencyName { get; }
    public string FirstCurrencyCharCode { get; }
    public string SecondCurrencyName { get; }
    public string SecondCurrencyCharCode { get; }
}