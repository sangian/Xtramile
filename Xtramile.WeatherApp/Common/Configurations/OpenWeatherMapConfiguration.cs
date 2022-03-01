namespace Xtramile.WeatherApp.Common.Configurations
{
    public class OpenWeatherMapConfiguration
    {
        public const string ConfigKey = "OpenWeatherMap";

        public string ApiKey { get; set; }
        public string Units { get; set; }
        public int ApiTimeout { get; set; }
    }
}
