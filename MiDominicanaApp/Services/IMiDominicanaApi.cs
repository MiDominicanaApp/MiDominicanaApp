using System;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;

namespace MiDominicanaApp.Services
{
    public interface IMiDominicanaApi
    {
        [Get(Config.FuelApi)]
        Task<HttpResponseMessage> GetFuelsAsync();

        [Get("/api/Currency")]
        Task<HttpResponseMessage> GetCurrenciesAsync();

        [Get(Config.ProvinceApi)]
        Task<HttpResponseMessage> GetProvincesAsync();

        [Get(Config.ProvinceApiId)]
        Task<HttpResponseMessage> GetProvinceDetailAsync(int provinceId);
    }
}
