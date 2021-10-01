using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MiDominicanaApp.Services
{
    public interface IMiDominicanaApiService
    {
        Task<HttpResponseMessage> GetFuelsAsync();
        Task<HttpResponseMessage> GetCurrenciesAsync();
        Task<HttpResponseMessage> GetProvincesAsync();
    }
}
