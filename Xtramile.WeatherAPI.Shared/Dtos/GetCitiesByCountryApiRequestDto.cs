using System.ComponentModel.DataAnnotations;

namespace Xtramile.WeatherAPI.Shared.Dtos
{
    public class GetCitiesByCountryApiRequestDto
    {
        [Required]
        [StringLength(100)]
        public string Country { get; set; }
    }
}
