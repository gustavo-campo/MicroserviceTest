using System.Threading.Tasks;

namespace Weather.Api
{
    public interface IWeatherClient
    {
        public Task<WeatherClient.Response> GetCurrentWeatherAsync(string city);
    }
}