using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Weather.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherClient _client;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherClient client)
        {
            _logger = logger;
            this._client = client;
        }

        [HttpGet]
        [Route("{city}")]
        public async Task<WeatherForecast> GetWeatherByCityAsync(string city)
        {
            //en caso de llegar una solicitud con el campo ciudad nulo o vacio
            if (string.IsNullOrEmpty(city))
            {
                _logger.LogError("Se recibe parametro null o vacio");
                return new WeatherForecast
                {
                    Summary = "City cannot be null or empty",
                    Status = "400"
                };
            }

            //realizamos la consulta al endpoint
            var res = await _client.GetCurrentWeatherAsync(city);

            //si la respuesta no es nula devolvemos el contenido de la misma
            if (res.forecast != null)
            {
                return new WeatherForecast()
                {
                    Summary = res.forecast.weather[0].description,
                    TemperatureC = (int)res.forecast.main.temp,
                    Date = DateTimeOffset.FromUnixTimeSeconds(res.forecast.dt).DateTime,
                    Status = res.status
                };
            }
            //en caso de error informamos que la ciudad no existe
            else
            {
                return new WeatherForecast()
                {
                    Summary = "City not found",
                    Status = res.status
                };
            }
        }
    }
}
