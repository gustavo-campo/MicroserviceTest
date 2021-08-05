using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Weather.Api;
using Weather.Api.Controllers;
using Xunit;

namespace Weather.UnitTests
{
    public class WeatherForecastControllerTest
    {
        [Fact]
        public async void Get_WhenCalledWithEmptyCity_ReturnsForecast()
        {
            ILogger<WeatherForecastController> logger = new Mock<ILogger<WeatherForecastController>>().Object;

            //HttpClient httpClient, IOptions<ServiceSettings> options
            var httpClient = Mock.Of<HttpClient>();
            var options = Mock.Of<IOptions<ServiceSettings>>();

            WeatherClient client = new Mock<WeatherClient>(httpClient, options).Object;

            var controller = new WeatherForecastController(logger, client);

            var forecastResult = await controller.GetWeatherByCityAsync("");

            Assert.IsType<WeatherForecast>(forecastResult);
        }

        [Fact]
        public async void Get_WhenCalledWithNullCity_ReturnsForecast()
        {
            ILogger<WeatherForecastController> logger = new Mock<ILogger<WeatherForecastController>>().Object;

            //HttpClient httpClient, IOptions<ServiceSettings> options
            var httpClient = Mock.Of<HttpClient>();
            var options = Mock.Of<IOptions<ServiceSettings>>();

            WeatherClient client = new Mock<WeatherClient>(httpClient, options).Object;

            var controller = new WeatherForecastController(logger, client);

            var forecastResult = await controller.GetWeatherByCityAsync(null);

            Assert.IsType<WeatherForecast>(forecastResult);
        }
    }
}
