using NetProcalc23Feb.Domain.Enums;

namespace NetProcalc23Feb.Application.Services;

public static class FrequencyConverter
{
    public static decimal ToMonthly(decimal amount, Frequency from)
    {
        return from switch
        {
            Frequency.Monthly => amount,
            Frequency.SemiMonthly => amount * 2m,
            Frequency.BiWeekly => amount * 26m / 12m,
            Frequency.Weekly => amount * 52m / 12m,
            _ => amount
        };
    }
}
