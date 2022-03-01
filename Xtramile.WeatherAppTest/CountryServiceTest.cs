using AutoMapper;
using Moq;
using System.Collections.Generic;
using Xtramile.WeatherApp.Common.Dtos;
using Xtramile.WeatherApp.Common.Mappings;
using Xtramile.WeatherApp.Common.Repositories;
using Xtramile.WeatherApp.Countries;
using Xtramile.WeatherDomain.Entities;
using Xtramile.WeatherInfra.Persistence.Repositories;
using Xunit;

namespace Xtramile.WeatherAppTest
{
    public class CountryServiceTest
    {
        public Mock<MockCountryRepository> countryRepoMock = new Mock<MockCountryRepository>();

        private static IMapper mapper;

        public CountryServiceTest()
        {
            if (mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                CountryServiceTest.mapper = mapper;
            }
        }

        [Fact]
        public void ShouldReturnCountries()
        {
            countryRepoMock.SetupGet(x => x.Countries).Returns(new List<Country>
            {
                new Country { Id = 1, Name = "Australia" },
                new Country { Id = 2, Name = "Indonesia" }
            });

            CountryRepository countryRepository = countryRepoMock.Object;

            CountryService countryService = new MockCountryService(countryRepository, mapper);

            AppResultDto<IList<CountryDto>> result = countryService.GetCountries();

            Assert.True(result.Succeeded);
        }
    }
}
