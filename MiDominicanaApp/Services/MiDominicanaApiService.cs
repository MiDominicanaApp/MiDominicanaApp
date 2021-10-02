using System;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;

namespace MiDominicanaApp.Services
{
    public class MiDominicanaApiService : IMiDominicanaApiService
    {
        public async Task<HttpResponseMessage> GetCurrenciesAsync()
        {
            return await RestService.For<IMiDominicanaApi>(Config.MiDominicanaApiBaseURL).GetCurrenciesAsync();
        }

        public async Task<HttpResponseMessage> GetFuelsAsync()
        {
            return await RestService.For<IMiDominicanaApi>(Config.MiDominicanaApiBaseURL).GetFuelsAsync();
        }

        public async Task<HttpResponseMessage> GetProvinceDetailAsync(int ID)
        {
            return await RestService.For<IMiDominicanaApi>(Config.MiDominicanaApiBaseURL).GetProvinceDetailAsync(ID);
        }

        public async Task<HttpResponseMessage> GetProvincesAsync()
        {
            return await RestService.For<IMiDominicanaApi>(Config.MiDominicanaApiBaseURL).GetProvincesAsync();
        }
    }
}
