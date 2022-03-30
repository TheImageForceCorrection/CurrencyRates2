using System;

namespace CurrencyRates.BusinessLogic.Implementation;

public sealed class RelativeCurrencyRateCalculator : IRelativeCurrencyRateCalculator
{
    public double Calculate(double firstCurrencyRate, double secondCurrencyRate)
    {
        if (firstCurrencyRate == 0 || secondCurrencyRate == 0)
        {
            throw new InvalidOperationException("Zero currency rate is not allowed");
        }

        return firstCurrencyRate / secondCurrencyRate;
    }
}