using CurrencyExchangeAPI.Models;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Newtonsoft.Json;
using System.Net.NetworkInformation;
using System.Reflection;

namespace CurrencyExchangeAPI.Services
{
    public class ExchangeServices : IExchangeServices
    {

        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public ExchangeServices(HttpClient httpClient, ApiKey apiKey)
        {
            _httpClient = httpClient;
            _apiKey = apiKey();
        }

        public async Task<ExchangeRates> GetLatestEcbRates()
        {
            string httpString = "client/latest?apikey=" + _apiKey + "&base_currency=EUR";
            var response = await _httpClient.GetAsync(httpString);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ExchangeRates>(content);
            }else
            {
                throw new Exception("Failed to retrieve data from the external API.");
            }
        }

        public async Task<LatestDate> LatestDate()
        {
            string httpString = "client/latest?apikey=" + _apiKey;
            var response = await _httpClient.GetAsync(httpString);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<LatestDate>(content);
            }
            else
            {
                throw new Exception("Failed to retrieve data from the external API.");
            }
        }

        public async Task<ExchangeRates> ConvertRates(double value)
        {
            LatestDate latestDate = await LatestDate();

            string httpString = "client/convert?apikey=" + _apiKey + "&from=EUR&amount=" + value.ToString() + "&date=" + latestDate.Date;
            var response = await _httpClient.GetAsync(httpString);


            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ExchangeRates>(content);
            }
            else
            {
                throw new Exception("Failed to retrieve data from the external API.");
            }
        }
    }
}
