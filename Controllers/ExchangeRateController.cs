using CurrencyExchangeAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;

namespace CurrencyExchangeAPI.Controllers
{
    [Route("api/exchangerates")]
    [ApiController]
    public class ExchangeRateController : ControllerBase
    {

        private readonly IExchangeServices _srv;

        public ExchangeRateController(IExchangeServices srv)
        {
            _srv = srv;
        }

        [HttpGet("/api/rates/getLatestEcbRates")]
        public async Task<IActionResult> GetLatestEcbRates()
        {
            try
            {
                var result = await this._srv.GetLatestEcbRates();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to retrieve exchange rates: {ex.Message}");
            }
        }

        [HttpGet("/api/rates/convertRates")]
        public async Task<IActionResult> ConvertRates(double value)
        {
            try
            {
                var result = await this._srv.ConvertRates(value);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to retrieve exchange rates: {ex.Message}");
            }
        }



    }
}
