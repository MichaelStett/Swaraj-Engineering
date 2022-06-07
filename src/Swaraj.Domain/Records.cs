using System;

namespace Swaraj.Domain
{
    public static class Records
    {
        public record WeatherData(DateTime Date, int Temperature, string Summary);
    }
}
