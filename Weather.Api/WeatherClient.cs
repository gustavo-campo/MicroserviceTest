using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Weather.Api
{
    public class WeatherClient : IWeatherClient
    {
        //vamos a usar el hhtpclient para contactar el endpoint
        private readonly HttpClient _httpClient;
        //y el servicio con los settings para configurar el mismo
        private readonly ServiceSettings _settings;

        public WeatherClient(HttpClient httpClient, IOptions<ServiceSettings> options)
        {
            this._httpClient = httpClient;
            _settings = options.Value;
        }

        //la respuesta de la api es un objeto compuesto, pero no lo vamos a usar en su totalidad
        //declaramos un record type con cada porcion del objeto que necesitamos
        public record Weather(string description);

        public record Main(decimal temp);

        //este record type se utilizara para armar la respuesta del servidor
        public record Response(Forecast forecast, String status);
        //por ultimo este objeto sera el registro completo que recibiremos con los dos records anteriormente declarados y la propiedad dt que corresponde al timestamp
        public record Forecast(Weather[] weather, Main main, long dt);

        //declaramos el metodo que vamos a utilizar
        public async Task<Response> GetCurrentWeatherAsync(string city)
        {
            if (string.IsNullOrEmpty(city)) city = "";

            //armamos la url tomando la direccion del endpoint desde el appsetting, el valor city desde el parametro de metodo y la apikey desde los user secrets
            string url = $"https://{ _settings.OpenWheaterHost }/data/2.5/weather?q={ city }&appid={ _settings.ApiKey }&units=metric";
            //invocamos el endpoint pasandole el tipo de claso que vamos a usar para deserializar y la url correspondiente
            try
            {
                Forecast forecast = await _httpClient.GetFromJsonAsync<Forecast>(url);
                Response res = new(forecast, "200");
                return res;
            }
            catch (Exception e)
            {

                Response res = new(null, "404");
                return res;
            }

        }
    }
}
