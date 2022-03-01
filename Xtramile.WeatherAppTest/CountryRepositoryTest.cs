using Moq;
using System.Collections.Generic;
using Xtramile.WeatherApp.Common.Repositories;
using Xtramile.WeatherDomain.Entities;
using Xtramile.WeatherInfra.Persistence.Repositories;
using Xunit;

namespace Xtramile.WeatherAppTest
{
    public class CountryRepositoryTest
    {
        public Mock<MockCountryRepository> mock = new Mock<MockCountryRepository>();

        [Fact]
        public void ShouldReturnCountries()
        {
            mock.SetupGet(x => x.Countries).Returns(new List<Country>
            {
                new Country { Id = 1, Name = "Australia" },
                new Country { Id = 2, Name = "Indonesia" }
            });

            CountryRepository countryRepository = mock.Object;

            IList<Country> countries = countryRepository.GetCountries();

            Assert.Equal(2, countries.Count);
        }

        [Theory]
        [InlineData("indonesia")]
        [InlineData("australia")]
        public void ShouldBeValidCountryName(string country)
        {
            mock.SetupGet(x => x.Countries).Returns(new List<Country>
            {
                new Country { Id = 1, Name = "Australia" },
                new Country { Id = 2, Name = "Indonesia" }
            });

            CountryRepository countryRepository = mock.Object;

            bool isValidCountryName = countryRepository.IsValidCountryName(country);

            Assert.True(isValidCountryName);
        }

        [Theory]
        [InlineData("new zealand")]
        [InlineData("papua new guinea")]
        public void ShouldBeInvalidCountryName(string country)
        {
            mock.SetupGet(x => x.Countries).Returns(new List<Country>
            {
                new Country { Id = 1, Name = "Australia" },
                new Country { Id = 2, Name = "Indonesia" }
            });

            CountryRepository countryRepository = mock.Object;

            bool isValidCountryName = countryRepository.IsValidCountryName(country);

            Assert.False(isValidCountryName);
        }
    }
}
