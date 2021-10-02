using System;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;

namespace MiDominicanaApp.Services
{
    public interface IMiDominicanaApi
    {
        [Get("/api/Fuels")]
        Task<HttpResponseMessage> GetFuelsAsync();

        [Get("/api/Currency")]
        Task<HttpResponseMessage> GetCurrenciesAsync();

        [Get("/api/Province")]
        Task<HttpResponseMessage> GetProvincesAsync();

        [Get("/api/Province/{id}")]
        Task<HttpResponseMessage> GetProvinceDetailAsync(int ID);
    }
}
