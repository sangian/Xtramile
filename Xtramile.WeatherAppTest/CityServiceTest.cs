using AutoMapper;
using Moq;
using System.Collections.Generic;
using Xtramile.WeatherApp.Cities;
using Xtramile.WeatherApp.Common.Dtos;
using Xtramile.WeatherApp.Common.Mappings;
using Xtramile.WeatherApp.Common.Repositories;
using Xtramile.WeatherDomain.Entities;
using Xtramile.WeatherInfra.Persistence.Repositories;
using Xunit;

namespace Xtramile.WeatherAppTest
{
    public class CityServiceTest
    {
        public Mock<MockCountryRepository> countryRepoMock = new Mock<MockCountryRepository>();
        public Mock<MockCityRepository> cityRepoMock = new Mock<MockCityRepository>();

        private static IMapper mapper;

        public CityServiceTest()
        {
            if (mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                CityServiceTest.mapper = mapper;
            }
        }

        [Theory]
        [InlineData("indonesia")]
        [InlineData("australia")]
        public void ShouldReturnCitiesOfCountry(string country)
        {
            countryRepoMock.SetupGet(x => x.Countries).Returns(new List<Country>
            {
                new Country { Id = 1, Name = "Australia" },
                new Country { Id = 2, Name = "Indonesia" }
            });

            cityRepoMock.SetupGet(x => x.Cities).Returns(new List<City>
            {
                new City { Id = 1, Name = "Sydney", Country = "Australia" },
                new City { Id = 2, Name = "Melbourne", Country = "Australia" },
                new City { Id = 3, Name = "Brisbane", Country = "Australia" },
                new City { Id = 4, Name = "Perth", Country = "Australia" },
                new City { Id = 5, Name = "Jakarta", Country = "Indonesia" },
                new City { Id = 6, Name = "Bandung", Country = "Indonesia" },
                new City { Id = 7, Name = "Surabaya", Country = "Indonesia" },
                new City { Id = 8, Name = "Yogyakarta", Country = "Indonesia" }
            });

            CountryRepository countryRepository = countryRepoMock.Object;
            CityRepository cityRepository = cityRepoMock.Object;

            CityService cityService = new MockCityService(countryRepository, cityRepository, mapper);

            AppResultDto<IList<CityDto>> result = cityService.GetCitiesByCountry(new GetCitiesByCountryRequest
            {
                Country = country
            });

            Assert.True(result.Succeeded);
        }
    }
}
