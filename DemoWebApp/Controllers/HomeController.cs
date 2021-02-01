using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DemoWebApp.ViewModels;
using DemoWebApp.ServiceClients;
using System.Threading.Tasks;

namespace DemoWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDemoWebApiServiceClient _demoWebApiServiceClient;

        public HomeController(IDemoWebApiServiceClient client)
        {
            _demoWebApiServiceClient = client;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new HomeIndexViewModel
            {
                Forecasts = await _demoWebApiServiceClient.GetWeatherInfo(),
                UserName = "test user"
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
