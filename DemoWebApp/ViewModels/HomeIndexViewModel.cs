using DemoWebApp.Models;
using System.Collections.Generic;

namespace DemoWebApp.ViewModels
{
    public class HomeIndexViewModel
    {
        public IList<WeatherForecast> Forecasts;

        public string UserName;
    }
}
