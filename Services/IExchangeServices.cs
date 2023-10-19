using CurrencyExchangeAPI.Models;

namespace CurrencyExchangeAPI.Services
{
    public interface IExchangeServices
    {

        public Task<ExchangeRates> GetLatestEcbRates();
        public Task<LatestDate> LatestDate();
        public Task<ExchangeRates> ConvertRates(double value);

    }
}
