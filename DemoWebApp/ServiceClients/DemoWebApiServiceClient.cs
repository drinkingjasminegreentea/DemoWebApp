using DemoWebApp.Models;
using DemoWebApp.Settings;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DemoWebApp.ServiceClients
{
    public class DemoWebApiServiceClient: IDemoWebApiServiceClient
    {
        private DemoWebApiSettings _settings;
        private HttpClient _httpClient;

        public DemoWebApiServiceClient(HttpClient client, IOptions<DemoWebApiSettings> options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            _settings = options.Value;
            _httpClient = client;
        }

        public async Task<IList<WeatherForecast>> GetWeatherInfo()
        {
            var token = await GetAuthToken();
            var defaultRequestHeaders = _httpClient.DefaultRequestHeaders;

            if (defaultRequestHeaders.Accept == null ||
               !defaultRequestHeaders.Accept.Any(m => m.MediaType == "application/json"))
            {
                _httpClient.DefaultRequestHeaders.Accept.Add(new
                  MediaTypeWithQualityHeaderValue("application/json"));
            }
            defaultRequestHeaders.Authorization =
              new AuthenticationHeaderValue("bearer", token);

            HttpResponseMessage response = await _httpClient.GetAsync(_settings.BaseAddress);
            var json = await response.Content.ReadAsStringAsync();

            var forecasts = JsonConvert.DeserializeObject<List<WeatherForecast>>(json);

            return forecasts;
        }

        private async Task<string> GetAuthToken()
        {
            IConfidentialClientApplication app;

            app = ConfidentialClientApplicationBuilder.Create(_settings.ClientId)
                .WithClientSecret(_settings.ClientSecret)
                .WithAuthority(new Uri(_settings.Authority))
                .Build();

            string[] ResourceIds = new string[] { _settings.ResourceId };

            var tokenResponse = await app.AcquireTokenForClient(ResourceIds).ExecuteAsync();

            return tokenResponse.AccessToken;
        }


    }
}
