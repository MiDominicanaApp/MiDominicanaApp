using System;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;

namespace MiDominicanaApp.Services
{
    public interface IMiDominicanaApi
    {
        // http://eladio37-001-site1.ftempurl.com/api/Fuels
        [Get("/api/Fuels")]
        Task<HttpResponseMessage> GetFuelsAsync();

        [Get("/api/Currency")]
        Task<HttpResponseMessage> GetCurrenciesAsync();

        [Get("/api/Province")]
        Task<HttpResponseMessage> GetProvincesAsync();
    }
}
