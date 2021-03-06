using System.ComponentModel.DataAnnotations;

namespace Xtramile.WeatherAPI.Shared.Dtos
{
    public class GetCitiesByCountryApiRequestDto
    {
        [Required]
        [StringLength(50)]
        public string Country { get; set; }
    }
}
