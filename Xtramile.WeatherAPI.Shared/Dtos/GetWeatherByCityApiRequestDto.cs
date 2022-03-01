using System.ComponentModel.DataAnnotations;

namespace Xtramile.WeatherAPI.Shared.Dtos
{
    public class GetWeatherByCityApiRequestDto
    {
        [Required]
        [StringLength(50)]
        public string City { get; set; }
    }
}
