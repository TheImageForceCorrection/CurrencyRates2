namespace CurrencyRates.BusinessLogic.Implementation;

public interface IRelativeCurrencyRateCalculator
{
    double Calculate(double firstCurrencyRate, double secondCurrencyRate);
}