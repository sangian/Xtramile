using System.Text.Json.Serialization;

namespace Xtramile.WeatherUI.Dtos
{
    public class AppResultDto
    {
        [JsonPropertyName("succeeded")]
        public bool Succeeded { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("errors")]
        public string[] Errors { get; set; }
    }

    public class AppResultDto<T> : AppResultDto where T : class
    {
        [JsonPropertyName("data")]
        public T Data { get; set; }
    }
}
