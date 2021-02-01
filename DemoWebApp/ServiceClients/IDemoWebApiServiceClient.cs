using DemoWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoWebApp.ServiceClients
{
    public interface IDemoWebApiServiceClient
    {
        Task<IList<WeatherForecast>> GetWeatherInfo();
    }
}
