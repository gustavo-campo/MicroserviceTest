using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace Weather.Api
{
    public class ExternalEndpointsHealthChecks : IHealthCheck
    {
        private readonly ServiceSettings settings;

        //importamos ServiceSettings para acceder a la url de OperWeather
        public ExternalEndpointsHealthChecks(IOptions<ServiceSettings> options)
        {
            settings = options.Value;
        }
        //esta funcion devuelve un objeto tipo HealthCheckResult
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            Ping ping = new();
            //enviamos un ping a la url de nuestra api
            var reply = await ping.SendPingAsync(settings.OpenWheaterHost);
            //si el ping no es exitoso
            if (reply.Status != IPStatus.Success)
            {
                //retornamos unhealthy
                return HealthCheckResult.Unhealthy();
            }
            //si es exitoso retornamos healthy
            return HealthCheckResult.Healthy();
        }
    }
}
