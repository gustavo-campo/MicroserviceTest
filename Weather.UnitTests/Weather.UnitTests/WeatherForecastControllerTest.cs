using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Weather.Api;
using Weather.Api.Controllers;
using Xunit;

namespace Weather.UnitTests
{
    public class WeatherForecastControllerTest
    {
        [Fact]
        public void GetWeatherByCityAsync_WithNullCity_ReturnsCityNotFound()
        {
            //Arrange

            //Act

            //Assert
        }
    }
}
