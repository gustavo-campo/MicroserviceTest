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
            //Arrange
            //mock dependency del logger para el controlador WeatherForecastController
            ILogger<WeatherForecastController> logger = new Mock<ILogger<WeatherForecastController>>().Object;

            //mock dependencies para el WeatherClient
            var httpClient = Mock.Of<HttpClient>();
            var options = Mock.Of<IOptions<ServiceSettings>>();

            //mock del cliente que se conecta con openweathermap.org
            WeatherClient client = new Mock<WeatherClient>(httpClient, options).Object;
            //mock del controlador que contiene el metodo GET a testear
            var controller = new WeatherForecastController(logger, client);

            //Act
            //probamos el metodo pasandole un parametro vacio
            var forecastResult = await controller.GetWeatherByCityAsync("");

            //Assert
            //esperamos como resultado un objeto WeatherForecast
            Assert.IsType<WeatherForecast>(forecastResult);
        }

        [Fact]
        public async void Get_WhenCalledWithNullCity_ReturnsForecast()
        {
            //Arrange
            ILogger<WeatherForecastController> logger = new Mock<ILogger<WeatherForecastController>>().Object;

            var httpClient = Mock.Of<HttpClient>();
            var options = Mock.Of<IOptions<ServiceSettings>>();

            WeatherClient client = new Mock<WeatherClient>(httpClient, options).Object;

            var controller = new WeatherForecastController(logger, client);

            //Act
            var forecastResult = await controller.GetWeatherByCityAsync(null);

            //Assert
            Assert.IsType<WeatherForecast>(forecastResult);
        }
    }
}
